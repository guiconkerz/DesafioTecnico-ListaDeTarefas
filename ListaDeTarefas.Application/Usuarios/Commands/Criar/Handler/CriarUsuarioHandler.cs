using ListaDeTarefas.Application.Interfaces.Perfis;
using ListaDeTarefas.Application.Interfaces.Services;
using ListaDeTarefas.Application.Interfaces.UnitOfWork;
using ListaDeTarefas.Application.Interfaces.Usuarios;
using ListaDeTarefas.Application.Interfaces.Usuarios.Handler;
using ListaDeTarefas.Application.Usuarios.Commands.Criar.Request;
using ListaDeTarefas.Application.Usuarios.Commands.Criar.Response;
using ListaDeTarefas.Domain.Models;
using ListaDeTarefas.Domain.ValueObjects;
using ListaDeTarefas.Shared.Interfaces;
using System.Net;

namespace ListaDeTarefas.Application.Usuarios.Commands.Criar.Handler
{
    public class CriarUsuarioHandler : ICriarUsuarioHandler
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IPerfilRepositorio _perfilRepositorio;
        private readonly IEmailServices _emailService;

        public CriarUsuarioHandler(IUnitOfWork unitOfWork, IUsuarioRepositorio usuarioRepositorio, IEmailServices emailService, IPerfilRepositorio perfilRepositorio)
        {
            _unitOfWork = unitOfWork;
            _usuarioRepositorio = usuarioRepositorio;
            _emailService = emailService;
            _perfilRepositorio = perfilRepositorio;
        }

        public async Task<IResponse> Handle(CriarUsuarioRequest request)
        {
            #region Validações
            request.Validar();
            if (!request.IsValid)
            {
                return new CriarUsuarioResponse(StatusCode: HttpStatusCode.BadRequest,
                                                Mensagem: "Requisição inválida. Por favor, valide os dados informados.",
                                                Notifications: request.Notifications);
            }
            #endregion

            try
            {
                #region Verificar se E-mail está cadastrado

                var emailCadastrado = await _usuarioRepositorio.EmailCadastrado(request.Email);
                if (emailCadastrado)
                {
                    return new CriarUsuarioResponse(StatusCode: HttpStatusCode.BadRequest,
                                                Mensagem: "Este e-mail já está cadastrado.",
                                                Notifications: request.Notifications);
                }

                #endregion

                #region Verifica o perfil informado

                var perfil = await _perfilRepositorio.ObterPorNomeAsync(request.Perfil);
                if (perfil is null)
                {
                    return new CriarUsuarioResponse(StatusCode: HttpStatusCode.BadRequest,
                                                    Mensagem: $"O Perfil informado não existe no sistema.",
                                                    Notifications: request.Notifications);
                }

                #endregion

                #region Adiciona um usuário 

                var usuario = new Usuario(
                                    login: new Domain.ValueObjects.Login(username: request.Login),
                                    senha: new Senha(senha: request.Senha),
                                    email: new Email(request.Email),
                                    perfil: perfil);

                _unitOfWork.BeginTransaction();

                await _usuarioRepositorio.AdicionarAsync(usuario);
                await _emailService.EnviarEmailVerificacao(usuario);

                _unitOfWork.Commit();

                return new CriarUsuarioResponse(StatusCode: HttpStatusCode.OK,
                                                Mensagem: $"Usuario {request.Login} criado com sucesso!.",
                                                Notifications: request.Notifications);

                #endregion
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
