using ListaDeTarefas.Domain.Models;

namespace ListaDeTarefas.Application.Interfaces.Services
{
    public interface IEmailService
    {
        Task EnviarEmailVerificacao(Usuario usuario);
    }
}
