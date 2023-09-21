using ListaDeTarefas.Domain.Models;

namespace ListaDeTarefas.Application.Interfaces.Services
{
    public interface IEmailServices
    {
        Task EnviarEmailVerificacao(Usuario usuario);
        Task EnviarEmailSucessoAtivacao(Usuario usuario);
        Task EnviarEmailCodigoAlteracaoSenha(Usuario usuario);
        Task EnviarEmailSucessoAlteracaoSenha(Usuario usuario);
    }
}
