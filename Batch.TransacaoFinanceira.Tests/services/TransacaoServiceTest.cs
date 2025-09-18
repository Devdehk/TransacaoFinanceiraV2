using Xunit;
using Moq;
using Batch.TransacaoFinanceira.domain.dto;
using Batch.TransacaoFinanceira.domain.mappers;
using Batch.TransacaoFinanceira.services;
using Batch.TransacaoFinanceira.services.interfaces;
using Microsoft.Extensions.Logging;

namespace Batch.TransacaoFinanceira.Tests.services
{
    public class TransacaoServiceTest
    {
        private readonly TransacaoService _transacaoService;

        public TransacaoServiceTest()
        {
            var loggerMock = new Mock<ILogger<TransacaoService>>();
            var contaServiceMock = new Mock<IContaService>();
            var transacaoMapperMock = new Mock<TransacaoMapper>(contaServiceMock.Object, new Mock<ILogger<TransacaoMapper>>().Object);

            _transacaoService = new TransacaoService(loggerMock.Object, transacaoMapperMock.Object, contaServiceMock.Object);
        }

        [Fact]
        public void ProcessarTransacoesTest_SucessoProcessarTransacoes()
        {
            // Arrange
            IList<TransacaoDTO> transacoes = new List<TransacaoDTO>
            {
                new() { correlation_id = 1, datetime = DateTime.Now.AddMinutes(-10), valor = 100, conta_origem = 123, conta_destino = 456 },
                new TransacaoDTO { correlation_id = 2, datetime = DateTime.Now.AddMinutes(-5), valor = 200, conta_origem = 123, conta_destino = 789 }
            };

            // // quando o método LerArquivoTransacao for chamado, retornar a lista de transações mockada
            // var lerArquivoTransacaoMethod = typeof(TransacaoService).GetMethod("LerArquivoTransacao", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            // lerArquivoTransacaoMethod.Invoke(_transacaoService, [It.IsAny<string>()]).ReturnsAsync(transacoes);
        }

        [Fact]
        public void ProcessarTransacoesTest_NenhumaTransacao()
        {
        }

        [Fact]
        public void ProcessarTransacoesTest_TransacaoInvalida()
        {
        }

        [Fact]
        public void LerArquivoTransacaoTest_SucessoLeitura()
        {
        }

        [Fact]
        public void LerArquivoTransacaoTest_ArquivoInvalido()
        {
        }


        
    }
}