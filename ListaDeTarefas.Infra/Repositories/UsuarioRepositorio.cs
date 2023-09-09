using ListaDeTarefas.Application.Interfaces.RepositoryBase;
using ListaDeTarefas.Application.Interfaces.Usuarios;
using ListaDeTarefas.Domain.Models;
using ListaDeTarefas.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaDeTarefas.Infra.Repositories
{
    public sealed class UsuarioRepositorio : IUsuarioRepositorio
    {
        public readonly TarefasDbContext _tarefasContext;
        public readonly IRepositorioBase<Usuario> _repositorioBase;

        public UsuarioRepositorio(TarefasDbContext tarefasContext, IRepositorioBase<Usuario> repositorioBase)
        {
            _tarefasContext = tarefasContext;
            _repositorioBase = repositorioBase;
        }

        public async Task Adicionar(Usuario usuario)
        {
            try
            {
                await _repositorioBase.AdicionarAsync(usuario);
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<Usuario> BuscarPorId(int id)
        {
            try
            {
                return await _repositorioBase.ObterPorId(x => x.UsuarioId == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }    
        }
    }
}
