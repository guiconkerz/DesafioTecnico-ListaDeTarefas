using Azure;
using ListaDeTarefas.Application.Interfaces.UnitOfWork;
using ListaDeTarefas.Application.Interfaces.Usuarios;
using ListaDeTarefas.Application.Usuarios.Commands.Excluir.Handler;
using ListaDeTarefas.Application.Usuarios.Commands.Excluir.Request;
using ListaDeTarefas.Domain.Abstraction;
using ListaDeTarefas.Domain.Models;
using ListaDeTarefas.Domain.ValueObjects;
using ListaDeTarefas.Infra.Repositories;
using ListaDeTarefas.Tests.FakeRepository;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
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
            var sucesso = 0;
            foreach (var usuario in fakeUsuarios.Usuarios)
            {
                var request = new ExcluirUsuarioRequest(14);
                if (usuario.UsuarioId == request.Id)
                {
                    fakeUsuarios.Usuarios.Remove(usuario);
                    sucesso++;
                }
            }            
            Assert.IsTrue(sucesso == 0);
        }

        [TestMethod]
        public void Deve_retornar_sucesso_ao_excluir_um_usuario_com_id_15_que_esta_cadastrado()
        {
            var sucesso = 0;
            foreach (var usuario in fakeUsuarios.Usuarios)
            {
                var request = new ExcluirUsuarioRequest(15);
                if (usuario.UsuarioId == request.Id)
                {
                    fakeUsuarios.Usuarios.Remove(usuario);
                    sucesso++;
                    break;
                }
            }
            Assert.IsTrue(sucesso > 0);
        }
    }
}
