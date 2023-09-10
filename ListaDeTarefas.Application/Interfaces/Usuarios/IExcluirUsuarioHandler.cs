using ListaDeTarefas.Application.Usuarios.Commands.Excluir.Request;
using ListaDeTarefas.Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ListaDeTarefas.Application.Interfaces.Usuarios
{
    public interface IExcluirUsuarioHandler
    {
        public HttpStatusCode StatusCode { get; set; }
        Task<IResponse> Handle(ExcluirUsuarioRequest request);
    }
}
