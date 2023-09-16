using ListaDeTarefas.Application.Interfaces.Tarefas;
using ListaDeTarefas.Application.Interfaces.UnitOfWork;
using ListaDeTarefas.Application.Tarefas.Commands.MarcarEmAndamento.Response;
using ListaDeTarefas.Application.Tarefas.Commands.MarcarFinalizada.Request;
using ListaDeTarefas.Application.Tarefas.Commands.MarcarFinalizada.Response;
using ListaDeTarefas.Domain.Abstraction;
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
                return new FinalizarTarefaResponse(statusCode: HttpStatusCode.BadRequest,
                                            mensagem: "Falha na requisição para criar um usuário.",
                                            notifications: request.Notifications);
            }

            try
            {
                var tarefa = await _tarefaRepositorio.ObterPorIdAsync(request.IdTarefa);
                if (tarefa is null)
                {
                    return new FinalizarTarefaResponse(statusCode: HttpStatusCode.BadRequest,
                                                mensagem: $"Tarefa informada não foi encontrada.",
                                                notifications: request.Notifications);
                }

                tarefa.MarcarComoFinalizada();

                _unitOfWork.BeginTransaction();
                await _tarefaRepositorio.AlterarStatus(tarefa);
                _unitOfWork.Commit();

                return new FinalizarTarefaResponse(statusCode: HttpStatusCode.OK,
                                            mensagem: $"Status da tarefa {request.IdTarefa} alterado para Finalizada.",
                                            notifications: request.Notifications);
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
