using ListaDeTarefas.Application.Interfaces.Perfis;
using ListaDeTarefas.Application.Interfaces.Services;
using ListaDeTarefas.Application.Interfaces.UnitOfWork;
using ListaDeTarefas.Application.Interfaces.Usuarios;
using ListaDeTarefas.Application.Usuarios.Commands.Criar.Handler;
using ListaDeTarefas.Application.Usuarios.Commands.Criar.Request;
using ListaDeTarefas.Domain.Models;
using ListaDeTarefas.Domain.ValueObjects;
using Moq;
using System.Net;

namespace ListaDeTarefas.Tests.Commands.Criar
{
    [TestClass]
    public class CriarUsuarioTest
    {
        private Mock<IUnitOfWork> uowMock;
        private Mock<IUsuarioRepositorio> usuarioMock;
        private Mock<IPerfilRepositorio> perfilMock;
        private Mock<IEmailServices> emailMock;
        private CriarUsuarioRequest request;
        private readonly Login _login;
        private readonly Senha _senha;
        private readonly Email _email;

        public CriarUsuarioTest()
        {
            uowMock = new Mock<IUnitOfWork>();
            usuarioMock = new Mock<IUsuarioRepositorio>();
            perfilMock = new Mock<IPerfilRepositorio>();
            emailMock = new Mock<IEmailServices>();

            _login = new Login("admin");
            _senha = new Senha("@Admin123");
            _email = new Email("email@email.com");

            request = new CriarUsuarioRequest(login: _login.Username.Trim(),
                                                  senha: _senha.Password.Trim(),
                                                  email: _email.Endereco.Trim(),
                                                  perfil: "Admin");


            
        }

        [TestMethod]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow("  ")]
        public void RetornarErroAoInformarLoginInvalido(string login)
        {
            request.Login = login;

            var response = new CriarUsuarioHandler(uowMock.Object, usuarioMock.Object, emailMock.Object, perfilMock.Object).Handle(request).GetAwaiter().GetResult();
            Assert.IsTrue(response.Notifications.Count > 0);
        }


        [TestMethod]
        [DataRow(" usuario")]
        [DataRow("usuario ")]
        [DataRow(" usuario ")]
        [DataRow("usuario")]
        [DataRow("usuario1")]
        [DataRow("343423")]
        public void RetornarSucessoAoInformarLoginValido(string login)
        {
            request.Login = login;

            var response = new CriarUsuarioHandler(uowMock.Object, usuarioMock.Object, emailMock.Object, perfilMock.Object).Handle(request).GetAwaiter().GetResult();
            Assert.IsTrue(response.Notifications.Count == 0);
        }

        [TestMethod]
        [DataRow("some string")]
        [DataRow("some@string")]
        [DataRow("some@stringcom")]
        [DataRow("somestring.com")]
        public void RetonarErroAoInformarEmailInvalido(string email)
        {

            request.Email = email;

            var response = new CriarUsuarioHandler(uowMock.Object, usuarioMock.Object, emailMock.Object, perfilMock.Object).Handle(request).GetAwaiter().GetResult();

            Assert.IsTrue(response.Notifications.Count > 0);
        }

        [TestMethod]
        [DataRow("some@gmail.com")]
        [DataRow("some@hotmail.com")]
        [DataRow("some@customdomain.com")]
        [DataRow("some@outlook.com")]
        [DataRow("some@yahoo.com")]
        [DataRow("some@dominio.com.br")]
        public void RetonarErroAoInformarEmailValidoEJaCadastrado(string email)
        {
            request.Email = email;

            usuarioMock.Setup(x => x.EmailCadastrado(request.Email)).ReturnsAsync(true);

            var response = new CriarUsuarioHandler(uowMock.Object, usuarioMock.Object, emailMock.Object, perfilMock.Object).Handle(request).GetAwaiter().GetResult();

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow("a ")]
        [DataRow("ab ")]
        [DataRow("abc ")]
        [DataRow("abcd")]
        [DataRow("abcde")]
        [DataRow("abcdef")]
        [DataRow("abcdefg")]
        [DataRow("abcdefgh")]
        [DataRow("abcdefghi")]
        [DataRow("1")]
        [DataRow("12")]
        [DataRow("123")]
        [DataRow("1234")]
        [DataRow("12345")]
        [DataRow("123456")]
        [DataRow("1234567")]
        [DataRow("12345678")]
        [DataRow("123456789")]
        [DataRow("a1bcdefghi")]
        [DataRow("A1bcdefghi")]
        [DataRow("A1b2C3d4E5f6G7h8i9")]
        [DataRow("@@#@#@#@adasdasds")]
        [DataRow("@@#@#@#@321321321")]
        public void RetornarErroAoInformarSenhaInvalida(string senha)
        {
            request.Senha = senha;
            var response = new CriarUsuarioHandler(uowMock.Object, usuarioMock.Object, emailMock.Object, perfilMock.Object).Handle(request).GetAwaiter().GetResult();
            Assert.IsTrue(response.Notifications.Count > 0);
        }

        [TestMethod]
        [DataRow("A1b2C3d4E5f6G7h8i9!")]
        [DataRow("@@#@#@#@adasdAasd1s")]
        [DataRow("@@#@#@#@321321321aZ")]
        [DataRow("457f45$%ds%Xdsmsd")]
        public void RetornarSucessoAoInformarSenhaValida(string senha)
        {
            request.Senha = senha;

            var response = new CriarUsuarioHandler(uowMock.Object, usuarioMock.Object, emailMock.Object, perfilMock.Object).Handle(request).GetAwaiter().GetResult();
            Assert.IsTrue(response.Notifications.Count == 0);
        }

        [TestMethod]
        [DataRow("Administrador")]
        [DataRow("Padrão")]
        public void RetonarErroAoInformarPerfilInvalido(string perfil)
        {
            request.Perfil = perfil;

            perfilMock.Setup(x => x.ObterPorNomeAsync(request.Perfil)).ReturnsAsync((Perfil)null);

            var response = new CriarUsuarioHandler(uowMock.Object, usuarioMock.Object, emailMock.Object, perfilMock.Object).Handle(request).GetAwaiter().GetResult();

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        [DataRow("Administrador")]
        [DataRow("Padrão")]
        public void RetonarSucessoAoInformarPerfilValido(string perfil)
        {
            request.Perfil = perfil;

            perfilMock.Setup(x => x.ObterPorNomeAsync(request.Perfil)).ReturnsAsync(new Perfil(request.Perfil));

            var response = new CriarUsuarioHandler(uowMock.Object, usuarioMock.Object, emailMock.Object, perfilMock.Object).Handle(request).GetAwaiter().GetResult();

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }


        [TestMethod]
        public void RetornarSucessoAoCriarUsuario()
        {
            usuarioMock.Setup(x => x.EmailCadastrado(request.Email)).ReturnsAsync(false);
            perfilMock.Setup(x => x.ObterPorNomeAsync(request.Perfil)).ReturnsAsync(new Perfil(request.Perfil));

            var response = new CriarUsuarioHandler(uowMock.Object, usuarioMock.Object, emailMock.Object, perfilMock.Object).Handle(request).GetAwaiter().GetResult();

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
