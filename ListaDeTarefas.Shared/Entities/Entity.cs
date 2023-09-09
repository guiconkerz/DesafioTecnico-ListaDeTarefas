using Flunt.Notifications;

namespace ListaDeTarefas.Shared.Entities
{
    public class Entity : Notifiable<Notification>
    {
        public Guid Id { get; set; }
        protected Entity() => Id = Guid.NewGuid();
    }
}
