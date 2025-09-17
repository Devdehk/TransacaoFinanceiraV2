namespace Batch.TransacaoFinanceira.domain.models
{
    public class Transacao
    {
        public Transacao(int codigoTransacao, DateTime dataHoraTransacao, decimal valorTransacao, Conta contaOrigemTransacao, Conta contaDestinoTransacao)
        {
            CodigoTransacao = codigoTransacao;
            DataHoraTransacao = dataHoraTransacao;
            ValorTransacao = valorTransacao;
            ContaOrigemTransacao = contaOrigemTransacao;
            ContaDestinoTransacao = contaDestinoTransacao;

        }

        public int CodigoTransacao { get; set; }
        public DateTime DataHoraTransacao { get; set; }
        public decimal ValorTransacao { get; set; }
        public Conta ContaOrigemTransacao { get; set; }
        public Conta ContaDestinoTransacao { get; set; }
    }
}