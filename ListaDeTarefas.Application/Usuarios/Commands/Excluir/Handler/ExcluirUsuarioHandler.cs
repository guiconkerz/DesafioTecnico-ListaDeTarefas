using ListaDeTarefas.Application.Interfaces.UnitOfWork;
using ListaDeTarefas.Application.Interfaces.Usuarios;
using ListaDeTarefas.Application.Interfaces.Usuarios.Handler;
using ListaDeTarefas.Application.Usuarios.Commands.Excluir.Request;
using ListaDeTarefas.Application.Usuarios.Commands.Excluir.Response;
using ListaDeTarefas.Shared.Interfaces;
using System.Net;

namespace ListaDeTarefas.Application.Usuarios.Commands.Excluir.Handler
{
    public class ExcluirUsuarioHandler : IExcluirUsuarioHandler
    {
        public ExcluirUsuarioHandler(IUnitOfWork unitOfWork, IUsuarioRepositorio usuarioRepositorio)
        {
            _unitOfWork = unitOfWork;
            _usuarioRepositorio = usuarioRepositorio;
        }
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public async Task<IResponse> Handle(ExcluirUsuarioRequest request)
        {
            request.Validar();
            if (!request.IsValid)
            {
                return new ExcluirUsuarioResponse(StatusCode: HttpStatusCode.BadRequest,
                                                  Mensagem: "Falha na requisição para excluir um usuário.",
                                                  Notifications: request.Notifications);
            }

            try
            {
                _unitOfWork.BeginTransaction();
                var removido = await _usuarioRepositorio.RemoverAsync(request.Id);
                if (removido == false)
                {
                    return new ExcluirUsuarioResponse(StatusCode: HttpStatusCode.BadRequest,
                                                      Mensagem: $"Nenhum usuário com Id {request.Id} cadastrado.",
                                                      Notifications: request.Notifications);
                }
                _unitOfWork.Commit();

                return new ExcluirUsuarioResponse(StatusCode: HttpStatusCode.OK,
                                                  Mensagem: $"Usuario com Id {request.Id} excluído com sucesso.",
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
