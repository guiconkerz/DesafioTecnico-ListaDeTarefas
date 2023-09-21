using Flunt.Notifications;
using ListaDeTarefas.Application.Interfaces.Services;
using ListaDeTarefas.Domain.ValueObjects;
using ListaDeTarefas.Shared.Interfaces;
using System.Net;

namespace ListaDeTarefas.Application.Usuarios.Commands.Login.Response
{
    public sealed class LogarResponse : IResponse
    {
        public LogarResponse(HttpStatusCode statusCode, string mensagem, string id, string login, string email, string perfil)
        {
            StatusCode = statusCode;
            Mensagem = mensagem;
            Id = id;
            Login = login;
            Email = email;
            Perfil = perfil;
        }
        public LogarResponse(HttpStatusCode statusCode, string mensagem, IReadOnlyCollection<Notification> notifications)
        {
            StatusCode = statusCode;
            Mensagem = mensagem;
            Notifications = notifications;
        }

        public string Data => DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public string Id { get; private set; }
        public string Login { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string Perfil { get; private set; } = string.Empty;
        public HttpStatusCode StatusCode { get; private set; }
        public string Mensagem { get; private set; }
        public IReadOnlyCollection<Notification> Notifications { get; private set; }

        public string GerarToken(ITokenServices tokenServices)
        {
            return Token = tokenServices.GerarToken(this);
        }
    }
    
}
