using ListaDeTarefas.Application.Interfaces.Perfis;
using ListaDeTarefas.Application.Interfaces.Services;
using ListaDeTarefas.Application.Interfaces.UnitOfWork;
using ListaDeTarefas.Application.Interfaces.Usuarios;
using ListaDeTarefas.Application.Usuarios.Commands.Criar.Handler;
using ListaDeTarefas.Application.Usuarios.Commands.Criar.Request;
using ListaDeTarefas.Domain.Models;
using ListaDeTarefas.Domain.ValueObjects;

namespace ListaDeTarefas.Tests.Commands.Criar
{
    [TestClass]
    public class CriarUsuarioTest
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IPerfilRepositorio _perfilRepositorio;
        private readonly IEmailServices _emailService;
        private readonly Usuario _usuario;
        private readonly Perfil _perfil;
        private readonly Login _login;
        private readonly Senha _senha;
        private readonly Email _email;

        public CriarUsuarioTest()
        {
            _login = new Login("admin");
            _senha = new Senha("@Admin123");
            _email = new Email("email@email.com");
            _perfil = new Perfil("Administrador");
        }

        [TestMethod]
        [DataRow("some string")]
        [DataRow("some@string")]
        [DataRow("some@stringcom")]
        [DataRow("somestring.com")]
        public void RetonarErroAoInformarEmailInvalido(string email)
        {
            var request = new CriarUsuarioRequest(login: _login.Username.Trim(),
                                                  senha: _senha.Password.Trim(),
                                                  email: email.Trim(),
                                                  perfil: "Adm");

            var response = new CriarUsuarioHandler(_unitOfWork, _usuarioRepositorio, _emailService, _perfilRepositorio).Handle(request).GetAwaiter().GetResult();
            Assert.IsTrue(response.Notifications.Count > 0);
        }

        [TestMethod]
        [DataRow("some@gmail.com")]
        [DataRow("some@hotmail.com")]
        [DataRow("some@customdomain.com")]
        [DataRow("some@outlook.com")]
        [DataRow("some@yahoo.com")]
        [DataRow("some@dominio.com.br")]
        public void RetonarSucessoAoInformarEmailValido(string email)
        {
            var request = new CriarUsuarioRequest(login: _login.Username.Trim(),
                                                  senha: _senha.Password.Trim(),
                                                  email: email.Trim(),
                                                   perfil: "Adm");
            var response = new CriarUsuarioHandler(_unitOfWork, _usuarioRepositorio, _emailService, _perfilRepositorio).Handle(request).GetAwaiter().GetResult();
            Assert.IsTrue(response.Notifications.Count == 0);
        }

        [TestMethod]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow("  ")]
        public void RetornarErroAoInformarLoginInvalido(string login)
        {
            var request = new CriarUsuarioRequest(login: login.Trim(),
                                                  senha: _senha.Password.Trim(),
                                                  email: _email.Endereco.Trim(),
                                                  perfil: "Adm");
            var response = new CriarUsuarioHandler(_unitOfWork, _usuarioRepositorio, _emailService, _perfilRepositorio).Handle(request).GetAwaiter().GetResult();
            Assert.IsTrue(response.Notifications.Count > 0);
        }

        [TestMethod]
        [DataRow(" usuario")]
        [DataRow("usuario ")]
        [DataRow(" usuario ")]
        [DataRow("usuario")]
        [DataRow("usuario1")]
        [DataRow("343423")]
        [DataRow("sdasd232")]
        public void RetornarSucessoAoInformarLoginValido(string login)
        {
            var request = new CriarUsuarioRequest(login: login.Trim(),
                                                  senha: _senha.Password.Trim(),
                                                  email: _email.Endereco.Trim(),
                                                  perfil: "Adm");
            var response = new CriarUsuarioHandler(_unitOfWork, _usuarioRepositorio, _emailService, _perfilRepositorio).Handle(request).GetAwaiter().GetResult();
            Assert.IsTrue(response.Notifications.Count == 0);
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
            var request = new CriarUsuarioRequest(login: _login.Username.Trim(),
                                                  senha: senha.Trim(),
                                                  email: _email.Endereco.Trim(),
                                                  perfil: "Adm");
            var response = new CriarUsuarioHandler(_unitOfWork, _usuarioRepositorio, _emailService, _perfilRepositorio).Handle(request).GetAwaiter().GetResult();
            Assert.IsTrue(response.Notifications.Count > 0);
        }

        [TestMethod]
        [DataRow("A1b2C3d4E5f6G7h8i9!")]
        [DataRow("@@#@#@#@adasdAasd1s")]
        [DataRow("@@#@#@#@321321321aZ")]
        [DataRow("457f45$%ds%Xdsmsd")]
        public void RetornarSucessoAoInformarSenhaValida(string senha)
        {
            var request = new CriarUsuarioRequest(login: _login.Username.Trim(),
                                                  senha: senha.Trim(),
                                                  email: _email.Endereco.Trim(),
                                                  perfil: "Adm");
            var response = new CriarUsuarioHandler(_unitOfWork, _usuarioRepositorio, _emailService, _perfilRepositorio).Handle(request).GetAwaiter().GetResult();
            Assert.IsTrue(response.Notifications.Count == 0);
        }

        [TestMethod]
        public void RetornarSucessoAoCriarUsuario()
        {
            var request = new CriarUsuarioRequest(login: _login.Username.Trim(),
                                                  senha: _senha.Password.Trim(),
                                                  email: _email.Endereco.Trim(),
                                                  perfil: "Adm");
            var response = new CriarUsuarioHandler(_unitOfWork, _usuarioRepositorio, _emailService, _perfilRepositorio).Handle(request).GetAwaiter().GetResult();
            Assert.IsTrue(response.Notifications.Count == 0);
        }
    }
}
