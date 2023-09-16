namespace ListaDeTarefas.Domain.Services
{
    public static class CriptografiaServices
    {
        public static string Criptografar(this string objeto)
        {
            var hash = BCrypt.Net.BCrypt.HashString(objeto.Trim());
            return hash;
        }

        public static string CriptografarSenha(this string senha)
        {
            var salt = GerarSalt();
            var senhaCriptografada = BCrypt.Net.BCrypt.HashPassword(senha, salt);

            return senhaCriptografada;
        }

        public static bool VerificarSenha(string senha, string senhaCriptografada)
        {
            return BCrypt.Net.BCrypt.Verify(senha, senhaCriptografada);
        }

        private static string GerarSalt()
        {
            var salt = BCrypt.Net.BCrypt.GenerateSalt();
            return salt;
        }
    }
}
