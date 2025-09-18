using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Batch.TransacaoFinanceira.domain.models;

namespace Batch.TransacaoFinanceira.data.repositories.interfaces
{
    public interface IContaRepository
    {

        Task CadastrarConta(Conta conta);
        Task CadastrarConta(ICollection<Conta> conta);
        Task<Conta> BuscarContaPorCodigo(BigInteger codigoConta);
        Task AtualizarConta(Conta conta);
    }
}