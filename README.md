# TransacaoFinanceira

Case para refatoração

## Passos a implementar:

1. Corrija o que for necessário para resolver os erros de compilação.
2. Execute o programa para avaliar a saída, identifique e corrija o motivo de algumas transações estarem sendo canceladas mesmo com saldo positivo e outras sem saldo sendo efetivadas.
3. Aplique o code review e refatore conforme as melhores práticas (SOLID, Patterns, etc.).
4. Implemente os testes unitários que julgar efetivos.
5. Crie um GitHub e compartilhe o link respondendo ao último e-mail.

**Obs:** Você é livre para implementar na linguagem de sua preferência, desde que respeite as funcionalidades e saídas existentes, além de aplicar os conceitos solicitados.

## Identificação e resolução dos erros de compilação

### Erros Identificados

1. Target Framework;
![Falha Build Target](https://github.com/gimunaretto/TransacaoFinanceira/blob/main/img/FalhaBuildTarget.PNG)
2. Erro de matriz;
![Falha Build 2 Implicitly](https://github.com/gimunaretto/TransacaoFinanceira/blob/main/img/FalhaBuild2Implicitly.PNG)
3. Erro de uint e long;
![Falha Build uint convert](https://github.com/gimunaretto/TransacaoFinanceira/blob/main/img/FalhaBuildUintConvert.PNG)

4.Erro de index no Console.WriteLIne.
![Falha Index Console.WriteLine](https://github.com/gimunaretto/TransacaoFinanceira/blob/main/img/ResolucaoIndexConsoleWriteLine.PNG)

### Correções Aplicadas

1. Atualização da versão do Target de 5.0 para 8.0 e execução do comando `dotnet restore` para realizar a restauração.
  ![Resolução Build Target](https://github.com/gimunaretto/TransacaoFinanceira/blob/main/img/ResolucaoBuildTarget.PNG)

2. Criação da classe Transacao e ajuste na inicialização do array, passando como o tipo a nova classe criada.
  ![Falha Build 2 Implicitly](https://github.com/gimunaretto/TransacaoFinanceira/blob/main/img/ResolucaoBuild2Implicitly.PNG)
   
3. Mudança do tipo de dado das variáveis relacionadas ao número da conta de origem e destino de `uint` para `long`.
  ![Resolução Build uint convert](https://github.com/gimunaretto/TransacaoFinanceira/blob/main/img/ResolucaoBuildUintConvert.PNG)

4. Erro de index no Console.WriteLIne: Ajustar o index interpretado que estava apontando para uma posição inexistente (3) para a posição correta (2).
  ![Resolução Index Console.WriteLine](https://github.com/gimunaretto/TransacaoFinanceira/blob/main/img/ResolucaoIndexConsoleWriteLine.PNG)

## Refatoração e correção dos erros de transação

Foi identificado que o código executava as transações em threads separadas devido ao `Parallel.ForEach`, o que ocasionava inconsistências nos dados das transações. A correção foi feita aplicando a instrução `lock`, garantindo que, no máximo, apenas um thread execute seu corpo a qualquer momento.

### Resolução de Erros de Saída

Após executar o programa e avaliar a saída, foram identificados motivos para algumas transações serem canceladas mesmo com saldo positivo e outras sem saldo sendo efetivadas. 
**![Falha Logica Transf Saldo](https://github.com/gimunaretto/TransacaoFinanceira/blob/main/img/FalhaLogicaTransfSaldo.PNG)**

**Resoluções:**

- O `Parallel` estava processando as transações de forma paralela, sem garantir que a ordem correta de criação fosse respeitada. Isso resultava em inconsistências nos saldos das contas, pois as transações eram aplicadas fora de sequência e as atualizações de saldo não estavam sendo devidamente refletidas. Para corrigir esse comportamento e garantir a consistência dos dados, substituí a execução paralela por um loop `foreach`, ordenando as transações pelo `correlation_id`. Dessa forma, assegurei que as transações fossem processadas na ordem correta e os saldos fossem atualizados adequadamente.

**![Resolucao Logica Transf Saldo](https://github.com/gimunaretto/TransacaoFinanceira/blob/main/img/ResolucaoLogicaTransfSaldo.PNG)**
  
### Arquitetura em Camadas

Para atender à demanda de refatoração e aplicação das melhores práticas, ajustes foram realizados em todo o código de forma a separar de maneira adequada cada responsabilidade através da arquitetura em camadas.

- **Domain:** Contém as entidades do negócio, como `Transacao` e `Conta`. Essa camada encapsula as regras de negócio, o que facilita na manutenção do código.
- **Data:** Gerencia a persistência dos dados e consulta das contas. Essa separação permite que a lógica seja alterada sem impactar as demais camadas.
- **Application:** Nela, são definidos os DTOs, como `TransacaoDTO`, que simplificam a transferência de dados entre as camadas e evitam o acoplamento direto com a camada de domínio. Isso permite que alterações em uma camada não impactem a outra. Também é aqui que a interface `ITransacaoService` é definida, estabelecendo um contrato para os serviços de manipulação de transações, o que facilita a realização de testes e a manutenção do código.

### Código e Manutenção

- **Legibilidade e Manutenção:** Código foi modificado de forma a melhorar a legibilidade, seguindo convenções de nomenclatura e separação de responsabilidades utilizando camadas.
- **Tratamento de Erros:** Validação dos dados da transação criado separadamente no serviço, centralizando a lógica de verificação e aumentando a manutenção e controle de qualidade da transação executada.

## Testes Unitários

Foram implementados testes unitários para atender ao quarto tópico de ajustes do projeto. Esses testes foram organizados para cobrir cenários positivos e negativos, garantindo a robustez da aplicação.

### Estrutura dos Testes

- **Cenários Positivos:** Testes foram criados para validar se as transações são processadas corretamente quando os saldos são suficientes. Isso assegura que a lógica de negócios funcione como esperado em situações normais.

- **Cenários Negativos:** Testes foram implementados para verificar se mensagens de erro são lançadas adequadamente quando não há saldo suficiente para a transação. Essa abordagem ajuda a identificar e tratar erros de forma eficaz, melhorando a experiência do usuário.

### Ferramentas Utilizadas

A utilização do **Moq** permitiu a simulação das interações com a camada de acesso a dados. Isso isolou a lógica de negócios durante os testes, facilitando a identificação de falhas e permitindo que os testes focassem no comportamento da aplicação sem depender de implementações reais de acesso a dados.

Esses testes não apenas garantem a integridade do código, mas também ajudam na manutenção futura, uma vez que mudanças no código podem ser rapidamente verificadas quanto à sua correta funcionalidade através da execução dos testes.

