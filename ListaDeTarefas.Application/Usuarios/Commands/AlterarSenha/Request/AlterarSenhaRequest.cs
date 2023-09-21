using Flunt.Notifications;
using Flunt.Validations;
using ListaDeTarefas.Shared.Interfaces;

namespace ListaDeTarefas.Application.Usuarios.Commands.AlterarSenha.Request
{
    public sealed class AlterarSenhaRequest : Notifiable<Notification>, IRequest
    {
        public string Email { get; set; }
        public string NovaSenha { get; set; }
        public string CodigoAlteracaoSenha { get; set; }

        public AlterarSenhaRequest() { }
        public AlterarSenhaRequest(string id, string novaSenha, string codigoAlteracaoSenha)
        {
            Email = id;
            NovaSenha = novaSenha;
            CodigoAlteracaoSenha = codigoAlteracaoSenha;
        }

        public void Validar()
        {
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNullOrWhiteSpace(NovaSenha, "AlterarSenhaRequest.NovaSenha", "Nova Senha inválida.")
                .IsLowerThan(NovaSenha, 64, "AlterarSenhaRequest.NovaSenha", "Senha deve ser menor que 64 caracteres.")
                .IsGreaterOrEqualsThan(NovaSenha, 8, "AlterarSenhaRequest.NovaSenha", "Senha deve conter pelo menos 8 dígitos.")
                .Matches(NovaSenha, @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", "AlterarSenhaRequest.NovaSenha", "A senha deve conter ao menos 8 dígitos, 1 letra, 1 caractere especial, 1 letra minuscula e 1 letra maiuscula.")

                .IsNotNullOrWhiteSpace(Email, "AlterarSenhaRequest.Email", "Email inválido.")
                .IsLowerThan(Email, 60, "AlterarSenhaRequest.Email", "Email deve ter no máximo 60 caracteres.")
                .IsEmail(Email, "AlterarSenhaRequest.Email", "Por favor, forneça um Email válido.")

                .IsNotNullOrWhiteSpace(CodigoAlteracaoSenha, "AlterarSenhaRequest.CodigoAlteracaoSenha", "Código inválido.")
                .IsBetween(CodigoAlteracaoSenha.Length, 8, 8, "AlterarSenhaRequest.CodigoAlteracaoSenha", "Por favor, forneça um código válido.")
                );
        }
    }
}
