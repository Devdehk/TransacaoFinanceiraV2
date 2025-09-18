using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Batch.TransacaoFinanceira.domain.dto
{
    public class TransacaoDTO
    {
        public int correlation_id { get; set; }

        public DateTime datetime { get; set; }

        public BigInteger conta_origem { get; set; }

        public BigInteger conta_destino { get; set; }

        public decimal valor { get; set; }
    }
}