using ListaDeTarefas.Shared.Exceptions;
using ListaDeTarefas.Shared.Results;
using ListaDeTarefas.Shared.ValueObjects;

namespace ListaDeTarefas.Domain.ValueObjects
{
    public class VerificarEmail : ValueObject
    {
        public VerificarEmail() { }
        public VerificarEmail(string codigo, DateTime dataExpiracao)
        {
            Codigo = codigo;
            DataExpiracao = dataExpiracao;
        }

        public VerificarEmail(string codigo, DateTime? dataExpiracao = null, DateTime? dataVerificacao = null)
        {
            Codigo = codigo;
            DataExpiracao = dataExpiracao;
            DataVerificacao = dataVerificacao;
        }
        
        
        public string? Codigo { get; private set; }
        public DateTime? DataExpiracao { get; private set; } 
        public DateTime? DataVerificacao { get; private set; } = null;
        public bool Ativo => DataVerificacao != null && DataExpiracao == null;

        public void GerarCodigoAlteracao()
        {
            Codigo = Guid.NewGuid().ToString(format: "N")[0..6].ToUpper();
            DataExpiracao = DateTime.Now.AddMinutes(5);
            DataVerificacao = null;
        }

        public VerificacaoCodigoResult VerificarCodigo(string codigo)
        {
            DadosInvalidosException.ThrowIfNull(codigo, "Código de ativação informado não confere com o que foi enviado. Por favor, verifique.");

            if (Codigo is null && DataVerificacao != null)
            {
                return new VerificacaoCodigoResult
                {
                    CodigoValido = false,
                    Mensagem = "Essa conta já foi ativada."
                };
            }

            if (Ativo)
            {
                return new VerificacaoCodigoResult
                {
                    CodigoValido = false,
                    Mensagem = "Este código já foi utilizado."
                };
            }

            if (DataExpiracao < DateTime.Now || DataExpiracao is null)
            {
                return new VerificacaoCodigoResult
                {
                    CodigoValido = false,
                    Mensagem = "Este código já expirou."
                };
            }

            return new VerificacaoCodigoResult
            {
                CodigoValido = true,
                Mensagem = string.Empty
            };
        }

        public void AtivarConta()
        {
            DataExpiracao = null;
            Codigo = null;
            DataVerificacao = DateTime.Now;
        }

    }
}
