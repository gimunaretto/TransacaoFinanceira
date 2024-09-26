using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransacaoFinanceira.Application.DTOs
{
    public class TransacaoDTO
    {
        public int CorrelationId { get; set; }
        public string DataHora { get; set; }
        public long IdContaOrigem { get; set; }
        public long IdContaDestino { get; set; }
        public decimal ValorTransacao { get; set; }

        public TransacaoDTO(int correlationId, string dataHora, long idContaOrigem, long idContaDestino, decimal valorTransacao)
        {
            CorrelationId = correlationId;
            DataHora = dataHora;
            IdContaOrigem = idContaOrigem;
            IdContaDestino = idContaDestino;
            ValorTransacao = valorTransacao;
        }
    }
}
