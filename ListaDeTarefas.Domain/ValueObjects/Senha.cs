using ListaDeTarefas.Domain.Services;
using ListaDeTarefas.Shared.ValueObjects;

namespace ListaDeTarefas.Domain.ValueObjects
{
    public sealed class Senha : ValueObject
    {
        public string Password { get; private set; }
        public string CodigoAlteracao { get; private set; } 
        public DateTime? DataExpiracao { get; private set; }
        public DateTime? DataVerificacao { get; private set; } = null;
        public bool Ativo => DataVerificacao != null && DataExpiracao == null;

        protected Senha() { }
        public Senha(string senha)
        {
            Password = senha.Trim().CriptografarSenha();
        }

        public bool Verificar(string senha, string senhaCriptografada)
        {
            return BCrypt.Net.BCrypt.Verify(senha, senhaCriptografada);
        }

        public bool Verificar(string senha)
        {
            return BCrypt.Net.BCrypt.Verify(senha, Password);
        }

        public void GerarCodigoAlteracao()
        {
            CodigoAlteracao = Guid.NewGuid().ToString("N")[..8].ToUpper();
            DataExpiracao = DateTime.Now.AddMinutes(5);
            DataVerificacao = null;
        }

        public string VerificarCodigo(string codigo)
        {
            if (CodigoAlteracao is null && DataVerificacao != null)
            {
                return "Este código já foi verificado.";
            }

            if (string.IsNullOrEmpty(codigo) || string.IsNullOrWhiteSpace(codigo))
            {
                return "Código informado é inválido.";
            }
            if (codigo.Trim() != CodigoAlteracao.Trim())
            {
                return "Código de alteração de senha inválido.";
            }
            if (DataExpiracao < DateTime.Now)
            {
                return "Este código já expirou.";
            }
            return string.Empty;
        }

        public void AlterarSenha(string senha)
        {
            if (senha is null)
            {
                return;
            }
            if (string.IsNullOrEmpty(senha.Trim()))
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(senha.Trim()))
            {
                return;
            }
            Password = senha.Trim().CriptografarSenha();
        }

    }
}
