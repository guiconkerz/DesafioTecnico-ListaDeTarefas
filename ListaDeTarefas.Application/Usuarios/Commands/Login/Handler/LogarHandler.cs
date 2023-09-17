using ListaDeTarefas.Application.Interfaces.UnitOfWork;
using ListaDeTarefas.Application.Interfaces.Usuarios;
using ListaDeTarefas.Application.Interfaces.Usuarios.Handler;
using ListaDeTarefas.Application.Usuarios.Commands.Criar.Response;
using ListaDeTarefas.Application.Usuarios.Commands.Login.Request;
using ListaDeTarefas.Application.Usuarios.Commands.Login.Response;
using ListaDeTarefas.Domain.Abstraction;
using System.Net;

namespace ListaDeTarefas.Application.Usuarios.Commands.Login.Handler
{
    public sealed class LogarHandler : ILogarHandler
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IUnitOfWork _unitOfWork;

        public LogarHandler(IUsuarioRepositorio usuarioRepositorio, IUnitOfWork unitOfWork)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _unitOfWork = unitOfWork;
        }

        public async Task<IResponse> Handle(LogarRequest request)
        {
            request.Validar();
            if (!request.IsValid)
            {
                return new LogarResponse(StatusCode: HttpStatusCode.BadRequest,
                                                Mensagem: "Falha na requisição de login.",
                                                Notifications: request.Notifications);
            }

            try
            {
                var usuarioDB = await _usuarioRepositorio.BuscarPorEmailAsync(request.Email);
                if (usuarioDB is null)
                {
                    return new LogarResponse(StatusCode: HttpStatusCode.BadRequest,
                                                    Mensagem: "Perfil não encontrado.",
                                                    Notifications: request.Notifications);
                }

                if (!usuarioDB.Senha.Verificar(request.Senha, usuarioDB.Senha.Password))
                {
                    return new LogarResponse(StatusCode: HttpStatusCode.BadRequest,
                                                    Mensagem: "Usuário e/ou senha inválidos.",
                                                    Notifications: request.Notifications);
                }

                if (!usuarioDB.Email.VerificarEmail.Ativo)
                {
                    return new LogarResponse(StatusCode: HttpStatusCode.BadRequest,
                                                    Mensagem: "Conta inativa.",
                                                    Notifications: request.Notifications);
                }

                return new LogarResponse(StatusCode: HttpStatusCode.OK,
                                                    Mensagem: string.Empty,
                                                    Notifications: request.Notifications)
                                                    { 
                                                        Id = usuarioDB.UsuarioId.ToString(),
                                                        Email = usuarioDB.Email.Endereco,
                                                        Login = usuarioDB.Login.Username,
                                                        Perfil = Array.Empty<string>()
                                                    };
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
