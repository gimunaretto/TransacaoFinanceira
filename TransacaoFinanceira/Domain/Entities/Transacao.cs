namespace TransacaoFinanceira.Domain.Entities
{
    public class Transacao
    {
        public int CorrelationId { get; set; }
        public string DataHora { get; set; }
        public long IdContaOrigem { get; set; }
        public long IdContaDestino { get; set; }
        public decimal ValorTransacao { get; set; }

        public Transacao(int correlationId, string dataHora, long idContaOrigem, long idContaDestino, decimal valorTransacao)
        {
            CorrelationId = correlationId;
            DataHora = dataHora;
            IdContaOrigem = idContaOrigem;
            IdContaDestino = idContaDestino;
            ValorTransacao = valorTransacao;
        }
    }
}