using Flunt.Notifications;
using ListaDeTarefas.Shared.Interfaces;
using System.Net;

namespace ListaDeTarefas.Application.Tarefas.Commands.AlterarTitulo.Response
{
    public record AlterarTituloTarefaResponse(HttpStatusCode StatusCode, string Mensagem, IReadOnlyCollection<Notification> Notifications) : IResponse;
}
