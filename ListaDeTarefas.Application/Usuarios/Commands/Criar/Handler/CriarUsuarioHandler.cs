using ListaDeTarefas.Application.Interfaces.UnitOfWork;
using ListaDeTarefas.Application.Interfaces.Usuarios;
using ListaDeTarefas.Application.Usuarios.Commands.Criar.Request;
using ListaDeTarefas.Application.Usuarios.Commands.Criar.Response;
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

        public async Task<CriarUsuarioResponse> Handle(CriarUsuarioRequest request)
        {
            request.Validar();
            if (!request.IsValid)
            {
                return new CriarUsuarioResponse(request.Notifications)
                {
                    StatusCode = HttpStatusCode.BadRequest
                };
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

                return new CriarUsuarioResponse(request.Notifications)
                {
                    Mensagem = $"Usuario {usuario.Login} criado com sucesso!",
                    StatusCode = HttpStatusCode.OK
                };
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
