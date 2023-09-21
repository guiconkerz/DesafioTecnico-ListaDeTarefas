using Flunt.Notifications;
using Flunt.Validations;
using ListaDeTarefas.Shared.Interfaces;

namespace ListaDeTarefas.Application.Usuarios.Commands.Login.Request
{
    public sealed class LogarRequest : Notifiable<Notification>, IRequest
    {
        public LogarRequest(string email, string senha)
        {
            Email = email.Trim();
            Senha = senha.Trim();
        }

        public string Email { get; }
        public string Senha { get; }

        public void Validar()
        {
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNullOrWhiteSpace(Senha, "CriarUsuarioRequest.Senha", "Senha inválida.")
                .IsGreaterOrEqualsThan(Senha, 8, "CriarUsuarioRequest.Senha", "Senha deve conter pelo menos 8 dígitos.")
                .IsLowerThan(Senha, 64, "CriarUsuarioRequest.Senha", "Senha deve ser menor que 64 caracteres.")
                .Matches(Senha, @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", "CriarUsuarioRequest.Senha", "A senha deve conter ao menos 8 dígitos, 1 letra, 1 caractere especial, 1 letra minuscula e 1 letra maiuscula.")
                .IsNotNullOrWhiteSpace(Email, "CriarUsuarioRequest.Email", "Email inválido.")
                .IsGreaterThan(Email, 3, "LogarRequest.Email", "E-mail informado é inválido.")
                .IsEmail(Email, "CriarUsuarioRequest.Email", "Por favor, forneça um E-mail válido.")
                );
        }
    }
}
