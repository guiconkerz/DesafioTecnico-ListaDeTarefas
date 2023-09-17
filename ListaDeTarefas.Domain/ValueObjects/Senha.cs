using BCrypt.Net;
using ListaDeTarefas.Domain.Services;
using ListaDeTarefas.Shared.ValueObjects;

namespace ListaDeTarefas.Domain.ValueObjects
{
    public sealed class Senha : ValueObject
    {
        public string Password { get; private set; }
        public string CodigoAlteracao { get; } = Guid.NewGuid().ToString("N")[..8].ToUpper();

        protected Senha() { }
        public Senha(string senha)
        {
            Password = senha.Trim().CriptografarSenha();
        }

        public bool Verificar(string senha, string senhaCriptografada)
        {
            return BCrypt.Net.BCrypt.Verify(senha, senhaCriptografada);
        }

    }
}
