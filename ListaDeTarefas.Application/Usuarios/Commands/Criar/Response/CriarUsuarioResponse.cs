using Flunt.Notifications;
using ListaDeTarefas.Domain.Abstraction;
using System.Net;
using System;

namespace ListaDeTarefas.Application.Usuarios.Commands.Criar.Response
{
    public sealed class CriarUsuarioResponse : IResponse
    {
        public CriarUsuarioResponse(HttpStatusCode statusCode, string mensagem, IReadOnlyCollection<Notification> notifications)
        {
            StatusCode = statusCode;
            Mensagem = mensagem;
            Notifications = notifications;
            _dataCriacao = DateTime.Now;
        }
        public HttpStatusCode StatusCode { get; set; }
        public string Mensagem { get; set; }
        public IReadOnlyCollection<Notification> Notifications { get; private set; } = new List<Notification>();
        public string DataCriacao { get => _dataCriacao.ToString("dd/MM/yyyy HH:mm:ss"); }
        private readonly DateTime _dataCriacao;
    }
}
