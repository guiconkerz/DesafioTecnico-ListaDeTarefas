using ListaDeTarefas.Shared.ValueObjects;

namespace ListaDeTarefas.Domain.ValueObjects
{
    public sealed class VerificarEmail : ValueObject
    {
        public string Codigo { get; } = Guid.NewGuid().ToString(format: "N")[0..6].ToUpper();
        public DateTime? DataExpiracao { get; private set; } = DateTime.UtcNow.AddMinutes(5);
        public DateTime? DataVerificacao { get; private set; } = null;
        public bool Ativo => DataVerificacao != null && DataExpiracao == null;

        public void VerificarCodigo(string codigo)
        {
            if (Ativo)
            {
                AddNotification("VerificacaoEmail.Codigo", "Este código já foi ativado.");
            }
            if (DataExpiracao < DateTime.UtcNow)
            {
                AddNotification("VerificacaoEmail.Codigo", "Este código já expirou.");
            }
            if (!string.Equals(a: codigo.Trim(), b: Codigo.Trim(), comparisonType: StringComparison.CurrentCultureIgnoreCase))
            {
                AddNotification("VerificacaoEmail.Codigo", "Código de verificação inválido.");
            }
            DataExpiracao = null;
            DataVerificacao = DateTime.UtcNow;
        }
    }
}
