using ListaDeTarefas.Application.Interfaces.RepositoryBase;
using ListaDeTarefas.Application.Interfaces.Tarefas;
using ListaDeTarefas.Application.Interfaces.UnitOfWork;
using ListaDeTarefas.Application.Interfaces.Usuarios;
using ListaDeTarefas.Application.Interfaces.Usuarios.Handler;
using ListaDeTarefas.Application.Tarefas.Commands.Criar.Handler;
using ListaDeTarefas.Application.Tarefas.Commands.MarcarEmAndamento.Handler;
using ListaDeTarefas.Application.Tarefas.Commands.MarcarFinalizada.Handler;
using ListaDeTarefas.Application.Usuarios.Commands.AlterarSenha.Handler;
using ListaDeTarefas.Application.Usuarios.Commands.Criar.Handler;
using ListaDeTarefas.Application.Usuarios.Commands.Excluir.Handler;
using ListaDeTarefas.Infra.Data.Context;
using ListaDeTarefas.Infra.Queries;
using ListaDeTarefas.Infra.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
AddServices(builder);
AddRepositories(builder);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static void AddServices(WebApplicationBuilder builder)
{
    builder.Services.AddDbContext<TarefasDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));
}

static void AddRepositories(WebApplicationBuilder builder)
{
    builder.Services.AddScoped(serviceType: typeof(IRepositorioBase<>), implementationType: typeof(RepositorioBase<>));
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    //Usuario
    builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
    builder.Services.AddScoped<ICriarUsuarioHandler, CriarUsuarioHandler>();
    builder.Services.AddScoped<IExcluirUsuarioHandler, ExcluirUsuarioHandler>();
    builder.Services.AddScoped<IAlterarSenhaHandler, AlterarSenhaHandler>();
    builder.Services.AddScoped<ITarefasQueries, TarefasQueries>();

    //Tarefa
    builder.Services.AddScoped<ITarefasRepositorio, TarefasRepositorio>();
    builder.Services.AddScoped<ICriarTarefaHandler, CriarTarefaHandler>();
    builder.Services.AddScoped<ITarefaEmAndamentoHandler, TarefaEmAndamentoHandler>();
    builder.Services.AddScoped<IFinalizarTarefaHandler, FinalizarTarefaHandler>();
}