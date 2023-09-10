using Flunt.Notifications;
using Flunt.Validations;
using ListaDeTarefas.Domain.Abstraction;

namespace ListaDeTarefas.Application.Usuarios.Commands.Excluir.Request
{
    public sealed class ExcluirUsuarioRequest : Notifiable<Notification>, IRequest
    {
        public int Id { get; set; }

        public ExcluirUsuarioRequest(int id)
        {
            Id = id;
        }

        public void Validar()
        {
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNull(Id, "ExcluirUsuarioRequest.Id", "Id inválido.")
                );
        }
    }
}
