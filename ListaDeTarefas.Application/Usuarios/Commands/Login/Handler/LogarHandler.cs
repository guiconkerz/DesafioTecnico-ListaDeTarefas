using ListaDeTarefas.Application.Interfaces.Services;
using ListaDeTarefas.Application.Interfaces.UnitOfWork;
using ListaDeTarefas.Application.Interfaces.Usuarios;
using ListaDeTarefas.Application.Interfaces.Usuarios.Handler;
using ListaDeTarefas.Application.Usuarios.Commands.Login.Request;
using ListaDeTarefas.Application.Usuarios.Commands.Login.Response;
using ListaDeTarefas.Shared.Interfaces;
using System.Net;

namespace ListaDeTarefas.Application.Usuarios.Commands.Login.Handler
{
    public sealed class LogarHandler : ILogarHandler
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenServices __tokenServices;

        public LogarHandler(IUsuarioRepositorio usuarioRepositorio, IUnitOfWork unitOfWork, ITokenServices tokenServices)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _unitOfWork = unitOfWork;
            __tokenServices = tokenServices;
        }

        public async Task<IResponse> Handle(LogarRequest request)
        {
            #region Validação de entrada
            request.Validar();
            if (!request.IsValid)
            {
                return new LogarResponse(statusCode: HttpStatusCode.BadRequest,
                                                mensagem: "Falha na requisição de login.",
                                                notifications: request.Notifications);
            }
            #endregion

            try
            {
                #region Busca o usuário
                var usuarioDB = await _usuarioRepositorio.BuscarPorEmailAsync(request.Email);
                if (usuarioDB is null)
                {
                    return new LogarResponse(statusCode: HttpStatusCode.BadRequest,
                                                    mensagem: "Usuário não encontrado.",
                                                    notifications: request.Notifications);
                }
                #endregion

                #region Valida senha
                if (!usuarioDB.Senha.Verificar(request.Senha, usuarioDB.Senha.Password))
                {
                    return new LogarResponse(statusCode: HttpStatusCode.BadRequest,
                                                    mensagem: "Senha inválida.",
                                                    notifications: request.Notifications);
                }
                #endregion

                #region Verifica se a verificação da conta está ativa
                if (!usuarioDB.Email.VerificarEmail.Ativo)
                {
                    return new LogarResponse(statusCode: HttpStatusCode.BadRequest,
                                                    mensagem: "Conta ainda não foi verificada. Por favor, verifique sua conta para ativa-la.",
                                                    notifications: request.Notifications);
                }
                #endregion


                #region Gera o token e response do usuário autenticado
                var response = new LogarResponse(statusCode: HttpStatusCode.OK,
                                                 mensagem: $"{usuarioDB.Login.Username} autenticado com sucesso!",
                                                 id: usuarioDB.Id.ToString(),
                                                 login: usuarioDB.Login.Username,
                                                 email: usuarioDB.Email.Endereco,
                                                 perfil: usuarioDB.Perfil.Nome);

                response.Token = __tokenServices.GerarToken(response);
                response.RefreshToken = __tokenServices.GerarRefreshToken();
                __tokenServices.SalvarRefreshToken(response.Email, response.RefreshToken);

                #endregion

                return response;
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw new Exception($"{ex.Message}");
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }
    }
}
