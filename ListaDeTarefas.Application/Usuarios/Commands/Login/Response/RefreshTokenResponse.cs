namespace ListaDeTarefas.Application.Usuarios.Commands.Login.Response
{
    public sealed class RefreshTokenResponse
    {
        public RefreshTokenResponse(string token, string refreshToken, DateTime data)
        {
            Token = token;
            RefreshToken = refreshToken;
            Data = data;
        }

        public string Token { get; private set; }
        public string RefreshToken { get; private set; }
        public DateTime Data { get; private set; }
    }
}
