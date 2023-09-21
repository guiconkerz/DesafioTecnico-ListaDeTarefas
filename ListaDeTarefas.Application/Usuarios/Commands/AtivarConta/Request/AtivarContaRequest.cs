using Flunt.Notifications;
using Flunt.Validations;
using ListaDeTarefas.Shared.Interfaces;

namespace ListaDeTarefas.Application.Usuarios.Commands.AtivarConta.Request
{
    public sealed class AtivarContaRequest : Notifiable<Notification>, IRequest
    {
        public AtivarContaRequest(string email, string codigoAtivacao)
        {
            Email = email;
            CodigoAtivacao = codigoAtivacao;
        }

        public string Email { get; set; }
        public string CodigoAtivacao { get; set; }

        public void Validar()
        {
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNullOrEmpty(Email, "AtivarContaRequest.Email", "E-mail inválido.")
                .IsNotNullOrWhiteSpace(Email, "AtivarContaRequest.Email", "E-mail inválido.")
                .IsEmail(Email, "AtivarContaRequest.Email", "Por favor, forneça um E-mail válido.")
                .IsNotNullOrEmpty(CodigoAtivacao, "AtivarContaRequest.CodigoAtivacao", "Código de ativação inválido.")
                .IsNotNullOrWhiteSpace(CodigoAtivacao, "AtivarContaRequest.CodigoAtivacao", "Código de ativação inválido.")
                .IsBetween(CodigoAtivacao.Length, 6, 6, "AtivarContaRequest.CodigoAtivacao", "O código de ativação informado é inválido.")
                );
        }
    }
}
