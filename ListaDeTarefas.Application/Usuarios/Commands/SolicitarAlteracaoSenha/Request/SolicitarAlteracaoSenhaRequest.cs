using Flunt.Notifications;
using Flunt.Validations;
using ListaDeTarefas.Shared.Interfaces;

namespace ListaDeTarefas.Application.Usuarios.Commands.SolicitarAlteracaoSenha.Request
{
    public sealed class SolicitarAlteracaoSenhaRequest : Notifiable<Notification>, IRequest
    {
        public string Email { get; set; }
        public void Validar()
        {
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNullOrWhiteSpace(Email, "AlterarSenhaRequest.Email", "Email inválido.")
                .IsLowerThan(Email, 60, "AlterarSenhaRequest.Email", "Email deve ter no máximo 60 caracteres.")
                .IsEmail(Email, "AlterarSenhaRequest.Email", "Por favor, forneça um Email válido.")
                );
        }
    }
}
