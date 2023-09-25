using Flunt.Notifications;
using ListaDeTarefas.Shared.Interfaces;
using System.Net;

namespace ListaDeTarefas.Application.Tarefas.Commands.AlterarDescricao.Response
{
    public record AlterarDescricaoTarefaResponse(HttpStatusCode StatusCode, string Mensagem, IReadOnlyCollection<Notification> Notifications) : IResponse;
}
