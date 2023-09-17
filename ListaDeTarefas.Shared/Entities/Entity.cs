using Flunt.Notifications;

namespace ListaDeTarefas.Shared.Entities
{
    public class Entity : Notifiable<Notification>, IEquatable<Guid>
    {
        public Guid Id { get; set; }
        protected Entity() => Id = Guid.NewGuid();

        public bool Equals(Guid id) => Id == id;
        public override int GetHashCode() => Id.GetHashCode();
    }
}
