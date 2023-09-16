using Flunt.Notifications;
using Flunt.Validations;
using ListaDeTarefas.Domain.Abstraction;

namespace ListaDeTarefas.Application.Tarefas.Commands.Criar.Request
{
    public sealed class CriarTarefaRequest : Notifiable<Notification>, IRequest
    {
        public CriarTarefaRequest(string titulo, string descricao, DateTime dataEntrega, int idUsuario, bool finalizada)
        {
            Titulo = titulo;
            Descricao = descricao;
            DataEntrega = dataEntrega;
            IdUsuario = idUsuario;
            Finalizada = finalizada;
        }

        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public DateTime DataEntrega { get; private set; }
        public int IdUsuario { get; private set; }
        public bool Finalizada { get; private set; }

        public void Validar()
        {
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNullOrWhiteSpace(Titulo, "CriarTarefaRequest.Titulo", "Título inválido.")
                .IsNotNullOrWhiteSpace(Descricao, "CriarTarefaRequest.Descricao", "Descricao inválido.")
                .IsNotNull(IdUsuario, "CriarTarefaRequest.Usuario", "Usuario inválido.")
                .IsFalse(DataEntrega.Day < DateTime.Now.Day, "CriarTarefaRequest.DataEntrega", "Não é possível criar uma tarefa com prazo de entrega inferior ao dia atual.")
                );
        }
    }
}
