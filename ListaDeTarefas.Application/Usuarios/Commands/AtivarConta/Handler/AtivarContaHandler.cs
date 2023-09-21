using ListaDeTarefas.Application.Interfaces.Services;
using ListaDeTarefas.Application.Interfaces.UnitOfWork;
using ListaDeTarefas.Application.Interfaces.Usuarios;
using ListaDeTarefas.Application.Interfaces.Usuarios.Handler;
using ListaDeTarefas.Application.Usuarios.Commands.AtivarConta.Request;
using ListaDeTarefas.Application.Usuarios.Commands.AtivarConta.Response;
using ListaDeTarefas.Shared.Interfaces;
using System.Net;

namespace ListaDeTarefas.Application.Usuarios.Commands.AtivarConta.Handler
{
    public sealed class AtivarContaHandler : IAtivarContaHandler
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailServices _emailService;

        public AtivarContaHandler(IUsuarioRepositorio usuarioRepositorio, IUnitOfWork unitOfWork, IEmailServices emailService)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _unitOfWork = unitOfWork;
            _emailService = emailService;
        }

        public async Task<IResponse> Handle(AtivarContaRequest request)
        {
            #region Validações
            request.Validar();
            if (!request.IsValid)
            {
                return new AtivarContaResponse(statusCode: HttpStatusCode.BadRequest,
                                                mensagem: "Requisição inválida. Por favor, valide os dados informados.",
                                                notificacoes: request.Notifications);
            }
            #endregion
            try
            {
                #region Busca o Usuário 
                var usuarioDB = await _usuarioRepositorio.BuscarPorEmailAsync(request.Email);
                if (usuarioDB is null)
                {
                    return new AtivarContaResponse(statusCode: HttpStatusCode.BadRequest,
                                                    mensagem: "E-mail informado não está cadastrado.");
                }

                if (usuarioDB.Email is null || usuarioDB.Email.VerificarEmail is null)
                {
                    return new AtivarContaResponse(statusCode: HttpStatusCode.BadRequest,
                                                    mensagem: "Não foi possível verificar o código de ativação. Os dados estão ausentes.");
                }
                #endregion

                #region Verifica o código de ativação
                var retornoCodigoValido = usuarioDB.Email.VerificarEmail.VerificarCodigo(request.CodigoAtivacao);
                if (!string.IsNullOrEmpty(retornoCodigoValido))
                {
                    return new AtivarContaResponse(statusCode: HttpStatusCode.BadRequest,
                                                    mensagem: retornoCodigoValido);
                }
                #endregion

                #region Ativa a conta do Usuário
                _unitOfWork.BeginTransaction();

                var ativado = await _usuarioRepositorio.AtivarConta(usuarioDB);
                if (ativado == false)
                {
                    _unitOfWork.Rollback();
                    return new AtivarContaResponse(statusCode: HttpStatusCode.InternalServerError,
                                                    mensagem: "Falha ao ativar conta do usuário. Por favor, tente novamente mais tarde.",
                                                    notificacoes: usuarioDB.Notifications);
                }
                _unitOfWork.Commit();
                #endregion

                #region Envia um E-mail de confirmação de conta ativada
                await _emailService.EnviarEmailSucessoAtivacao(usuarioDB);
                #endregion

                return new AtivarContaResponse(statusCode: HttpStatusCode.OK,
                                                    mensagem: $"{usuarioDB.Login.Username}, sua conta foi ativada com sucesso!");
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
