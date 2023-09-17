using ListaDeTarefas.Application.Interfaces.Services;
using ListaDeTarefas.Application.Interfaces.UnitOfWork;
using ListaDeTarefas.Application.Interfaces.Usuarios;
using ListaDeTarefas.Application.Interfaces.Usuarios.Handler;
using ListaDeTarefas.Application.Usuarios.Commands.Criar.Request;
using ListaDeTarefas.Application.Usuarios.Commands.Criar.Response;
using ListaDeTarefas.Domain.Abstraction;
using ListaDeTarefas.Domain.Models;
using ListaDeTarefas.Domain.ValueObjects;
using System.Net;

namespace ListaDeTarefas.Application.Usuarios.Commands.Criar.Handler
{
    public class CriarUsuarioHandler : ICriarUsuarioHandler
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IEmailService _emailService;

        public CriarUsuarioHandler(IUnitOfWork unitOfWork, IUsuarioRepositorio usuarioRepositorio, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _usuarioRepositorio = usuarioRepositorio;
            _emailService = emailService;
        }

        public async Task<IResponse> Handle(CriarUsuarioRequest request)
        {
            request.Validar();
            if (!request.IsValid)
            {
                return new CriarUsuarioResponse(StatusCode: HttpStatusCode.BadRequest,
                                                Mensagem: "Falha na requisição para criar um usuário.",
                                                Notifications: request.Notifications);
            }

            try
            {
                _unitOfWork.BeginTransaction();

                var emailCadastrado = await _usuarioRepositorio.EmailCadastrado(request.Email);

                if (emailCadastrado)
                {
                    return new CriarUsuarioResponse(StatusCode: HttpStatusCode.BadRequest,
                                                Mensagem: "Este e-mail já está cadastrado.",
                                                Notifications: request.Notifications);
                }

                var usuario = new Usuario(
                                    login: new Domain.ValueObjects.Login(username: request.Login),
                                    senha: new Senha(senha: request.Senha),
                                    email: new Email(request.Email));

                await _usuarioRepositorio.AdicionarAsync(usuario);
                await _emailService.EnviarEmailVerificacao(usuario);

                _unitOfWork.Commit();

                return new CriarUsuarioResponse(StatusCode: HttpStatusCode.OK,
                                                Mensagem: $"Usuario {request.Login} criado com sucesso!.",
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
