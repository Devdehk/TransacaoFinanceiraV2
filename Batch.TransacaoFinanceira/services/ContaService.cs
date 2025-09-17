using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Batch.TransacaoFinanceira.data.repositories.interfaces;
using Batch.TransacaoFinanceira.domain.models;
using Batch.TransacaoFinanceira.services.interfaces;
using Microsoft.Extensions.Logging;

namespace Batch.TransacaoFinanceira.services
{
    public class ContaService : IContaService
    {

        private readonly IContaRepository _repository;

        private readonly ILogger<ContaService> _logger;


        public ContaService(IContaRepository repository, ILogger<ContaService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Conta> BuscarContaPorCodigo(BigInteger codigoConta)
        {
            return await _repository.BuscarContaPorCodigo(codigoConta);
        }

        public async Task CadastrarConta(Conta conta)
        {
            await _repository.CadastrarConta(conta);
        }

        public async Task CadastrarConta(ICollection<Conta> conta)
        {
            await _repository.CadastrarConta(conta);
        }
    }
}