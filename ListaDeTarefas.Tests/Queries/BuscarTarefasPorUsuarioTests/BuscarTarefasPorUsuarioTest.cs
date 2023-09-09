using ListaDeTarefas.Domain.Models;
using ListaDeTarefas.Domain.Queries;
using ListaDeTarefas.Domain.ValueObjects;

namespace ListaDeTarefas.Tests.Queries.BuscarTarefasPorUsuarioTests
{
    [TestClass]
    public class BuscarTarefasPorUsuarioTest
    {
        private List<Tarefa> _tarefas;
        private Login _login;
        private Senha _senha;
        private Email _email;
        private Usuario _usuario;
        private Usuario _usuario1;
        private Usuario _usuario2;
        private Usuario _usuario3;
        private Usuario _usuario4;
        public BuscarTarefasPorUsuarioTest()
        {
            _login = new Login("admin");
            var _login1 = new Login("admin1");
            var _login2 = new Login("admin2");
            var _login3 = new Login("admin3");
            var _login4 = new Login("admin4");

            _senha = new Senha("@Admin123");
            var senha1 = new Senha("@Admin123");
            var senha2 = new Senha("@Admin123");
            var senha3 = new Senha("@Admin123");
            var senha4 = new Senha("@Admin123");

            _email = new Email("email@email.com");
            var email1 = new Email("email@email.com");
            var email2 = new Email("email@email.com");
            var email3 = new Email("email@email.com");
            var email4 = new Email("email@email.com");


            _usuario = new Usuario(_login, _senha, _email);

            _usuario1 = new Usuario(_login1, senha1, email1);
            _usuario2 = new Usuario(_login2, senha2, email2);
            _usuario3 = new Usuario(_login3, senha3, email3);
            _usuario4 = new Usuario(_login4, senha4, email4);

            _tarefas = new List<Tarefa>
            {
                new Tarefa(titulo: "Tarefa 1", descricao: "tenho que fazer a tarefa 1", new DateTime(2023, 09, 09), finalizada: false, _usuario1),
                new Tarefa(titulo: "Tarefa 2", descricao: "tenho que fazer a tarefa 2", new DateTime(2023, 09, 12), finalizada: false, _usuario3),
                new Tarefa(titulo: "Tarefa 3", descricao: "tenho que fazer a tarefa 3", new DateTime(2023, 09, 20), finalizada: false, _usuario4),
                new Tarefa(titulo: "Tarefa 4", descricao: "tenho que fazer a tarefa 4", new DateTime(2023, 09, 15), finalizada: false, _usuario1),
                new Tarefa(titulo: "Tarefa 5", descricao: "tenho que fazer a tarefa 5", new DateTime(2023, 09, 10), finalizada: true, _usuario3),
                new Tarefa(titulo: "Tarefa 6", descricao: "tenho que fazer a tarefa 6", new DateTime(2023, 09, 10), finalizada: false, _usuario2),
                new Tarefa(titulo: "Tarefa 7", descricao: "tenho que fazer a tarefa 7", new DateTime(2023, 09, 14), finalizada: true, _usuario4),
                new Tarefa(titulo: "Tarefa 8", descricao: "tenho que fazer a tarefa 8", new DateTime(2023, 09, 11), finalizada: true, _usuario2),
                new Tarefa(titulo: "Tarefa 9", descricao: "tenho que fazer a tarefa 9", new DateTime(2023, 09, 08), finalizada: true, _usuario1),
            };
        }

        [TestMethod]
        public void Deve_Retornar_Tarefas_Apenas_Do_Usuario1()
        {
            var result = _tarefas.AsQueryable().Where(BuscarUsuarioQueries.ObterTodos(_usuario1.Login.Username));
            Assert.AreEqual(3, result.Count());
        }

        [TestMethod]
        public void Deve_Retornar_Apenas_Tarefas_Finalizadas_Do_Usuario1()
        {
            var result = _tarefas.AsQueryable().Where(BuscarUsuarioQueries.ObterTarefasFinalizadas(_usuario1.Login.Username));
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public void Deve_Retornar_Apenas_Tarefas_Em_Andamento_Do_Usuario1()
        {
            var result = _tarefas.AsQueryable().Where(BuscarUsuarioQueries.ObterTarefasEmAndamento(_usuario1.Login.Username));
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void Deve_Retornar_Apenas_Tarefas_Nao_Finalizadas_Por_Periodo_Do_Usuario1()
        {
            var dataInicial = new DateTime(2023, 09, 07);
            var dataFinal = new DateTime(2023, 09, 15);

            var result = _tarefas.AsQueryable().Where(BuscarUsuarioQueries.ObterPorPeriodo(_usuario1.Login.Username, dataInicial, dataFinal, false));
            Assert.AreEqual(2, result.Count());
        }
        [TestMethod]
        public void Deve_Retornar_Apenas_Tarefas_Finalizadas_Por_Periodo_Do_Usuario1()
        {
            var dataInicial = new DateTime(2023, 09, 07);
            var dataFinal = new DateTime(2023, 09, 15);

            var result = _tarefas.AsQueryable().Where(BuscarUsuarioQueries.ObterPorPeriodo(_usuario1.Login.Username, dataInicial, dataFinal, true));
            Assert.AreEqual(1, result.Count());
        }
    }
}
