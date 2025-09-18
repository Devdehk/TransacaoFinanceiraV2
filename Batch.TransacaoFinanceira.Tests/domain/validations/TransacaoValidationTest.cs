using Batch.TransacaoFinanceira.domain.models;
using Xunit;
using Moq;
using Batch.TransacaoFinanceira.domain.validations;

namespace Batch.TransacaoFinanceira.Tests.domain.validations
{
    public class TransacaoValidationTest
    {
        [Fact]
        public void ValidateEstruturaTest_RetornaStringVaziaQuandoSucesso()
        {
            Transacao transacao = new
            (
                1,
                DateTime.Now,
                100,
                new Conta(123, 1000),
                new Conta(456, 500)
            );
            var resultado = TransacaoValidation.ValidateEstrutura(transacao);
            Assert.Equal(string.Empty, resultado);
        }

        [Fact]
        public void ValidateEstruturaTest_RetornaErroQuandoCodigoInvalido()
        {
            Transacao transacao = new
            (
                0,
                DateTime.Now,
                100,
                new Conta(123, 1000),
                new Conta(456, 500)
            );
            var resultado = TransacaoValidation.ValidateEstrutura(transacao);
            Assert.Equal("Código da Transação inválido", resultado);
        }

        [Fact]
        public void ValidateEstruturaTest_RetornaErroQuandoContaOrigemNula()
        {
            Transacao transacao = new
            (
                1,
                DateTime.Now,
                100,
                null,
                new Conta(456, 500)
            );
            var resultado = TransacaoValidation.ValidateEstrutura(transacao);
            Assert.Equal("Conta de origem inválida", resultado);
        }

        [Fact]
        public void ValidateEstruturaTest_RetornaErroQuandoContaDestinoNula()
        {
            Transacao transacao = new
            (
                1,
                DateTime.Now,
                100,
                new Conta(123, 1000),
                null
            );
            var resultado = TransacaoValidation.ValidateEstrutura(transacao);
            Assert.Equal("Conta de destino inválida", resultado);
        }

        [Fact]
        public void ValidateNegocioTest_RetornaStringVaziaQuandoSucesso()
        {
            Transacao transacao = new
            (
                1,
                DateTime.Now,
                100,
                new Conta(123, 1000),
                new Conta(456, 500)
            );
            var resultado = TransacaoValidation.ValidateNegocio(transacao);
            Assert.Equal(string.Empty, resultado);
        }

        [Fact]
        public void ValidateNegocioTest_RetornaErroQuandoSaldoInsuficiente()
        {
            Transacao transacao = new
            (
                1,
                DateTime.Now,
                1500,
                new Conta(123, 1000),
                new Conta(456, 500)
            );
            var resultado = TransacaoValidation.ValidateNegocio(transacao);
            Assert.Equal("Transacao numero 1 foi cancelada por falta de saldo", resultado);
        }

        [Fact]
        public void ValidateNegocioTest_RetornaErroQuandoContasIguais()
        {
            Transacao transacao = new
            (
                1,
                DateTime.Now,
                100,
                new Conta(123, 1000),
                new Conta(123, 500)
            );
            var resultado = TransacaoValidation.ValidateNegocio(transacao);
            Assert.Equal($"Transacao numero 1 foi cancelada. Conta de origem: {transacao.ContaOrigemTransacao.CodigoConta} e destino: {transacao.ContaDestinoTransacao} não podem ser iguais", resultado);
        }

        [Fact]
        public void ValidateNegocioTest_RetornaErroQuandoValorInvalido()
        {
            Transacao transacao = new
            (
                1,
                DateTime.Now,
                0,
                new Conta(123, 1000),
                new Conta(456, 500)
            );
            var resultado = TransacaoValidation.ValidateNegocio(transacao);
            Assert.Equal("Transacao numero 1 foi cancelada. Valor da transação deve ser maior que zero", resultado);

        }

        [Fact]
        public void ValidateNegocioTest_RetornaErroQuandoDataFutura()
        {
            Transacao transacao = new
            (
                1,
                DateTime.Now.AddHours(1),
                100,
                new Conta(123, 1000),
                new Conta(456, 500)
            );
            var resultado = TransacaoValidation.ValidateNegocio(transacao);
            Assert.Equal($"Transacao numero 1 foi cancelada. Data/Hora da transação {transacao.DataHoraTransacao} é inválida", resultado);

        }

    }
}