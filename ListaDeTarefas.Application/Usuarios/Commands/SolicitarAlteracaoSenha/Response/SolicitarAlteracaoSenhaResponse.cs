using Flunt.Notifications;
using ListaDeTarefas.Shared.Interfaces;
using System.Net;

namespace ListaDeTarefas.Application.Usuarios.Commands.SolicitarAlteracaoSenha.Response
{
    public sealed record SolicitarAlteracaoSenhaResponse(HttpStatusCode StatusCode, IReadOnlyCollection<Notification> Notifications, string Mensagem) : IResponse
    {
        public string Data { get => DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"); }
    }
}
