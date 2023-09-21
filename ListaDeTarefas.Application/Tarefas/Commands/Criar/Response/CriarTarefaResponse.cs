using Flunt.Notifications;
using ListaDeTarefas.Shared.Interfaces;
using System.Net;

namespace ListaDeTarefas.Application.Tarefas.Commands.Criar.Response
{
    public record CriarTarefaResponse(HttpStatusCode StatusCode, string Mensagem, IReadOnlyCollection<Notification> Notifications) : IResponse;
}
