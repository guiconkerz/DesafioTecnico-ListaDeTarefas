using Flunt.Notifications;
using ListaDeTarefas.Domain.Abstraction;
using System.Net;

namespace ListaDeTarefas.Application.Usuarios.Commands.Excluir.Response
{
    public sealed class ExcluirUsuarioResponse : IResponse
    {
        public ExcluirUsuarioResponse(HttpStatusCode statusCode, string mensagem, IReadOnlyCollection<Notification> notifications)
        {
            StatusCode = statusCode;
            Mensagem = mensagem;
            Notifications = notifications;
            _dataExclusao = DateTime.Now;
        }

        public HttpStatusCode StatusCode { get; private set; }
        public IReadOnlyCollection<Notification> Notifications { get; private set; } = new List<Notification>();
        public string Mensagem { get; private set; }
        public string DataExclusao { get => _dataExclusao.ToString("dd/MM/yyyy HH:mm:ss"); }
        private readonly DateTime _dataExclusao;
    }
}
