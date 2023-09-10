using Flunt.Notifications;
using ListaDeTarefas.Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ListaDeTarefas.Application.Usuarios.Commands.Excluir.Response
{
    public sealed class ExcluirUsuarioResponse : IResponse
    {
        public ExcluirUsuarioResponse(HttpStatusCode statusCode, string mensagem, IReadOnlyCollection<Notification> notifications)
        {
            StatusCode = statusCode;
            Mensagem = mensagem;
            Notifications = notifications;
        }
        public ExcluirUsuarioResponse(IReadOnlyCollection<Notification> notifications)
        {
            Notifications = notifications;
            DataExclusao = DateTime.UtcNow;
            //OBS: Falta criar ExcluirUsuarioResponseHandler
        }

        public HttpStatusCode StatusCode { get; set; }
        public IReadOnlyCollection<Notification> Notifications { get; private set; } = new List<Notification>();
        public string Mensagem { get; set; }
        public DateTime DataExclusao { get; private set; }
    }
}
