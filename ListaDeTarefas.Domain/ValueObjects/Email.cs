using ListaDeTarefas.Shared.Extensions;
using ListaDeTarefas.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaDeTarefas.Domain.ValueObjects
{
    public sealed class Email : ValueObject
    {
        public string Endereco { get; private set; } = string.Empty;
        public VerificarEmail VerificarEmail { get; private set; } = new VerificarEmail();
        public string Hash => Endereco.ToBase64();

        protected Email() { }

        public Email(string endereco)
        {
            Endereco = endereco;
        }

        public static implicit operator string(Email email) => email.ToString();
        public static implicit operator Email(string endereco) => new Email(endereco);
        public override string ToString() => Endereco;

        public void ReenviarVerificacao() => VerificarEmail = new VerificarEmail();
    }
}
