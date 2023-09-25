namespace ListaDeTarefas.Shared.Exceptions
{
    public sealed class DadosInvalidosException : Exception
    {
        private const string __mensagemPadrão = "Dado inválido.";
        public DadosInvalidosException(string mensagem = __mensagemPadrão) : base(mensagem) { }

        public static void ThrowIfNull(
            string? item,
            string mensagem = __mensagemPadrão)
        {
            if (string.IsNullOrEmpty(item) || string.IsNullOrWhiteSpace(item))
            {
                throw new DadosInvalidosException(mensagem);
            }
        }

        public static void ThrowIfNull(
            string?[] itens,
            string mensagem = __mensagemPadrão)
        {
            foreach (var item in itens)
            {
                if (string.IsNullOrEmpty(item) || string.IsNullOrWhiteSpace(item))
                {
                    throw new DadosInvalidosException(mensagem);
                }
            }
        }
    }
}
