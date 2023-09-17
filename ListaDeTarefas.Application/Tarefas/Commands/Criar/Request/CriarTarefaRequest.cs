using Flunt.Notifications;
using Flunt.Validations;
using ListaDeTarefas.Domain.Abstraction;

namespace ListaDeTarefas.Application.Tarefas.Commands.Criar.Request
{
    public sealed class CriarTarefaRequest : Notifiable<Notification>, IRequest
    {
        public CriarTarefaRequest(string titulo, string descricao, DateTime dataEntrega, int idUsuario)
        {
            Titulo = titulo;
            Descricao = descricao;
            DataEntrega = dataEntrega;
            IdUsuario = idUsuario;
        }

        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public DateTime DataEntrega { get; private set; }
        public int IdUsuario { get; private set; }

        public void Validar()
        {
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNullOrWhiteSpace(Titulo, "CriarTarefaRequest.Titulo", "Título inválido.")
                .IsLowerThan(Titulo, 30, "CriarTarefaRequest.Titulo", "Título da tarefa deve ser menor que 30 caracteres.")
                .IsNotNullOrWhiteSpace(Descricao, "CriarTarefaRequest.Descricao", "Descricao inválido.")
                .IsLowerThan(Descricao, 300, "CriarTarefaRequest.Descricao", "Descricao da tarefa deve ser menor que 300 caracteres.")
                .IsNotNull(IdUsuario, "CriarTarefaRequest.Usuario", "Usuario inválido.")
                .IsFalse(DataEntrega.Day < DateTime.Now.Day, "CriarTarefaRequest.DataEntrega", "Não é possível criar uma tarefa com prazo de entrega inferior ao dia atual.")
                );
        }
    }
}
