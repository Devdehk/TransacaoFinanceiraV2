using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using Microsoft.EntityFrameworkCore;

namespace Batch.TransacaoFinanceira.domain.models
{
    [Table("tb_conta")]
    public class Conta
    {
        public Conta(BigInteger codigoConta, decimal saldoConta)
        {
            CodigoConta = codigoConta;
            SaldoConta = saldoConta;
        }

        [Key]
        [Column("cod_conta", TypeName = "number")]
        public BigInteger CodigoConta { get; set; }

        [Column("qtd_saldo_conta", TypeName = "number")]
        public decimal SaldoConta { get; set; }

    }
}