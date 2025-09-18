using Batch.TransacaoFinanceira.domain.models;

namespace Batch.TransacaoFinanceira.domain.validations
{
    public class TransacaoValidation
    {
        public static string ValidateEstrutura(Transacao transacao)
        {

            if (transacao.CodigoTransacao <= 0)
                return "Código da Transação inválido";

            if (transacao.ContaOrigemTransacao == null)
                return "Conta de origem inválida";

            if (transacao.ContaDestinoTransacao == null)
                return "Conta de destino inválida";

            if (transacao.ValorTransacao <= 0)
                return "Valor da transação deve ser maior que zero";

            return string.Empty;
            
        }

        public static string ValidateNegocio(Transacao transacao)
        {
            if (transacao.ContaOrigemTransacao.CodigoConta == transacao.ContaDestinoTransacao.CodigoConta)
                return $"Conta de origem: {transacao.ContaOrigemTransacao} e destino: {transacao.ContaDestinoTransacao} não podem ser iguais";

            if (transacao.ContaOrigemTransacao.SaldoConta < transacao.ValorTransacao)
                return $"Transacao numero {transacao.CodigoTransacao} foi cancelada por falta de saldo";
               
            return string.Empty;
        }
    }
}