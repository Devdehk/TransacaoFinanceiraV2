using Batch.TransacaoFinanceira.domain.dto;
using Batch.TransacaoFinanceira.domain.mappers;
using Batch.TransacaoFinanceira.domain.models;
using Batch.TransacaoFinanceira.services.interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Batch.TransacaoFinanceira.services
{
    public class TransacaoService : ITransacaoService
    {

        private readonly ILogger<TransacaoService> _logger;
        private readonly TransacaoMapper _transacaoMapper;
        private readonly IContaService _contaService;

        public TransacaoService(ILogger<TransacaoService> logger, TransacaoMapper transacaoMapper, IContaService contaService)
        {
            _logger = logger;
            _transacaoMapper = transacaoMapper;
            _contaService = contaService;
        }


        // Metodo principal para processar as transações a partir de um arquivo JSON
        public async Task ProcessarTransacoes(string caminhoArquivo)
        {
            IList<TransacaoDTO> transacoesArquivo = await LerArquivoTransacao(caminhoArquivo);

            // Processando transações uma por vez
            // O uso do Parallel.ForEach pode ser considerado, porém necessita de cuidados com concorrência na atualização dos saldos das contas
            // Necessário implementar Factory, resultando em múltiplas instâncias de ContaService e TransacaoMapper para evitar conflitos, podendo impactar na performance
            foreach (TransacaoDTO transacaoDTO in transacoesArquivo)
            {
                // Mappper onde é mapeado o DTO para a entidade de domínio e feitas as validações
                Transacao? transacao = await _transacaoMapper.Mapper(transacaoDTO);
                if (transacao != null)
                {
                    // Atualização dos saldos das contas envolvidas na transação
                    transacao.ContaOrigemTransacao.SaldoConta -= transacao.ValorTransacao;
                    transacao.ContaDestinoTransacao.SaldoConta += transacao.ValorTransacao;
                    await _contaService.AtualizarConta(transacao.ContaDestinoTransacao);
                    await _contaService.AtualizarConta(transacao.ContaOrigemTransacao);

                    _logger.LogInformation($"Transação número {transacao.CodigoTransacao} foi efetivada com sucesso! Novos saldos: Conta Origem: {transacao.ContaOrigemTransacao.SaldoConta} | Conta Destino: {transacao.ContaDestinoTransacao.SaldoConta}");
                }
            }
        }

        // Método para ler e desserializar o arquivo JSON de transações
        private async Task<IList<TransacaoDTO>> LerArquivoTransacao(string caminhoArquivo)
        {
            try
            {
                var json = File.ReadAllText(caminhoArquivo);
                var transacao = JsonConvert.DeserializeObject<IList<TransacaoDTO>>(json) ?? throw new JsonException("O arquivo JSON está vazio ou malformado.");
                return transacao;
            }
            catch (Exception ex)
            {
                if (ex is JsonException || ex is IOException)
                {
                    _logger.LogError($"Erro ao ler ou desserializar o arquivo de transação: {caminhoArquivo}", ex);
                    throw;
                }
                else
                {
                    _logger.LogError($"Erro inesperado ao processar o arquivo de transação: {caminhoArquivo}", ex);
                    throw;
                }
            }
        }
    }
}