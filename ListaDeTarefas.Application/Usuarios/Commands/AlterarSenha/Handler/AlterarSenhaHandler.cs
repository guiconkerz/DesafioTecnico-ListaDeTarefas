using ListaDeTarefas.Application.Interfaces.UnitOfWork;
using ListaDeTarefas.Application.Interfaces.Usuarios;
using ListaDeTarefas.Application.Interfaces.Usuarios.Handler;
using ListaDeTarefas.Application.Usuarios.Commands.AlterarSenha.Request;
using ListaDeTarefas.Application.Usuarios.Commands.AlterarSenha.Response;
using ListaDeTarefas.Shared.Interfaces;
using System.Net;

namespace ListaDeTarefas.Application.Usuarios.Commands.AlterarSenha.Handler
{
    public sealed class AlterarSenhaHandler : IAlterarSenhaHandler
    {
        public readonly IUsuarioRepositorio _usuarioRepositorio;
        public readonly IUnitOfWork _unitOfWork;
        public AlterarSenhaHandler(IUsuarioRepositorio usuarioRepositorio, IUnitOfWork unitOfWork)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _unitOfWork = unitOfWork;
        }

        public async Task<IResponse> Handle(AlterarSenhaRequest request)
        {

            #region Valições
            request.Validar();
            if (!request.IsValid)
            {
                return new AlterarSenhaResponse(StatusCode: HttpStatusCode.BadRequest,
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
                    return new AlterarSenhaResponse(StatusCode: HttpStatusCode.BadRequest,
                                                    Mensagem: $"Não foi encontrado usuário com este E-mail.",
                                                    Notifications: request.Notifications);
                }
                #endregion

                #region Validar código de alteração
                var retornoCodigoValido = usuarioDB.Senha.VerificarCodigo(request.CodigoAlteracaoSenha);
                if (!string.IsNullOrEmpty(retornoCodigoValido))
                {
                    return new AlterarSenhaResponse(StatusCode: HttpStatusCode.BadRequest,
                                                    Mensagem: retornoCodigoValido,
                                                    Notifications: request.Notifications);
                }
                #endregion

                #region Alterar senha do usuário
                usuarioDB.Senha.AlterarSenha(request.NovaSenha);

                _unitOfWork.BeginTransaction();

                var senhaAlterada = await _usuarioRepositorio.AlterarSenha(usuarioDB);
                if (senhaAlterada == false)
                {
                    return new AlterarSenhaResponse(StatusCode: HttpStatusCode.InternalServerError,
                                                    Mensagem: $"Houve uma falha ao alterar senha do usuário {usuarioDB.Email.Endereco}. Por favor, tente novamente mais tarde.",
                                                    Notifications: request.Notifications);
                }

                _unitOfWork.Commit();
                #endregion

                return new AlterarSenhaResponse(StatusCode: HttpStatusCode.OK,
                                                Mensagem: $"Senha alterada com sucesso!",
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
