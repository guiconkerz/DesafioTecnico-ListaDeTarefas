using Flunt.Notifications;
using ListaDeTarefas.Domain.Abstraction;
using System.Net;

namespace ListaDeTarefas.Application.Usuarios.Commands.Criar.Response
{
    public sealed class CriarUsuarioResponse : IResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Mensagem { get; set; }
        public IReadOnlyCollection<Notification> Notifications { get; private set; } = new List<Notification>();
        public DateTime DataCriacao { get; private set; }


        public CriarUsuarioResponse(IReadOnlyCollection<Notification> notifications)
        {
            Notifications = notifications;
            DataCriacao = DateTime.UtcNow;
        }
        public CriarUsuarioResponse(HttpStatusCode statusCode, string mensagem, IReadOnlyCollection<Notification> notifications)
        {
            StatusCode = statusCode;
            Mensagem = mensagem;
            Notifications = notifications;
            DataCriacao = DateTime.UtcNow;
        }
    }
}
