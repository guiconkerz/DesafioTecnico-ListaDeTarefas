using Flunt.Notifications;
using ListaDeTarefas.Domain.Abstraction;
using System.Net;

namespace ListaDeTarefas.Application.Usuarios.Commands.Login.Response
{
    public sealed record LogarResponse(HttpStatusCode StatusCode, string Mensagem, IReadOnlyCollection<Notification> Notifications) : IResponse
    {
        public string Data => DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        public string Token { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;
        public string Login { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string[] Perfil { get; set; } = Array.Empty<string>();
    }
}
