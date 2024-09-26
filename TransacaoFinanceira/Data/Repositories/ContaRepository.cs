using System.Collections.Generic;
using TransacaoFinanceira.Domain.Entities;
using TransacaoFinanceira.Domain.Interfaces;

namespace TransacaoFinanceira.Data.Repositories
{
    public class ContaRepository : IContaRepository
    {
        private readonly List<Conta> _contas;

        public ContaRepository()
        {
            _contas = new List<Conta>
            {
                new Conta(938485762, 180),
                new Conta(347586970, 1200),
                new Conta(2147483649, 0),
                new Conta(675869708, 4900),
                new Conta(238596054, 478),
                new Conta(573659065, 787),
                new Conta(210385733, 10),
                new Conta(674038564, 400),
                new Conta(563856300, 1200),
            };
        }

        public Conta GetConta(long idConta)
        {
            return _contas.Find(x => x.IdConta == idConta);
        }

        public void UpdateConta(Conta conta)
        {
            var contaDesatualizada = GetConta(conta.IdConta);
            if (contaDesatualizada != null)
            {
                _contas.Remove(contaDesatualizada);
                _contas.Add(conta);
            }
        }
    }
}