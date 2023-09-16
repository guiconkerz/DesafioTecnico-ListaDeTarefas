using ListaDeTarefas.Application.Interfaces.UnitOfWork;
using ListaDeTarefas.Application.Interfaces.Usuarios;
using ListaDeTarefas.Application.Interfaces.Usuarios.Handler;
using ListaDeTarefas.Application.Usuarios.Commands.AlterarSenha.Request;
using ListaDeTarefas.Application.Usuarios.Commands.AlterarSenha.Response;
using ListaDeTarefas.Domain.Abstraction;
using ListaDeTarefas.Domain.ValueObjects;
using System.Net;

namespace ListaDeTarefas.Application.Usuarios.Commands.AlterarSenha.Handler
{
    public sealed class AlterarSenhaHandler : IAlterarSenhaHandler
    {
        public readonly IUsuarioRepositorio _usuarioRepositorio;
        public readonly IUnitOfWork _unitOfWork;
        public AlterarSenhaHandler(IUsuarioRepositorio usuarioRepositorio, IUnitOfWork unitOfWork)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _unitOfWork = unitOfWork;
        }

        public async Task<IResponse> Handle(AlterarSenhaRequest request)
        {

            request.Validar();
            if (!request.IsValid)
            {
                return new AlterarSenhaResponse(statusCode: HttpStatusCode.BadRequest,
                                                mensagem: "Requisição inválida. Por favor, valide os dados informados.",
                                                notifications: request.Notifications);
            }
            try
            {
                var usuario = await _usuarioRepositorio.BuscarPorIdAsync(request.Id);
                if (usuario is null)
                {
                    return new AlterarSenhaResponse(statusCode: HttpStatusCode.BadRequest, mensagem: $"Não foi encontrado usuário com Id {request.Id}", notifications: request.Notifications);
                }
                
                usuario.AlterarSenha(new Senha(request.NovaSenha));

                _unitOfWork.BeginTransaction();

                var senhaAlterada = await _usuarioRepositorio.AlterarSenha(usuario);
                if (senhaAlterada == false)
                {
                    return new AlterarSenhaResponse(statusCode: HttpStatusCode.InternalServerError,
                                                mensagem: $"Houve uma falha ao alterar senha do usuário {usuario.Email.Endereco}. Por favor, tente novamente mais tarde.",
                                                notifications: request.Notifications);
                }

                _unitOfWork.Commit();
                return new AlterarSenhaResponse(statusCode: HttpStatusCode.OK,
                                                mensagem: $"Senha alterada com sucesso!",
                                                notifications: request.Notifications);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw new Exception($"{ex.Message}");
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }
    }
}
