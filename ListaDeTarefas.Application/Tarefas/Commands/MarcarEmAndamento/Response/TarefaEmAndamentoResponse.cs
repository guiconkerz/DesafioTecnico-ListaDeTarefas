using Flunt.Notifications;
using ListaDeTarefas.Domain.Abstraction;
using System.Net;

namespace ListaDeTarefas.Application.Tarefas.Commands.MarcarEmAndamento.Response
{
    public sealed class TarefaEmAndamentoResponse : IResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Mensagem { get; set; }
        public IReadOnlyCollection<Notification> Notifications { get; private set; } = new List<Notification>();
        public string Data { get => _data.ToString("dd/MM/yyyy HH:mm:ss"); }
        private readonly DateTime _data;
        public TarefaEmAndamentoResponse(HttpStatusCode statusCode, string mensagem, IReadOnlyCollection<Notification> notifications)
        {
            StatusCode = statusCode;
            Mensagem = mensagem;
            Notifications = notifications;
            _data = DateTime.Now;
        }
    }
}
