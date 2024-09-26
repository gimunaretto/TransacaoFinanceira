using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransacaoFinanceira.Application.DTOs;
using TransacaoFinanceira.Application.Interfaces;
using TransacaoFinanceira.Domain.Entities;
using TransacaoFinanceira.Domain.Interfaces;

namespace TransacaoFinanceira.Domain.Services
{
    public class TransacaoService : ITransacaoService
    {
        private readonly IContaRepository _contaRepository;

        public TransacaoService(IContaRepository contaRepository)
        {
            _contaRepository = contaRepository;
        }

        private static (bool, string) ValidarTransacao(Transacao transacao, Conta contaOrigem, Conta contaDestino)
        {

            if (contaOrigem == null || contaDestino == null)
            {
                string tipoConta = (contaOrigem == null ? "origem" : "destino");
                long contaId = (contaOrigem == null ? transacao.IdContaOrigem : transacao.IdContaDestino);

                return (false, $"Conta {tipoConta} {contaId} não encontrada.");
            }

            if (contaOrigem.Saldo < transacao.ValorTransacao)
            {
                return (false, $"Transação nº {transacao.CorrelationId} foi cancelada por falta de saldo.");
            }

            if (contaOrigem.IdConta.Equals(contaDestino.IdConta))
            {
                return (false, $"Transação nº {transacao.CorrelationId} foi cancelada devido a conta origem ser igual a conta destino.");
            }

            return (true, string.Empty);
        }

        public void Transferir(TransacaoDTO transacaoDTO)
        {
            var transacao = MapearTransacaoDTO(transacaoDTO);
            var contaOrigem = _contaRepository.GetConta(transacao.IdContaOrigem);
            var contaDestino = _contaRepository.GetConta(transacao.IdContaDestino);

            var (transacaoValida, mensagemValidacao) = ValidarTransacao(transacao, contaOrigem, contaDestino);

            if (!transacaoValida)
            {
                Console.WriteLine(mensagemValidacao);
                return;
            }

            contaOrigem.Descontar(transacao.ValorTransacao);
            contaDestino.Receber(transacao.ValorTransacao);

            _contaRepository.UpdateConta(contaOrigem);
            _contaRepository.UpdateConta(contaDestino);

            Console.WriteLine($"Transação nº {transacao.CorrelationId} foi efetivada com sucesso! " +
                              $"Novos saldos: Conta Origem: {contaOrigem.Saldo} | Conta Destino: {contaDestino.Saldo}");

        }

        private Transacao MapearTransacaoDTO(TransacaoDTO transacaoDTO)
        {
            return new Transacao(
               transacaoDTO.CorrelationId,
               transacaoDTO.DataHora,
                 transacaoDTO.IdContaOrigem,
               transacaoDTO.IdContaDestino,
                transacaoDTO.ValorTransacao
            );
        }
    }
}
