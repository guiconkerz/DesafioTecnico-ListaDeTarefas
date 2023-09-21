using ListaDeTarefas.Application.Usuarios.Commands.SolicitarAlteracaoSenha.Request;
using ListaDeTarefas.Shared.Interfaces;

namespace ListaDeTarefas.Application.Interfaces.Usuarios.Handler
{
    public interface ISolicitarAlteracaoSenhaHandler
    {
        Task<IResponse> Handle(SolicitarAlteracaoSenhaRequest request);
    }
}
