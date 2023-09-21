using ListaDeTarefas.Application.Interfaces.Tarefas;
using ListaDeTarefas.Application.Interfaces.UnitOfWork;
using ListaDeTarefas.Application.Tarefas.Commands.MarcarFinalizada.Request;
using ListaDeTarefas.Application.Tarefas.Commands.MarcarFinalizada.Response;
using ListaDeTarefas.Shared.Interfaces;
using System.Net;

namespace ListaDeTarefas.Application.Tarefas.Commands.MarcarFinalizada.Handler
{
    public sealed class FinalizarTarefaHandler : IFinalizarTarefaHandler
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITarefasRepositorio _tarefaRepositorio;

        public FinalizarTarefaHandler(IUnitOfWork unitOfWork, ITarefasRepositorio tarefaRepositorio)
        {
            _unitOfWork = unitOfWork;
            _tarefaRepositorio = tarefaRepositorio;
        }
        public async Task<IResponse> Handle(FinalizarTarefaRequest request)
        {
            request.Validar();
            if (!request.IsValid)
            {
                return new FinalizarTarefaResponse(StatusCode: HttpStatusCode.BadRequest,
                                            Mensagem: "Falha na requisição para criar um usuário.",
                                            Notifications: request.Notifications);
            }

            try
            {
                var tarefa = await _tarefaRepositorio.ObterPorIdAsync(request.IdTarefa);
                if (tarefa is null)
                {
                    return new FinalizarTarefaResponse(StatusCode: HttpStatusCode.BadRequest,
                                                Mensagem: $"Tarefa informada não foi encontrada.",
                                                Notifications: request.Notifications);
                }

                tarefa.MarcarComoFinalizada();

                _unitOfWork.BeginTransaction();
                await _tarefaRepositorio.AlterarStatus(tarefa);
                _unitOfWork.Commit();

                return new FinalizarTarefaResponse(StatusCode: HttpStatusCode.OK,
                                            Mensagem: $"Status da tarefa {request.IdTarefa} alterado para Finalizada.",
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
