﻿namespace ListaDeTarefas.Domain
{
    public static class Configuration
    {
        public static SecretsConfiguration Secrets { get; set; } = new();
        public static EmailConfiguration Email { get; set; } = new();
        public static SendGridConfiguration SendGrid { get; set; } = new();
        public class SecretsConfiguration
        {
            public string ApiKey { get; set; } = string.Empty;
            public string JwtPrivateKey { get; set; } = string.Empty;
            public string PasswordSaltKey { get; set; } = string.Empty;
        }

        public class EmailConfiguration
        {
            public string DefaultFromEmail { get; set; } = "teste@email.com";
            public string DefaultFromName { get; set; } = "Guilherme Beltran";
        }

        public class SendGridConfiguration
        {
            public string ApiKey { get; set; } = string.Empty;
        }
    }
}
