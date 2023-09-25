using ListaDeTarefas.Domain.ValueObjects;
using ListaDeTarefas.Shared.Exceptions;

namespace ListaDeTarefas.Tests.Commands.VerificarEmailTests
{
    [TestClass]
    public class VerificarEmailTest
    {
        private VerificarEmail verificarEmail;

        [TestMethod]
        public void Deve_retornar_erro_ao_informar_codigo_expirado()
        {
            #region Arrange

             verificarEmail = new VerificarEmail(codigo: Guid.NewGuid().ToString(format: "N")[0..6].ToUpper(), 
                                                    dataExpiracao: DateTime.Now.AddMinutes(-5));

            #endregion

            #region Act

            var result = verificarEmail.VerificarCodigo("ABC123");

            #endregion


            #region Assert

            Assert.IsNotNull(result);
            Assert.IsFalse(result.CodigoValido);
            Assert.AreEqual("Este código já expirou.", result.Mensagem);

            #endregion
        }

        [TestMethod]
        public void Deve_retornar_erro_ao_informar_conta_ja_ativada()
        {
            #region Arrange

            verificarEmail = new VerificarEmail(codigo: null,
                                                    dataExpiracao: null,
                                                    dataVerificacao: DateTime.Now);

            #endregion

            #region Act

            var result = verificarEmail.VerificarCodigo("ABC123");

            #endregion


            #region Assert

            Assert.IsNotNull(result);
            Assert.IsFalse(result.CodigoValido);
            Assert.AreEqual("Essa conta já foi ativada.", result.Mensagem);

            #endregion

        }

        [TestMethod]
        public void Deve_retornar_erro_ao_informar_codigo_ja_utilizado()
        {
            #region Arrange

            verificarEmail = new VerificarEmail(codigo: Guid.NewGuid().ToString(format: "N")[0..6].ToUpper(),
                                                    dataExpiracao: null,
                                                    dataVerificacao: DateTime.Now);


            #endregion

            #region Act

            var result = verificarEmail.VerificarCodigo("ABC123");

            #endregion

            #region Assert

            Assert.IsNotNull(result);
            Assert.IsFalse(result.CodigoValido);
            Assert.AreEqual("Este código já foi utilizado.", result.Mensagem);

            #endregion
        }

        [TestMethod]
        public void Deve_retornar_excecao_ao_informar_codigo_nulo()
        {
            verificarEmail = new VerificarEmail();
            #region Assert

            Assert.ThrowsException<DadosInvalidosException>(() =>
            {
                verificarEmail.VerificarCodigo(null);
            });

            #endregion
        }
    }
}
