using ListaDeTarefas.Application.Usuarios.Commands.AlterarSenha.Request;
using ListaDeTarefas.Shared.Interfaces;

namespace ListaDeTarefas.Application.Interfaces.Usuarios.Handler
{
    public interface IAlterarSenhaHandler
    {
        Task<IResponse> Handle(AlterarSenhaRequest request);
    }
}
