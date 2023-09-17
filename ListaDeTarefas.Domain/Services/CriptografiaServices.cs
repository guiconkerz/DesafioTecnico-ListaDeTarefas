namespace ListaDeTarefas.Domain.Services
{
    public static class CriptografiaServices
    {
        public static string CriptografarSenha(this string senha)
        {
            var salt = BCrypt.Net.BCrypt.GenerateSalt();
            var senhaCriptografada = BCrypt.Net.BCrypt.HashPassword(senha, salt);

            return senhaCriptografada;
        }
    }
}
