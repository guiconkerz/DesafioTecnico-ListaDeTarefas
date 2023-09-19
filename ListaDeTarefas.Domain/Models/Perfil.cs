using ListaDeTarefas.Shared.Entities;

namespace ListaDeTarefas.Domain.Models
{
    public sealed class Perfil : Entity
    {
        protected Perfil() { }
        public Perfil(string nome)
        {
            Nome = nome;
        }

        public int PerfilId { get; private set; }
        public string Nome { get; private set; }

    }
}
