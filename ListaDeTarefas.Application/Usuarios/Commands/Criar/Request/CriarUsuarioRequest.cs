using Flunt.Notifications;
using Flunt.Validations;
using ListaDeTarefas.Domain.Abstraction;

namespace ListaDeTarefas.Application.Usuarios.Commands.Criar.Request
{
    public sealed class CriarUsuarioRequest : Notifiable<Notification>, IRequest
    {
        public CriarUsuarioRequest(string login, string senha, string email)
        {
            Login = login;
            Senha = senha;
            Email = email;
        }

        public string Login { get; private set; }
        public string Senha { get; private set; }
        public string Email { get; private set; }

        public void Validar()
        {
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNullOrWhiteSpace(Login, "CriarUsuarioRequest.Login", "Login inválido.")
                .IsNotNullOrWhiteSpace(Senha, "CriarUsuarioRequest.Senha", "Senha inválida.")
                .IsGreaterOrEqualsThan(Senha, 8, "CriarUsuarioRequest.Senha", "Senha deve conter pelo menos 8 dígitos.")
                .Matches(Senha, @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", "CriarUsuarioRequest.Senha", "A senha deve conter ao menos 8 dígitos, 1 letra, 1 caractere especial, 1 letra minuscula e 1 letra maiuscula.")
                .IsNotNullOrWhiteSpace(Email, "CriarUsuarioRequest.Email", "Email inválido.")
                .IsEmail(Email, "CriarUsuarioRequest.Email", "Por favor, forneça um E-mail válido.")
                );
        }
    }
}
