﻿namespace TransacaoFinanceira.Domain.Entities
{
    public class Conta
    {
        public long IdConta { get; set; }
        public decimal Saldo { get; set; }

        public Conta(long idConta, decimal saldo)
        {
            IdConta = idConta;
            Saldo = saldo;
        }

        public void Descontar(decimal valorTransacao)
        {
            Saldo -= valorTransacao;
        }

        public void Receber(decimal valorTransacao)
        {
            Saldo += valorTransacao;
        }
    }
}