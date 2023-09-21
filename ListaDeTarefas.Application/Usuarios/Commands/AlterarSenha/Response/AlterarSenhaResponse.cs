using Flunt.Notifications;
using ListaDeTarefas.Shared.Interfaces;
using System.Net;

namespace ListaDeTarefas.Application.Usuarios.Commands.AlterarSenha.Response
{
    public sealed record AlterarSenhaResponse(HttpStatusCode StatusCode, string Mensagem, IReadOnlyCollection<Notification> Notifications) : IResponse
    {
        public string Data { get => DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"); }
    }
}
