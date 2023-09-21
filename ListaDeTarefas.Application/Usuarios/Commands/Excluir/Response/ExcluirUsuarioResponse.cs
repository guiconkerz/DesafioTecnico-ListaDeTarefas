using Flunt.Notifications;
using ListaDeTarefas.Shared.Interfaces;
using System.Net;

namespace ListaDeTarefas.Application.Usuarios.Commands.Excluir.Response
{
    public sealed record ExcluirUsuarioResponse(HttpStatusCode StatusCode, string Mensagem, IReadOnlyCollection<Notification> Notifications) : IResponse
    {
        public string DataExclusao { get => DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"); }
    }
}
