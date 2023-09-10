using ListaDeTarefas.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaDeTarefas.Tests.FakeRepository
{
    public class FakeUsuarios
    {
        public List<Usuario> Usuarios { get; set; }
        public FakeUsuarios()
        {
            Usuarios = new List<Usuario>()
            {
                new Usuario(1),
                new Usuario(3),
                new Usuario(5),
                new Usuario(7),
                new Usuario(9),
                new Usuario(11),
                new Usuario(13),
                new Usuario(15)
            };
        }
    }
}
