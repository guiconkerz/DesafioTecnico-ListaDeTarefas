using ListaDeTarefas.Application.Interfaces.Services;
using ListaDeTarefas.Domain.Models;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace ListaDeTarefas.Infra.Services
{
    public class EmailServices : IEmailServices
    {
        private readonly IConfiguration _configuration;

        public EmailServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task EnviarEmailVerificacao(Usuario usuario)
        {
            var host = _configuration["SMTP:Host"];
            var nome = _configuration[key: "SMTP:Nome"];
            var username = _configuration[key: "SMTP:UserName"];
            var senha = _configuration[key: "SMTP:Senha"];
            var porta = Convert.ToInt32(_configuration[key: "SMTP:Porta"]);

            //Adiciona destino do email
            var mail = new MailMessage()
            {
                From = new MailAddress(address: username, displayName: nome)
            };

            mail.To.Add(usuario.Email.Endereco);
            //Define o assunto
            mail.Subject = "Verificação de conta";
            //Define a mensagem
            mail.Body = $"Olá {usuario.Login.Username!}\n" +
                        $"O código de ativação da sua conta é: {usuario.Email.VerificarEmail.Codigo}";
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

        public async Task EnviarEmailSucessoAtivacao(Usuario usuario)
        {
            var host = _configuration["SMTP:Host"];
            var nome = _configuration[key: "SMTP:Nome"];
            var username = _configuration[key: "SMTP:UserName"];
            var senha = _configuration[key: "SMTP:Senha"];
            var porta = Convert.ToInt32(_configuration[key: "SMTP:Porta"]);

            //Adiciona destino do email
            var mail = new MailMessage()
            {
                From = new MailAddress(address: username, displayName: nome)
            };

            mail.To.Add(usuario.Email.Endereco);
            //Define o assunto
            mail.Subject = $"Conta ativada!";
            //Define a mensagem
            mail.Body = $"Olá {usuario.Login.Username!}\n" +
                       $"Seja bem vindo!" +
                        $"Sua conta foi ativada com sucesso!\n\n" +
                        $"{DateTime.Now:dd/MM/yyyy HH:mm:ss}";

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

        public async Task EnviarEmailCodigoAlteracaoSenha(Usuario usuario)
        {
            var host = _configuration["SMTP:Host"];
            var nome = _configuration[key: "SMTP:Nome"];
            var username = _configuration[key: "SMTP:UserName"];
            var senha = _configuration[key: "SMTP:Senha"];
            var porta = Convert.ToInt32(_configuration[key: "SMTP:Porta"]);

            //Adiciona destino do email
            var mail = new MailMessage()
            {
                From = new MailAddress(address: username, displayName: nome)
            };

            mail.To.Add(usuario.Email.Endereco);
            //Define o assunto
            mail.Subject = $"Alteração de senha";
            //Define a mensagem
            mail.Body = $"Olá {usuario.Login.Username!}\n" +
                       $"Segue código para alteração de senha: {usuario.Senha.CodigoAlteracao}" +
                        $"{DateTime.Now:dd/MM/yyyy HH:mm:ss}";

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

        public async Task EnviarEmailSucessoAlteracaoSenha(Usuario usuario)
        {
            var host = _configuration["SMTP:Host"];
            var nome = _configuration[key: "SMTP:Nome"];
            var username = _configuration[key: "SMTP:UserName"];
            var senha = _configuration[key: "SMTP:Senha"];
            var porta = Convert.ToInt32(_configuration[key: "SMTP:Porta"]);

            //Adiciona destino do email
            var mail = new MailMessage()
            {
                From = new MailAddress(address: username, displayName: nome)
            };

            mail.To.Add(usuario.Email.Endereco);
            //Define o assunto
            mail.Subject = $"Senha alterada com sucesso!";
            //Define a mensagem
            mail.Body = $"Olá {usuario.Login.Username!}\n" +
                       $"Sua senha foi alterada com sucesso!" +
                        $"{DateTime.Now:dd/MM/yyyy HH:mm:ss}";

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
