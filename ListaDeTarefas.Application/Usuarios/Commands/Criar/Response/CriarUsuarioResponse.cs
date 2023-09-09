using Flunt.Notifications;
using System.Net;

namespace ListaDeTarefas.Application.Usuarios.Commands.Criar.Response
{
    public sealed class CriarUsuarioResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public IReadOnlyCollection<Notification> Notifications { get; private set; } = new List<Notification>();
        public string Mensagem { get; set; }
        public DateTime DataCriacao { get; private set; }


        public CriarUsuarioResponse(IReadOnlyCollection<Notification> notifications)
        {
            Notifications = notifications;
            DataCriacao = DateTime.UtcNow;
        }
    }
}
