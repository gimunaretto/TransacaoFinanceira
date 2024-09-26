using TransacaoFinanceira.Domain.Entities;

namespace TransacaoFinanceira.Domain.Interfaces
{
    public interface IContaRepository
    {
        Conta GetConta(long idConta);

        void UpdateConta(Conta conta);
    }
}