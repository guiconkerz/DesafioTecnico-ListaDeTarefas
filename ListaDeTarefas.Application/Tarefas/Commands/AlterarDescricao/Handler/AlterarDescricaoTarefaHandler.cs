using ListaDeTarefas.Application.Interfaces.Tarefas;
using ListaDeTarefas.Application.Interfaces.UnitOfWork;
using ListaDeTarefas.Application.Tarefas.Commands.AlterarDescricao.Request;
using ListaDeTarefas.Application.Tarefas.Commands.AlterarDescricao.Response;
using ListaDeTarefas.Shared.Interfaces;
using System.Net;

namespace ListaDeTarefas.Application.Tarefas.Commands.AlterarDescricao.Handler
{
    public sealed class AlterarDescricaoTarefaHandler : IAlterarDescricaoTarefaHandler
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITarefasRepositorio _tarefaRepositorio;

        public AlterarDescricaoTarefaHandler(IUnitOfWork unitOfWork, ITarefasRepositorio tarefaRepositorio)
        {
            _unitOfWork = unitOfWork;
            _tarefaRepositorio = tarefaRepositorio;
        }

        public async Task<IResponse> Handle(AlterarDescricaoTarefaRequest request)
        {
            #region Validações

            request.Validar();
            if (!request.IsValid)
            {
                return new AlterarDescricaoTarefaResponse(StatusCode: HttpStatusCode.BadRequest,
                                                          Mensagem: "Falha na requisição para alterar a descricao.",
                                                          Notifications: request.Notifications);
            }
            #endregion
            try
            {
                #region Buscar tarefa

                var tarefaDB = await _tarefaRepositorio.ObterPorIdAsync(id: request.IdTarefa);
                if (tarefaDB is null)
                {
                    return new AlterarDescricaoTarefaResponse(StatusCode: HttpStatusCode.BadRequest,
                                                Mensagem: $"Tarefa não encontrada.",
                                                Notifications: request.Notifications);
                }

                #endregion

                #region Alterar e persistir no banco de dados

                tarefaDB.AlterarDescricao(request.Descricao);

                _unitOfWork.BeginTransaction();
                var alterada = await _tarefaRepositorio.AlterarDescricao(tarefaDB);
                _unitOfWork.Commit();

                #endregion


                return new AlterarDescricaoTarefaResponse(StatusCode: HttpStatusCode.OK,
                                                Mensagem: $"Descrição da tarefa alterada com sucesso!",
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
