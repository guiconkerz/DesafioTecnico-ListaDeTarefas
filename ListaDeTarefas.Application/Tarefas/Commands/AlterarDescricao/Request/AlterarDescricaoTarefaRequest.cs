using Flunt.Notifications;
using Flunt.Validations;
using ListaDeTarefas.Shared.Interfaces;

namespace ListaDeTarefas.Application.Tarefas.Commands.AlterarDescricao.Request
{
    public class AlterarDescricaoTarefaRequest : Notifiable<Notification>, IRequest
    {
        public int IdTarefa { get; set; }
        public string Descricao { get; set; }
        public void Validar()
        {
            AddNotifications(new Contract<Notification>()
               .Requires()
               .IsNotNull(IdTarefa, "AlterarDescricaoTarefaRequest.IdTarefa", "Id da tarefa inválido.")
               .IsNotNullOrWhiteSpace(Descricao, "AlterarDescricaoTarefaRequest.Descricao", "Descricao inválido.")
               .IsLowerThan(Descricao, 300, "AlterarDescricaoTarefaRequest.Descricao", "Descricao da tarefa deve ser menor que 300 caracteres.")
               .IsGreaterOrEqualsThan(Descricao, 3, "AlterarDescricaoTarefaRequest.Descricao", "Descrição da tarefa deve ser maior que 3 caracteres")
               );
        }
    }
}
