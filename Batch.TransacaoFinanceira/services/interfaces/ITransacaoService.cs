using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Batch.TransacaoFinanceira.domain.models;

namespace Batch.TransacaoFinanceira.services.interfaces
{
    public interface ITransacaoService
    {
        Task ProcessarTransacoes(string caminhoArquivo);

    }
}