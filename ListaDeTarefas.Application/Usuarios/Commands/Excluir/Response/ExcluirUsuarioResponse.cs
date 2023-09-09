using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ListaDeTarefas.Application.Usuarios.Commands.Excluir.Response
{
    public sealed class ExcluirUsuarioResponse
    {
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
