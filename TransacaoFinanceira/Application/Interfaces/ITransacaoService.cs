using TransacaoFinanceira.Application.DTOs;

namespace TransacaoFinanceira.Application.Interfaces
{
    public interface ITransacaoService
    {
        void Transferir(TransacaoDTO transacaoDTO);
    }
}