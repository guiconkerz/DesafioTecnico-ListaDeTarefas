using ListaDeTarefas.Domain.ValueObjects;
using ListaDeTarefas.Shared.Entities;

namespace ListaDeTarefas.Domain.Models
{
    public sealed class Usuario : Entity
    {
        protected Usuario() { }
        public Usuario(Login login, Senha senha, Email email) => (Login, Senha, Email) = (login, senha, email);

        public int UsuarioId { get; private set; }
        public Login Login { get; private set; }
        public Senha Senha { get; private set; }
        public Email Email { get; private set; }
        public ICollection<Tarefa> Tarefas { get; private set; } = new List<Tarefa>();

       
    }
}
