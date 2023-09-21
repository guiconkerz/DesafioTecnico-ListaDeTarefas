using ListaDeTarefas.Domain.ValueObjects;
using ListaDeTarefas.Shared.Entities;

namespace ListaDeTarefas.Domain.Models
{
    public sealed class Usuario : Entity
    {
        protected Usuario() { }
        public Usuario(Login login, Senha senha, Email email, Perfil perfil) => (Login, Senha, Email, Perfil) = (login, senha, email, perfil);
        public Usuario(int id) => UsuarioId = id;

        public int UsuarioId { get; private set; }
        public Login Login { get; private set; }
        public Senha Senha { get; private set; }
        public Email Email { get; private set; }
        public ICollection<Tarefa> Tarefas { get; private set; } = new List<Tarefa>();
        public int PerfilId { get; private set; }
        public Perfil Perfil { get; private set; }

        public void AlterarEmail(Email email)
        {
            if (email is null)
            {
                AddNotification("Usuario.Email", "E-mail informado inválido.");
            }
            Email = email;
        }

        public void AdicionarTarefa(Tarefa tarefa)
        {
            if (tarefa is null)
            {
                AddNotification("Usuario.Tarefa", "Tarefa informada inválida.");
            }
            Tarefas.Add(tarefa);
        }

    }
}
