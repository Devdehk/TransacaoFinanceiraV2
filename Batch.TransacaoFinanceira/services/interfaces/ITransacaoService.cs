using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Batch.TransacaoFinanceira.services.interfaces
{
    public interface ITransacaoService
    {
        Task ProcessarTrasacao();

    }
}