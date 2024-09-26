using System;
using System.Collections.Generic;
using System.Linq;
using TransacaoFinanceira.Application.DTOs;
using TransacaoFinanceira.Application.Interfaces;
using TransacaoFinanceira.Data.Repositories;
using TransacaoFinanceira.Domain.Interfaces;
using TransacaoFinanceira.Domain.Services;



namespace TransacaoFinanceira
{
    class Program
    {

        static void Main(string[] args)
        {
            IContaRepository contaRepository = new ContaRepository();
            ITransacaoService transacaoService = new TransacaoService(contaRepository);

            TransacaoDTO[] transacoes = [
                new TransacaoDTO (1, "09/09/2023 14:15:00", 938485762, 2147483649, 150),
                new TransacaoDTO (2, "09/09/2023 14:15:05", 2147483649, 210385733, 149),
                new TransacaoDTO (3, "09/09/2023 14:15:29", 347586970, 238596054, 1100),
                new TransacaoDTO (4, "09/09/2023 14:17:00", 675869708, 210385733, 5300),
                new TransacaoDTO (5, "09/09/2023 14:18:00", 238596054, 674038564, 1489),
                new TransacaoDTO (6, "09/09/2023 14:18:20", 573659065, 563856300, 49),
                new TransacaoDTO (7, "09/09/2023 14:19:00", 938485762, 2147483649, 44),
                new TransacaoDTO (8, "09/09/2023 14:19:01", 573659065, 675869708, 150),
            ];

            foreach (var transacao in transacoes.OrderBy(t => t.CorrelationId))
            {
                new TransacaoService(contaRepository).Transferir(transacao);
            }
        }
    }

}
