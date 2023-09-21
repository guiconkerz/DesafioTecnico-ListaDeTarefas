using Flunt.Notifications;

namespace ListaDeTarefas.Application.Usuarios.Commands.Login.Request
{
    public sealed class RefreshTokenRequest : Notifiable<Notification>
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }

    }
}
