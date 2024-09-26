using Moq;
using TransacaoFinanceira.Application.DTOs;
using TransacaoFinanceira.Domain.Entities;
using TransacaoFinanceira.Domain.Interfaces;
using TransacaoFinanceira.Domain.Services;

namespace TransacaoFinanceira.Tests
{
    [TestFixture]
    public class TransacaoServiceTests
    {
        private TransacaoService _transacaoService;
        private Mock<IContaRepository> _mockContaRepository;

        [SetUp]
        public void Setup()
        {
            _mockContaRepository = new Mock<IContaRepository>();
            _transacaoService = new TransacaoService(_mockContaRepository.Object);
        }

        [Test]
        public void Transferir_ContaOrigemNaoExistente_CancelarTransacao()
        {
            var transacao = new TransacaoDTO(1, "09/09/2023 14:15:00", 123, 456, 100);

            _mockContaRepository.Setup(repo => repo.GetConta(transacao.IdContaOrigem)).Returns((Conta)null);

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                _transacaoService.Transferir(transacao);

                var resultado = sw.ToString().Trim();
                Assert.That(resultado, Is.EqualTo("Conta origem 123 não encontrada."));
            }
        }

        [Test]
        public void Transferir_ContaDestinoNaoExistente_CancelarTransacao()
        {
            var transacao = new TransacaoDTO(1, "09/09/2023 14:15:00", 123, 456, 100);

            var contaOrigem = new Conta(123, 200);
            _mockContaRepository.Setup(repo => repo.GetConta(transacao.IdContaOrigem)).Returns(contaOrigem);
            _mockContaRepository.Setup(repo => repo.GetConta(transacao.IdContaDestino)).Returns((Conta)null);

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                _transacaoService.Transferir(transacao);

                var resultado = sw.ToString().Trim();
                Assert.That(resultado, Is.EqualTo("Conta destino 456 não encontrada."));
            }
        }

        [Test]
        public void Transferir_SaldoInsuficiente_CancelarTransacao()
        {
            var transacao = new TransacaoDTO(1, "09/09/2023 14:15:00", 123, 456, 300);

            var contaOrigem = new Conta(123, 200);
            var contaDestino = new Conta(456, 100);
            _mockContaRepository.Setup(repo => repo.GetConta(transacao.IdContaOrigem)).Returns(contaOrigem);
            _mockContaRepository.Setup(repo => repo.GetConta(transacao.IdContaDestino)).Returns(contaDestino);

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                _transacaoService.Transferir(transacao);

                var resultado = sw.ToString().Trim();
                Assert.That(resultado, Is.EqualTo("Transação nº 1 foi cancelada por falta de saldo."));
            }
        }

        [Test]
        public void Transferir_ContaOrigemIgualContaDestino_CancelarTransacao()
        {
            var transacao = new TransacaoDTO(1, "09/09/2023 14:15:00", 123, 123, 100);

            var contaOrigem = new Conta(123, 200);
            _mockContaRepository.Setup(repo => repo.GetConta(transacao.IdContaOrigem)).Returns(contaOrigem);
            _mockContaRepository.Setup(repo => repo.GetConta(transacao.IdContaDestino)).Returns(contaOrigem);

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                _transacaoService.Transferir(transacao);

                var resultado = sw.ToString().Trim();
                Assert.That(resultado, Is.EqualTo("Transação nº 1 foi cancelada devido a conta origem ser igual a conta destino."));
            }
        }

        [Test]
        public void Transferir_TransacaoValida_EfetivarTransacao()
        {
            var transacao = new TransacaoDTO(1, "09/09/2023 14:15:00", 123, 456, 100);

            var contaOrigem = new Conta(123, 200);
            var contaDestino = new Conta(456, 100);
            _mockContaRepository.Setup(repo => repo.GetConta(transacao.IdContaOrigem)).Returns(contaOrigem);
            _mockContaRepository.Setup(repo => repo.GetConta(transacao.IdContaDestino)).Returns(contaDestino);

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                _transacaoService.Transferir(transacao);

                var resultado = sw.ToString().Trim();
                Assert.That(resultado, Is.EqualTo($"Transação nº 1 foi efetivada com sucesso! Novos saldos: Conta Origem: 100 | Conta Destino: 200"));
            }
        }
    }
}