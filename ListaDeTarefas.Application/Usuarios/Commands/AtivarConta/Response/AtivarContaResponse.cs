using Flunt.Notifications;
using ListaDeTarefas.Shared.Interfaces;
using System.Net;

namespace ListaDeTarefas.Application.Usuarios.Commands.AtivarConta.Response
{
    public sealed class AtivarContaResponse : IResponse
    {
        public AtivarContaResponse(HttpStatusCode statusCode, string mensagem)
        {
            StatusCode = statusCode;
            Mensagem = mensagem;
            _data = DateTime.Now;
        }
        public AtivarContaResponse(HttpStatusCode statusCode, string mensagem, IReadOnlyCollection<Notification> notificacoes)
        {
            StatusCode = statusCode;
            Mensagem = mensagem;
            Notifications = notificacoes;
            _data = DateTime.Now;
        }

        public HttpStatusCode StatusCode { get; set; }
        public string Mensagem { get; set; } = string.Empty;
        public IReadOnlyCollection<Notification> Notifications { get; set; } = new List<Notification>();
        public string Data { get => _data.ToString("dd/MM/yyyy HH:mm:ss"); }
        private readonly DateTime _data;
    }
}
