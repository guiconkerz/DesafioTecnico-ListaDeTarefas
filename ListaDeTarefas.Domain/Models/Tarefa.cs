using ListaDeTarefas.Shared.Entities;

namespace ListaDeTarefas.Domain.Models
{
    public sealed class Tarefa : Entity
    {
        protected Tarefa() { }
        public Tarefa(string titulo, string descricao, DateTime dataEntrega, bool finalizada, Usuario usuario) => (Titulo, Descricao, DataEntrega, Finalizada, Usuario) = (titulo, descricao, dataEntrega, finalizada, usuario);

        public int TarefaId { get; private set; }
        public string Titulo { get; private set; } = string.Empty;
        public string Descricao { get; private set; } = string.Empty;
        public DateTime DataEntrega { get; private set; }
        public bool Finalizada { get; private set; } 
        public int FkUsuario { get; private set; }
        public Usuario Usuario { get; private set; }

        public void MarcarComoFinalizada()
        {
            if (!Finalizada)
            {
                Finalizada = true;
            }
        }
        public void MarcarComoEmAndamento()
        {
            if (Finalizada)
            {
                Finalizada = false;
            }
        }

        public bool PrazoTarefaExpirada()
        {
            if (DataEntrega > DateTime.UtcNow)
            {
                return false;
            }
            return true;
        }

    }
}
