using ListaDeTarefas.Domain.Models;
using System.Linq.Expressions;

namespace ListaDeTarefas.Domain.Queries
{
    public static class BuscarUsuarioQueries
    {
        public static Expression<Func<Tarefa, bool>> ObterTodos(string login) => x => x.Usuario.Login.Username == login;

        public static Expression<Func<Tarefa, bool>> ObterTarefasFinalizadas(string login) => x => x.Usuario.Login.Username == login && x.Finalizada == true;

        public static Expression<Func<Tarefa, bool>> ObterTarefasEmAndamento(string login) => x => x.Usuario.Login.Username == login && x.Finalizada == false;

        public static Expression<Func<Tarefa, bool>> ObterPorPeriodo(string login, DateTime dataInicial, DateTime dataFinal, bool finalizada) =>
            x => x.Usuario.Login.Username == login && x.Finalizada == finalizada && x.DataEntrega >= dataInicial && x.DataEntrega <= dataFinal;

    }
}
