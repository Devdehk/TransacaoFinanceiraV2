using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Batch.TransacaoFinanceira.domain.models;

namespace Batch.TransacaoFinanceira.services.interfaces
{
    public interface IContaService
    {
        Task<Conta> BuscarContaPorCodigo(BigInteger codigoConta);

        Task CadastrarConta(Conta conta);
        Task CadastrarConta(ICollection<Conta> conta);
    }
}