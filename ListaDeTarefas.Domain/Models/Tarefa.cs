using ListaDeTarefas.Shared.Entities;

namespace ListaDeTarefas.Domain.Models
{
    public sealed class Tarefa : Entity
    {
        protected Tarefa() { }
        public Tarefa(string titulo, string descricao, DateTime dataEntrega, bool finalizada)
        {
            Titulo = titulo;
            Descricao = descricao;
            DataEntrega = dataEntrega;
            Finalizada = finalizada;
        }

        public int TarefaId { get; private set; }
        public string Titulo { get; private set; } = string.Empty;
        public string Descricao { get; private set; } = string.Empty;
        public DateTime DataEntrega { get; private set; }
        public bool Finalizada { get; private set; } 
        public int FkUsuario { get; private set; }
        public Usuario Usuario { get; private set; }

        public void MarcarComoFinalizada()
        {
            if (Finalizada)
            {
                return;
            }
            Finalizada = true;
        }
        public void MarcarComoEmAndamento()
        {
            if (!Finalizada)
            {
                return;
            }
            Finalizada = false;
        }

        public bool PrazoTarefaExpirada()
        {
            if (DataEntrega > DateTime.UtcNow)
            {
                return false;
            }
            return true;
        }

        public void VincularUsuario(Usuario usuario)
        {
            if (usuario is null)
            {
                AddNotification("Tarefa.Usuario", "Usuario inválido");
            }
            Usuario = usuario;
        }

    }
}
