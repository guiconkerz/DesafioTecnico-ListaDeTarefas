using ListaDeTarefas.Application.Interfaces.Perfis;
using ListaDeTarefas.Application.Interfaces.RepositoryBase;
using ListaDeTarefas.Application.Interfaces.Services;
using ListaDeTarefas.Application.Interfaces.Tarefas;
using ListaDeTarefas.Application.Interfaces.UnitOfWork;
using ListaDeTarefas.Application.Interfaces.Usuarios;
using ListaDeTarefas.Application.Interfaces.Usuarios.Handler;
using ListaDeTarefas.Application.Tarefas.Commands.AlterarDescricao.Handler;
using ListaDeTarefas.Application.Tarefas.Commands.AlterarTitulo.Handler;
using ListaDeTarefas.Application.Tarefas.Commands.Criar.Handler;
using ListaDeTarefas.Application.Tarefas.Commands.MarcarEmAndamento.Handler;
using ListaDeTarefas.Application.Tarefas.Commands.MarcarFinalizada.Handler;
using ListaDeTarefas.Application.Usuarios.Commands.AlterarSenha.Handler;
using ListaDeTarefas.Application.Usuarios.Commands.AtivarConta.Handler;
using ListaDeTarefas.Application.Usuarios.Commands.Criar.Handler;
using ListaDeTarefas.Application.Usuarios.Commands.Excluir.Handler;
using ListaDeTarefas.Application.Usuarios.Commands.Login.Handler;
using ListaDeTarefas.Application.Usuarios.Commands.SolicitarAlteracaoSenha.Handler;
using ListaDeTarefas.Infra.Data.Context;
using ListaDeTarefas.Infra.Queries;
using ListaDeTarefas.Infra.Repositories;
using ListaDeTarefas.Infra.Services;
using Microsoft.EntityFrameworkCore;

namespace ListaDeTarefas.API.Extensions
{
    public static class ServiceExtension
    {
        public static void AddServices(this WebApplicationBuilder builder)
        {
            #region Connection

            builder.Services.AddDbContext<TarefasDbContext>(options => options.UseSqlServer(builder.Configuration["ConnectionStrings:Database"]));

            #endregion 

            #region Services

            builder.Services.AddScoped<IEmailServices, EmailServices>();
            builder.Services.AddScoped<ITokenServices, TokenServices>();

            #endregion

            AddRepositories(builder);
        }

        private static void AddRepositories(WebApplicationBuilder builder)
        {
            #region Unit of Work

            builder.Services.AddScoped(serviceType: typeof(IRepositorioBase<>), implementationType: typeof(RepositorioBase<>));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            #endregion

            #region Usuario

            builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();

            builder.Services.AddScoped<ICriarUsuarioHandler, CriarUsuarioHandler>();
            builder.Services.AddScoped<IExcluirUsuarioHandler, ExcluirUsuarioHandler>();
            builder.Services.AddScoped<IAlterarSenhaHandler, AlterarSenhaHandler>();
            builder.Services.AddScoped<ILogarHandler, LogarHandler>();
            builder.Services.AddScoped<IAtivarContaHandler, AtivarContaHandler>();
            builder.Services.AddScoped<ISolicitarAlteracaoSenhaHandler, SolicitarAlteracaoSenhaHandler>();

            #endregion

            #region Tarefa

            builder.Services.AddScoped<ITarefasRepositorio, TarefasRepositorio>();

            builder.Services.AddScoped<ICriarTarefaHandler, CriarTarefaHandler>();
            builder.Services.AddScoped<ITarefaEmAndamentoHandler, TarefaEmAndamentoHandler>();
            builder.Services.AddScoped<IFinalizarTarefaHandler, FinalizarTarefaHandler>();
            builder.Services.AddScoped<IAlterarDescricaoTarefaHandler, AlterarDescricaoTarefaHandler>();
            builder.Services.AddScoped<IAlterarTituloTarefaHandler, AlterarTituloTarefaHandler>();

            builder.Services.AddScoped<ITarefasQueries, TarefasQueries>();

            #endregion

            #region Perfil
            builder.Services.AddScoped<IPerfilRepositorio, PerfilRepositorio>();
            #endregion

        }
    }
}
