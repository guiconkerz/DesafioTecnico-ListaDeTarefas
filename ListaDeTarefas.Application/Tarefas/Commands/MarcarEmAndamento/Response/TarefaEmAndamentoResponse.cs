using Flunt.Notifications;
using ListaDeTarefas.Shared.Interfaces;
using System.Net;

namespace ListaDeTarefas.Application.Tarefas.Commands.MarcarEmAndamento.Response
{
    public sealed record TarefaEmAndamentoResponse(HttpStatusCode StatusCode, string Mensagem, IReadOnlyCollection<Notification> Notifications) : IResponse
    {
        public string Data { get => DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"); }

    }
}
