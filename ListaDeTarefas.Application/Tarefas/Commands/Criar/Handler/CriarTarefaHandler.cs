using ListaDeTarefas.Application.Interfaces.Tarefas;
using ListaDeTarefas.Application.Tarefas.Commands.Criar.Request;
using ListaDeTarefas.Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaDeTarefas.Application.Tarefas.Commands.Criar.Handler
{
    public sealed class CriarTarefaHandler : ICriarTarefaHandler
    {
        public Task<IResponse> Handler(CriarTarefaRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
