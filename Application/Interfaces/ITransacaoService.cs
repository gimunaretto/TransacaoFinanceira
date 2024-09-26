using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransacaoFinanceira.Application.DTOs;


namespace TransacaoFinanceira.Application.Interfaces
{
    public interface ITransacaoService
    {
        void Transferir(TransacaoDTO transacaoDTO);
    }
}
