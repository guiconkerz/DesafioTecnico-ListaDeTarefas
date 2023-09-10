using ListaDeTarefas.Application.Interfaces.UnitOfWork;
using ListaDeTarefas.Application.Interfaces.Usuarios;
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

        public CriarUsuarioHandler(IUnitOfWork unitOfWork, IUsuarioRepositorio usuarioRepositorio)
        {
            _unitOfWork = unitOfWork;
            _usuarioRepositorio = usuarioRepositorio;
        }

        public async Task<IResponse> Handle(CriarUsuarioRequest request)
        {
            request.Validar();
            if (!request.IsValid)
            {
                return new CriarUsuarioResponse(statusCode: HttpStatusCode.BadRequest,
                                            mensagem: "Falha na requisição para criar um usuário.",
                                            notifications: request.Notifications);
            }

            var usuario = new Usuario(
                login: new Login(username: "admin"),
                senha: new Senha(senha: "@Admin123"),
                email: new Email("email@email.com"));

            try
            {
                _unitOfWork.BeginTransaction();
                await _usuarioRepositorio.Adicionar(usuario);
                _unitOfWork.Commit();

                return new CriarUsuarioResponse(statusCode: HttpStatusCode.OK,
                                            mensagem: $"Usuario {request.Login} criado com sucesso!.",
                                            notifications: request.Notifications);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw;
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }
    }
}
