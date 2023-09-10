using Flunt.Notifications;
using Flunt.Validations;
using ListaDeTarefas.Domain.Abstraction;
using ListaDeTarefas.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaDeTarefas.Application.Tarefas.Commands.Criar.Request
{
    public sealed class CriarTarefaRequest : Notifiable<Notification>, IRequest
    {
        public CriarTarefaRequest(string titulo, string descricao, DateTime dataEntrega, string usuario, bool finalizada)
        {
            Titulo = titulo;
            Descricao = descricao;
            DataEntrega = dataEntrega;
            Usuario = usuario;
            Finalizada = finalizada;
        }

        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public DateTime DataEntrega { get; private set; }
        public string Usuario { get; private set; }
        public bool Finalizada { get; private set; }

        public void Validar()
        {
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNullOrWhiteSpace(Titulo, "CriarTarefaRequest.Titulo", "Título inválido.")
                .IsNotNullOrWhiteSpace(Descricao, "CriarTarefaRequest.Descricao", "Descricao inválido.")
                .IsNotNullOrWhiteSpace(Usuario, "CriarTarefaRequest.Usuario", "Usuario inválido.")
                .IsTrue(DataEntrega < DateTime.Now, "CriarTarefaRequest.DataEntrega", "Não é possível criar uma tarefa com prazo inferior ao dia atual.")
                );
        }
    }
}
