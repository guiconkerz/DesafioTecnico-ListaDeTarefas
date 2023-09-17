using Flunt.Notifications;
using Flunt.Validations;
using ListaDeTarefas.Domain.Abstraction;

namespace ListaDeTarefas.Application.Usuarios.Commands.AlterarSenha.Request
{
    public sealed class AlterarSenhaRequest : Notifiable<Notification>, IRequest
    {
        public int Id { get; set; }
        public string NovaSenha { get; set; }

        public AlterarSenhaRequest(int id, string novaSenha)
        {
            Id = id;
            NovaSenha = novaSenha;
        }

        public void Validar()
        {
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNullOrWhiteSpace(NovaSenha, "AlterarSenhaRequest.NovaSenha", "Nova Senha inválida.")
                .IsLowerThan(NovaSenha, 64, "AlterarSenhaRequest.NovaSenha", "Senha deve ser menor que 64 caracteres.")
                .IsGreaterOrEqualsThan(NovaSenha, 8, "AlterarSenhaRequest.NovaSenha", "Senha deve conter pelo menos 8 dígitos.")
                .Matches(NovaSenha, @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", "AlterarSenhaRequest.NovaSenha", "A senha deve conter ao menos 8 dígitos, 1 letra, 1 caractere especial, 1 letra minuscula e 1 letra maiuscula.")
                );
        }
    }
}
