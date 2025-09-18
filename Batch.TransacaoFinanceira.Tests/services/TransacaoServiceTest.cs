using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Batch.TransacaoFinanceira.domain.dto;
using Batch.TransacaoFinanceira.domain.models;
using Batch.TransacaoFinanceira.domain.mappers;
using Batch.TransacaoFinanceira.services;
using Batch.TransacaoFinanceira.services.interfaces;
using Microsoft.Extensions.Logging;
using System.IO;
using Newtonsoft.Json;
using Castle.Core;

namespace Batch.TransacaoFinanceira.Tests.services
{
    public class TransacaoServiceTest
    {
        [Fact]
        public async Task ProcessarTransacoes_DeveAtualizarSaldosDasContas()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<TransacaoService>>();
            var contaServiceMock = new Mock<IContaService>();
            var transacaoMapperMock = new Mock<TransacaoMapper>(null);

            var contaOrigem = new Conta(1, 1000);
            var contaDestino = new Conta(2, 500);

            var transacaoDTO = new TransacaoDTO
            {
                correlation_id = 1,
                conta_origem = 1,
                conta_destino = 2,
                valor = 200
            };

            var transacao = new Transacao(
                1,
                DateTime.Now,
                200,
                contaOrigem,
                contaDestino
            );

            transacaoMapperMock
                .Setup(m => m.Mapper(It.IsAny<TransacaoDTO>()))
                .ReturnsAsync(transacao);

            contaServiceMock
                .Setup(m => m.AtualizarConta(It.IsAny<Conta>()))
                .Returns(Task.CompletedTask);

            var tempFile = Path.GetTempFileName();
            File.WriteAllText(tempFile, JsonConvert.SerializeObject(new List<TransacaoDTO> { transacaoDTO }));

            var service = new TransacaoService(
                loggerMock.Object,
                transacaoMapperMock.Object,
                contaServiceMock.Object
            );

            // Act
            await service.ProcessarTransacoes(tempFile);

            // Assert
            Assert.Equal(800, contaOrigem.SaldoConta);
            Assert.Equal(700, contaDestino.SaldoConta);

            contaServiceMock.Verify(m => m.AtualizarConta(contaOrigem), Times.Once);
            contaServiceMock.Verify(m => m.AtualizarConta(contaDestino), Times.Once);

            File.Delete(tempFile);
        }

        [Fact]
        public async Task ProcessarTransacoes_ArquivoJsonMalformado_DeveLancarExcecao()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<TransacaoService>>();
            var contaServiceMock = new Mock<IContaService>();
            var transacaoMapperMock = new Mock<TransacaoMapper>(null);

            var tempFile = Path.GetTempFileName();
            File.WriteAllText(tempFile, "arquivo_invalido");

            var service = new TransacaoService(
                loggerMock.Object,
                transacaoMapperMock.Object,
                contaServiceMock.Object
            );

            // Act & Assert
            await Assert.ThrowsAsync<JsonException>(() => service.ProcessarTransacoes(tempFile));

            File.Delete(tempFile);
        }
    }
}