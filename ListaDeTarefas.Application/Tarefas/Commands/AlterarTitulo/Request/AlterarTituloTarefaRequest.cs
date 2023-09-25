using Flunt.Notifications;
using Flunt.Validations;
using ListaDeTarefas.Shared.Interfaces;

namespace ListaDeTarefas.Application.Tarefas.Commands.AlterarTitulo.Request
{
    public class AlterarTituloTarefaRequest : Notifiable<Notification>, IRequest
    {
        public int IdTarefa { get; set; }
        public string Titulo { get; set; }
        public void Validar()
        {
            AddNotifications(new Contract<Notification>()
               .Requires()
               .IsNotNull(IdTarefa, "AlterarDescricaoTarefaRequest.IdTarefa", "Id da tarefa inválido.")
               .IsNotNullOrWhiteSpace(Titulo, "AlterarDescricaoTarefaRequest.Titulo", "Titulo inválido.")
               .IsLowerThan(Titulo, 30, "AlterarDescricaoTarefaRequest.Titulo", "Titulo da tarefa deve ser menor que 30 caracteres.")
               .IsGreaterOrEqualsThan(Titulo, 3, "AlterarDescricaoTarefaRequest.Titulo", "Descrição da tarefa deve ser maior que 3 caracteres")
               );
        }
    }
}
