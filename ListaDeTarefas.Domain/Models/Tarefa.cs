using ListaDeTarefas.Shared.Entities;
using ListaDeTarefas.Shared.Exceptions;

namespace ListaDeTarefas.Domain.Models
{
    public sealed class Tarefa : Entity
    {
        protected Tarefa() { }
        public Tarefa(string titulo, string descricao, DateTime dataEntrega, bool finalizada, Usuario usuario)
        {
            Titulo = titulo;
            Descricao = descricao;
            DataEntrega = dataEntrega;
            Finalizada = finalizada;
            Usuario = usuario;
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

        public void AlterarTitulo(string titulo)
        {
            DadosInvalidosException.ThrowIfNull(titulo);
            Titulo = titulo;
        }

        public void AlterarDescricao(string descricao)
        {
            DadosInvalidosException.ThrowIfNull(descricao);
            Descricao = descricao;
        }

    }
}
