using Flunt.Notifications;
using ListaDeTarefas.Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ListaDeTarefas.Application.Tarefas.Commands.Criar.Response
{
    public sealed class CriarTarefaResponse : IResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Mensagem { get; set; }
        public IReadOnlyCollection<Notification> Notifications { get; private set; } = new List<Notification>();
        public string DataCriacao { get => _dataCriacao.ToString("dd/MM/yyyy HH:mm:ss"); }
        private readonly DateTime _dataCriacao;

        public CriarTarefaResponse(HttpStatusCode statusCode, string mensagem, IReadOnlyCollection<Notification> notifications)
        {
            StatusCode = statusCode;
            Mensagem = mensagem;
            Notifications = notifications;
            _dataCriacao = DateTime.Now;
        }
    }
}
