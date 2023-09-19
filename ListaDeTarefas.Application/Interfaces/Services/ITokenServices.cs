using ListaDeTarefas.Application.Usuarios.Commands.Login.Response;

namespace ListaDeTarefas.Application.Interfaces.Services
{
    public interface ITokenServices
    {
        string GerarToken(LogarResponse response);
    }
}
