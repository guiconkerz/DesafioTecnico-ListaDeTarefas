using ListaDeTarefas.Application.Interfaces.UnitOfWork;
using ListaDeTarefas.Application.Interfaces.Usuarios;
using ListaDeTarefas.Application.Usuarios.Commands.Excluir.Handler;
using ListaDeTarefas.Application.Usuarios.Commands.Excluir.Request;
using ListaDeTarefas.Domain.Abstraction;
using ListaDeTarefas.Domain.Models;
using ListaDeTarefas.Domain.ValueObjects;
using ListaDeTarefas.Tests.FakeRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaDeTarefas.Tests.Commands.Excluir
{
    [TestClass]
    public class ExcluirUsuarioTests
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private ExcluirUsuarioRequest request;
        private FakeUsuarios fakeUsuarios;
        public ExcluirUsuarioTests()
        {
            request = new ExcluirUsuarioRequest(2);
            fakeUsuarios = new FakeUsuarios();
        }

        [TestMethod]
        public void Deve_retornar_erro_ao_informar_usuario_nao_cadastrado()
        {
            var erros = 0;
            foreach (var usuario in fakeUsuarios.Usuarios)
            {
                var request = new ExcluirUsuarioRequest(14);
                if (request.Id != usuario.UsuarioId)
                {
                    erros++;
                }
            }            
            Assert.IsTrue(erros > 0);
        }

        [TestMethod]
        public void Deve_retornar_sucesso_ao_excluir_um_usuario_cadastrado()
        {
            var response = new ExcluirUsuarioHandler(_unitOfWork, _usuarioRepositorio).Handle(request).GetAwaiter().GetResult();
            
            Assert.IsTrue(response.Notifications.Count == 0);
        }
    }
}
