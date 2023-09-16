using ListaDeTarefas.Application.Interfaces.Tarefas;
using ListaDeTarefas.Application.Interfaces.UnitOfWork;
using ListaDeTarefas.Application.Interfaces.Usuarios;
using ListaDeTarefas.Application.Tarefas.Commands.Criar.Request;
using ListaDeTarefas.Application.Tarefas.Commands.Criar.Response;
using ListaDeTarefas.Domain.Abstraction;
using ListaDeTarefas.Domain.Models;
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
                return new CriarTarefaResponse(statusCode: HttpStatusCode.BadRequest,
                                            mensagem: "Falha na requisição para criar um usuário.",
                                            notifications: request.Notifications);
            }
            
            
            try
            {
                var usuario = await _usuarioRepositorio.BuscarPorIdAsync(request.IdUsuario);
                if (usuario is null)
                {
                    return new CriarTarefaResponse(statusCode: HttpStatusCode.BadRequest,
                                                mensagem: $"Usuário informado não foi encontrado.",
                                                notifications: request.Notifications);
                }

                var tarefa = new Tarefa(titulo: request.Titulo,
                                        descricao: request.Descricao,
                                        dataEntrega: request.DataEntrega,
                                        finalizada: request.Finalizada,
                                        usuario: usuario);

                _unitOfWork.BeginTransaction();
                await _tarefaRepositorio.AdicionarAsync(tarefa);
                _unitOfWork.Commit();

                return new CriarTarefaResponse(statusCode: HttpStatusCode.OK,
                                            mensagem: $"Tarefa {request.Titulo} criada com sucesso.",
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
