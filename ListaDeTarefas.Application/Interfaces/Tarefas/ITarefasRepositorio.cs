using ListaDeTarefas.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaDeTarefas.Application.Interfaces.Tarefas
{
    public interface ITarefasRepositorio
    {
        Task AdicionarAsync(Tarefa tarefa);
        Task<bool> AtualizarAsync(Tarefa tarefa);
        Task<bool> RemoverAsync(int id);
    }
}
