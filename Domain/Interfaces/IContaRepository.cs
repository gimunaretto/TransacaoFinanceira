using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransacaoFinanceira.Domain.Entities;

namespace TransacaoFinanceira.Domain.Interfaces
{
    public interface IContaRepository
    {
        Conta GetConta(long idConta);
        void UpdateConta(Conta conta);
    }
}
