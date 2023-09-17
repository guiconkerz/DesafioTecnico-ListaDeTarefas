using ListaDeTarefas.Application.Interfaces.Services;
using ListaDeTarefas.Domain;
using ListaDeTarefas.Domain.Models;
using ListaDeTarefas.Domain.ValueObjects;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;
using System.Net;

namespace ListaDeTarefas.Infra.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task EnviarEmailVerificacao(Usuario usuario)
        {
            var host = _configuration.GetSection("SMTP:Host").Value;
            var nome = _configuration.GetSection(key: "SMTP:Nome").Value;
            var username = _configuration.GetSection(key: "SMTP:UserName").Value;
            var senha = _configuration.GetSection(key: "SMTP:Senha").Value;
            var porta = Convert.ToInt32(_configuration.GetSection(key: "SMTP:Porta").Value);

            //Adiciona destino do email
            var mail = new MailMessage()
            {
                From = new MailAddress(address: username, displayName: nome)
            };

            mail.To.Add(usuario.Email.Endereco);
            //Define o assunto
            mail.Subject = "Verificação de conta";
            //Define a mensagem
            mail.Body = $"Código de ativação: {usuario.Email.VerificarEmail.Codigo}";
            //Habilita codigo html para o email
            mail.IsBodyHtml = true;
            //Define a prioridade do envio do email
            mail.Priority = MailPriority.High;

            using var smtp = new SmtpClient(host: host, port: porta);
            //Define as credenciais do email
            smtp.Credentials = new NetworkCredential(userName: username, password: senha);
            //Define o envio de email seguro
            smtp.EnableSsl = true;

            smtp.Send(message: mail);
        }
    }
}
