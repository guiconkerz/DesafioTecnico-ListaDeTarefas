using ListaDeTarefas.Application.Interfaces.Tarefas;
using ListaDeTarefas.Application.Interfaces.UnitOfWork;
using ListaDeTarefas.Application.Interfaces.Usuarios;
using ListaDeTarefas.Application.Tarefas.Commands.Excluir.Request;
using ListaDeTarefas.Application.Usuarios.Commands.Excluir.Response;
using ListaDeTarefas.Shared.Interfaces;
using System.Net;

namespace ListaDeTarefas.Application.Tarefas.Commands.Excluir.Handler
{
    public sealed class ExcluirTarefaHandler : IExcluirTarefaHandler
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITarefasRepositorio _tarefaRepositorio;

        public ExcluirTarefaHandler(IUnitOfWork unitOfWork, ITarefasRepositorio tarefaRepositorio)
        {
            _unitOfWork = unitOfWork;
            _tarefaRepositorio = tarefaRepositorio;
        }

        public async Task<IResponse> Handle(ExcluirTarefaRequest request)
        {
            request.Validar();
            if (!request.IsValid)
            {
                return new ExcluirUsuarioResponse(StatusCode: HttpStatusCode.BadRequest,
                                                  Mensagem: "Falha na requisição para excluir uma tarefa.",
                                                  Notifications: request.Notifications);
            }

            try
            {
                _unitOfWork.BeginTransaction();
                var removido = await _tarefaRepositorio.RemoverAsync(request.IdTarefa);
                if (removido == false)
                {
                    return new ExcluirUsuarioResponse(StatusCode: HttpStatusCode.InternalServerError,
                                                      Mensagem: $"Falha ao excluir tarefa. Por favor, tente novamente mais tarde.",
                                                      Notifications: request.Notifications);
                }
                _unitOfWork.Commit();

                return new ExcluirUsuarioResponse(StatusCode: HttpStatusCode.OK,
                                                  Mensagem: $"Tarefa excluída com sucesso.",
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
