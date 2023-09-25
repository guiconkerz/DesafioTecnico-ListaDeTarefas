using ListaDeTarefas.Application.Interfaces.Tarefas;
using ListaDeTarefas.Application.Interfaces.UnitOfWork;
using ListaDeTarefas.Application.Tarefas.Commands.AlterarDescricao.Response;
using ListaDeTarefas.Application.Tarefas.Commands.AlterarTitulo.Request;
using ListaDeTarefas.Application.Tarefas.Commands.AlterarTitulo.Response;
using ListaDeTarefas.Shared.Interfaces;
using System.Net;

namespace ListaDeTarefas.Application.Tarefas.Commands.AlterarTitulo.Handler
{
    public sealed class AlterarTituloTarefaHandler : IAlterarTituloTarefaHandler
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITarefasRepositorio _tarefaRepositorio;

        public AlterarTituloTarefaHandler(IUnitOfWork unitOfWork, ITarefasRepositorio tarefaRepositorio)
        {
            _unitOfWork = unitOfWork;
            _tarefaRepositorio = tarefaRepositorio;
        }

        public async Task<IResponse> Handle(AlterarTituloTarefaRequest request)
        {
            #region Validações

            request.Validar();
            if (!request.IsValid)
            {
                return new AlterarTituloTarefaResponse(StatusCode: HttpStatusCode.BadRequest,
                                                          Mensagem: "Falha na requisição para criar um usuário.",
                                                          Notifications: request.Notifications);
            }
            #endregion
            try
            {
                #region Buscar tarefa

                var tarefaDB = await _tarefaRepositorio.ObterPorIdAsync(id: request.IdTarefa);
                if (tarefaDB is null)
                {
                    return new AlterarTituloTarefaResponse(StatusCode: HttpStatusCode.BadRequest,
                                                Mensagem: $"Tarefa não encontrada.",
                                                Notifications: request.Notifications);
                }

                #endregion

                #region Alterar e persistir no banco de dados

                tarefaDB.AlterarTitulo(request.Titulo);

                _unitOfWork.BeginTransaction();
                var alterada = await _tarefaRepositorio.AlterarTitulo(tarefaDB);
                _unitOfWork.Commit();

                #endregion


                return new AlterarTituloTarefaResponse(StatusCode: HttpStatusCode.OK,
                                                Mensagem: $"Titulo da tarefa alterada com sucesso!",
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
