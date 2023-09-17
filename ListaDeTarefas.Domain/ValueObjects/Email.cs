using ListaDeTarefas.Shared.Extensions;
using ListaDeTarefas.Shared.ValueObjects;

namespace ListaDeTarefas.Domain.ValueObjects
{
    public sealed class Email : ValueObject
    {
        public string Endereco { get; } = string.Empty;
        public string Hash => Endereco.ToBase64();
        public VerificarEmail VerificarEmail { get; private set; } = new VerificarEmail();

        protected Email() { }

        public Email(string endereco)
        {
            Endereco = endereco.Trim();
        }

        public static implicit operator string(Email email) => email.ToString();
        public override string ToString() => Endereco.Trim();
        public static implicit operator Email(string endereco) => new Email(endereco);
        public void ReenviarVerificacao() => VerificarEmail = new VerificarEmail();
    }
}
