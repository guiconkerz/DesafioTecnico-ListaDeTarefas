using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ListaDeTarefas.Domain.Abstraction
{
    public interface IResponse
    {
        public HttpStatusCode StatusCode { get; }
        public IReadOnlyCollection<Notification> Notifications { get; }
        public string Mensagem { get; }
    }
}
