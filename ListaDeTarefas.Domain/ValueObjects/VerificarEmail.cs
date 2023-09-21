using ListaDeTarefas.Shared.ValueObjects;

namespace ListaDeTarefas.Domain.ValueObjects
{
    public class VerificarEmail : ValueObject
    {
        public VerificarEmail(string codigo)
        {
            Codigo = codigo;
        }
        public VerificarEmail() {}
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

        public string VerificarCodigo(string codigo)
        {
            if (Codigo is null && DataVerificacao != null)
            {
                return "Essa conta já foi ativada.";
            }

            if (string.IsNullOrEmpty(codigo.Trim()) != string.IsNullOrEmpty(Codigo.Trim()))
            {
                return "Código de ativação informado não confere com o que foi enviado. Por favor, verifique.";
            }

            if (Ativo)
            {
                return "Este código já foi utilizado.";
            }

            if (DataExpiracao < DateTime.Now || DataExpiracao is null)
            {
                return "Este código já expirou.";
            }

            return string.Empty;
        }

    }
}
