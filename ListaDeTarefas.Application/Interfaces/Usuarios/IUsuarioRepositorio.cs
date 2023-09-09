using ListaDeTarefas.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaDeTarefas.Application.Interfaces.Usuarios
{
    public interface IUsuarioRepositorio
    {
        Task Adicionar(Usuario usuario);
        Task<Usuario> BuscarPorId(int id);
    }
}
