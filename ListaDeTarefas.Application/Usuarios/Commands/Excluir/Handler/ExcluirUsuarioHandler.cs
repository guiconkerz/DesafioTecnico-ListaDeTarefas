using Flunt.Notifications;
using ListaDeTarefas.Application.Interfaces.UnitOfWork;
using ListaDeTarefas.Application.Interfaces.Usuarios;
using ListaDeTarefas.Application.Usuarios.Commands.Criar.Response;
using ListaDeTarefas.Application.Usuarios.Commands.Excluir.Request;
using ListaDeTarefas.Application.Usuarios.Commands.Excluir.Response;
using ListaDeTarefas.Domain.Abstraction;
using ListaDeTarefas.Domain.Models;
using ListaDeTarefas.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ListaDeTarefas.Application.Usuarios.Commands.Excluir.Handler
{
    public class ExcluirUsuarioHandler : IExcluirUsuarioHandler
    {
        public ExcluirUsuarioHandler(IUnitOfWork unitOfWork, IUsuarioRepositorio usuarioRepositorio)
        {
            _unitOfWork = unitOfWork;
            _usuarioRepositorio = usuarioRepositorio;
        }

        public HttpStatusCode StatusCode { get; set; }
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public async Task<IResponse> Handle(ExcluirUsuarioRequest request)
        {
            request.Validar();
            if (!request.IsValid)
            {
                return new ExcluirUsuarioResponse(statusCode: HttpStatusCode.BadRequest,
                                                  mensagem: "Falha na requisição para excluir um usuário.",
                                                  request.Notifications);
            }

            try
            {
                _unitOfWork.BeginTransaction();
                var removido = await _usuarioRepositorio.Remover(request.Id);
                if (removido == false)
                {
                    return new ExcluirUsuarioResponse(statusCode: HttpStatusCode.BadRequest,
                                                  mensagem: $"Nenhum usuário com Id {request.Id} cadastrado.",
                                                  request.Notifications);
                }
                _unitOfWork.Commit();

                return new ExcluirUsuarioResponse(statusCode: HttpStatusCode.OK,
                                                  mensagem: $"Usuario com Id {request.Id} excluído com sucesso.",
                                                  request.Notifications);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw new Exception($"{ex.Message}");
            }
            finally
            {
                _unitOfWork.Rollback();
            }
        }
    }
}
