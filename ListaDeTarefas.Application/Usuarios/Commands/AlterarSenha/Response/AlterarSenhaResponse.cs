using Flunt.Notifications;
using ListaDeTarefas.Domain.Abstraction;
using System.Net;

namespace ListaDeTarefas.Application.Usuarios.Commands.AlterarSenha.Response
{
    public sealed class AlterarSenhaResponse : IResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Mensagem { get; set; } = string.Empty;
        public IReadOnlyCollection<Notification> Notifications { get; set; } = new List<Notification>();
        public string Data { get => _data.ToString("dd/MM/yyyy HH:mm:ss"); }
        private readonly DateTime _data;

        public AlterarSenhaResponse(HttpStatusCode statusCode, string mensagem, IReadOnlyCollection<Notification> notifications)
        {
            StatusCode = statusCode;
            Mensagem = mensagem;
            Notifications = notifications;
            _data = DateTime.Now;
        }
    }
}
