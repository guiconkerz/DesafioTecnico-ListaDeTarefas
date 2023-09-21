using Flunt.Notifications;
using Flunt.Validations;
using ListaDeTarefas.Shared.Interfaces;

namespace ListaDeTarefas.Application.Tarefas.Commands.MarcarFinalizada.Request
{
    public sealed class FinalizarTarefaRequest : Notifiable<Notification>, IRequest
    {
        public int IdTarefa { get; set; }

        public FinalizarTarefaRequest(int idTarefa)
        {
            IdTarefa = idTarefa;
        }

        public void Validar()
        {
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNull(IdTarefa, "TarefaEmAndamentoRequest.IdTarefa", "Id inválido.")
                );
        }
    }
}
