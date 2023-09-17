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
using ListaDeTarefas.Infra.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
AddServices(builder);
AddRepositories(builder);

builder.Services.AddTransient<TokenServices>();

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(PrivateKey.Key)),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});
builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("Admin", p => p.RequireUserName("teste1"));
});

// Add services to the container.


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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

static void AddServices(WebApplicationBuilder builder)
{
    builder.Services.AddDbContext<TarefasDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));

    builder.Configuration.GetSection("Secrets").GetValue<string>("ApiKey");
    builder.Configuration.GetSection("Secrets").GetValue<string>("JwtPrivateKey");
    builder.Configuration.GetSection("Secrets").GetValue<string>("PasswordSaltKey");
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