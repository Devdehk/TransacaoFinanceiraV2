using Batch.TransacaoFinanceira.domain.dto;
using Batch.TransacaoFinanceira.domain.models;
using Batch.TransacaoFinanceira.domain.validations;
using Batch.TransacaoFinanceira.services;
using Batch.TransacaoFinanceira.services.interfaces;
using Microsoft.Extensions.Logging;

namespace Batch.TransacaoFinanceira.domain.mappers
{
    public class TransacaoMapper
    {
        private readonly IContaService _contaService;
        private readonly ILogger<TransacaoMapper> _logger;

        public TransacaoMapper(IContaService contaService, ILogger<TransacaoMapper> logger)
        {
            _contaService = contaService;
            _logger = logger;
        }

        // Mapeia o DTO para a entidade de domínio
        public async Task<Transacao>? Mapper(TransacaoDTO dto)
        {
            try
            {
                Transacao transacao = new
                (
                    dto.correlation_id,
                    dto.datetime,
                    dto.valor,
                    await _contaService.BuscarContaPorCodigo(dto.conta_origem),
                    await _contaService.BuscarContaPorCodigo(dto.conta_destino)
                );

                // Validações de estrutura e negócio
                // Se alguma validação falhar, retorna null e registra o erro no log
                // Pode-se considerar lançar exceções específicas para diferentes tipos de erros, dependendo dos requisitos do sistema
                // Pode-se considerar o uso do padrão Strategy para diferentes tipos de validações
                string resposta = TransacaoValidation.ValidateEstrutura(transacao);
                if (!string.IsNullOrEmpty(resposta))
                {
                    _logger.LogError(resposta);
                    return null;
                }

                resposta = TransacaoValidation.ValidateNegocio(transacao);
                if (!string.IsNullOrEmpty(resposta))
                {
                    _logger.LogError(resposta);
                    return null;
                }

                return transacao;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao mapear Transação: {Message}", ex.Message);
                throw;
            }
        }
    }
}