using Flunt.Notifications;
using ListaDeTarefas.Domain.Abstraction;
using System.Net;

namespace ListaDeTarefas.Application.Usuarios.Commands.Criar.Response
{
    public sealed record CriarUsuarioResponse(HttpStatusCode StatusCode, string Mensagem, IReadOnlyCollection<Notification> Notifications) : IResponse
    {
        public string DataCriacao { get => DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"); }
    }
}
