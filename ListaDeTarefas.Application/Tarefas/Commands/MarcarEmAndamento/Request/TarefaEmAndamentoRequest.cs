using Flunt.Notifications;
using Flunt.Validations;
using ListaDeTarefas.Shared.Interfaces;

namespace ListaDeTarefas.Application.Tarefas.Commands.MarcarEmAndamento.Request
{
    public class TarefaEmAndamentoRequest : Notifiable<Notification>, IRequest
    {
        public int IdTarefa { get; set; }
        public void Validar()
        {
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNull(IdTarefa, "TarefaEmAndamentoRequest.IdTarefa", "Id inválido.")
                );
        }
    }
}
