using ListaDeTarefas.Application.Interfaces.Tarefas;
using ListaDeTarefas.Application.Interfaces.UnitOfWork;
using ListaDeTarefas.Application.Interfaces.Usuarios;
using ListaDeTarefas.Application.Tarefas.Commands.Criar.Request;
using ListaDeTarefas.Application.Tarefas.Commands.Criar.Response;
using ListaDeTarefas.Domain.Models;
using ListaDeTarefas.Shared.Interfaces;
using System.Net;

namespace ListaDeTarefas.Application.Tarefas.Commands.Criar.Handler
{
    public sealed class CriarTarefaHandler : ICriarTarefaHandler
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITarefasRepositorio _tarefaRepositorio;
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public CriarTarefaHandler(IUnitOfWork unitOfWork, ITarefasRepositorio tarefaRepositorio, IUsuarioRepositorio usuarioRepositorio)
        {
            _unitOfWork = unitOfWork;
            _tarefaRepositorio = tarefaRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
        }

        public async Task<IResponse> Handle(CriarTarefaRequest request)
        {
            request.Validar();
            if (!request.IsValid)
            {
                return new CriarTarefaResponse(StatusCode: HttpStatusCode.BadRequest,
                                            Mensagem: "Falha na requisição para criar um usuário.",
                                            Notifications: request.Notifications);
            }


            try
            {
                var usuarioDB = await _usuarioRepositorio.BuscarPorIdAsync(request.IdUsuario);
                if (usuarioDB is null)
                {
                    return new CriarTarefaResponse(StatusCode: HttpStatusCode.BadRequest,
                                                Mensagem: $"Usuário informado não foi encontrado.",
                                                Notifications: request.Notifications);
                }

                var tarefa = new Tarefa(titulo: request.Titulo,
                                        descricao: request.Descricao,
                                        dataEntrega: request.DataEntrega,
                                        finalizada: false,
                                        usuario: usuarioDB);


                usuarioDB.AdicionarTarefa(tarefa);

                _unitOfWork.BeginTransaction();
                await _tarefaRepositorio.AdicionarAsync(tarefa);
                _unitOfWork.Commit();

                return new CriarTarefaResponse(StatusCode: HttpStatusCode.OK,
                                            Mensagem: $"Tarefa {request.Titulo} criada com sucesso.",
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
