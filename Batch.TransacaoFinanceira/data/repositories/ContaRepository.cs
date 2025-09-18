using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Batch.TransacaoFinanceira.data.database;
using Batch.TransacaoFinanceira.data.repositories.interfaces;
using Batch.TransacaoFinanceira.domain.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Batch.TransacaoFinanceira.data.repositories
{
    public class ContaRepository : IContaRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ContaRepository> _logger;

        public ContaRepository(AppDbContext context, ILogger<ContaRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Conta> BuscarContaPorCodigo(BigInteger codigoConta)
        {
            try
            {
                return await _context.Contas.Where(c => c.CodigoConta == codigoConta).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task CadastrarConta(Conta conta)
        {
            try
            {
                _context.Contas.Add(conta);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task CadastrarConta(ICollection<Conta> conta)
        {
            try
            {
                _context.Contas.AddRange(conta);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task AtualizarConta(Conta conta)
        {
            try
            {
                var contaAtualizada = _context.Contas.Update(conta);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}