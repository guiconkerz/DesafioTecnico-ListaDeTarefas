using ListaDeTarefas.Application.Interfaces.Services;
using ListaDeTarefas.Application.Interfaces.UnitOfWork;
using ListaDeTarefas.Application.Interfaces.Usuarios;
using ListaDeTarefas.Application.Interfaces.Usuarios.Handler;
using ListaDeTarefas.Application.Usuarios.Commands.AlterarSenha.Response;
using ListaDeTarefas.Application.Usuarios.Commands.SolicitarAlteracaoSenha.Request;
using ListaDeTarefas.Application.Usuarios.Commands.SolicitarAlteracaoSenha.Response;
using ListaDeTarefas.Shared.Interfaces;
using System.Net;

namespace ListaDeTarefas.Application.Usuarios.Commands.SolicitarAlteracaoSenha.Handler
{
    public sealed class SolicitarAlteracaoSenhaHandler : ISolicitarAlteracaoSenhaHandler
    {
        private readonly IEmailServices _emailServices;
        public readonly IUsuarioRepositorio _usuarioRepositorio;
        public readonly IUnitOfWork _unitOfWork;

        public SolicitarAlteracaoSenhaHandler(IEmailServices emailServices, IUsuarioRepositorio usuarioRepositorio, IUnitOfWork unitOfWork)
        {
            _emailServices = emailServices;
            _usuarioRepositorio = usuarioRepositorio;
            _unitOfWork = unitOfWork;
        }

        public async Task<IResponse> Handle(SolicitarAlteracaoSenhaRequest request)
        {
            #region Validações
            request.Validar();
            if (!request.IsValid)
            {
                return new SolicitarAlteracaoSenhaResponse(StatusCode: HttpStatusCode.BadRequest,
                                                           Mensagem: "Requisição inválida. Por favor, valide os dados informados.",
                                                           Notifications: request.Notifications);
            }
            #endregion

            try
            {
                #region Buscar usuário pelo E-mail
                var usuarioDB = await _usuarioRepositorio.BuscarPorEmailAsync(request.Email);
                if (usuarioDB is null)
                {
                    return new SolicitarAlteracaoSenhaResponse(StatusCode: HttpStatusCode.BadRequest,
                                                    Mensagem: $"Não foi encontrado usuário com este E-mail.",
                                                    Notifications: request.Notifications);
                }
                #endregion

                #region Gerar novo código de alteração e salvar no banco
                usuarioDB.Senha.GerarCodigoAlteracao();

                _unitOfWork.BeginTransaction();

                var alterado = await _usuarioRepositorio.CadastrarCodigoAlteracaoSenha(usuarioDB);
                if (alterado == false)
                {
                    _unitOfWork.Rollback();
                    return new SolicitarAlteracaoSenhaResponse(StatusCode: HttpStatusCode.InternalServerError,
                                                               Mensagem: $"Houve uma falha ao cadastrar o código de alteração da senha. Por favor, tente novamente mais tarde.",
                                                               Notifications: request.Notifications);
                }

                _unitOfWork.Commit();

                #endregion
                await _emailServices.EnviarEmailCodigoAlteracaoSenha(usuarioDB);
                #region Enviar E-mail com código para alteração de senha

                #endregion

                return new SolicitarAlteracaoSenhaResponse(StatusCode: HttpStatusCode.OK,
                                                Mensagem: $"Código de alteração de senha enviado para o E-mail {usuarioDB.Email.Endereco}.",
                                                Notifications: request.Notifications);
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
