# FraudSys.Api

## O sistema tem como objetivo gerenciar os limites de transações PIX das contas dos clientes, garantindo que as transações sejam realizadas apenas se o valor estiver dentro do limite disponível. O sistema permite que analistas de fraudes gerenciem e validem esses limites de maneira eficiente e segura.

**Funcionalidades**

**1. Cadastro de Limite**
O analista de fraudes pode cadastrar os seguintes dados no banco de dados:

- Documento (CPF)
- Número da Agência
- Número da Conta
- Limite de Transação PIX

Obs: Todos os campos são obrigatórios para o cadastro.
<br>

**2. Consulta de Limite**
<br>
O analista pode consultar as informações de limite para uma conta já cadastrada, informando o CPF e o número da agência.

**3. Alteração de Limite**
<br>
O analista de fraudes pode alterar o limite de transações PIX de uma conta já cadastrada.

**4. Remoção de Registro**
<br>
O analista pode remover um registro de limite de transação PIX do banco de dados.

**5. Validação de Transações PIX**
<br>
As transações de PIX devem passar pelo sistema de gestão de limites. O sistema verifica se o valor da transação está dentro do limite disponível para o cliente. Se o limite for menor que o valor transacionado, a transação é negada e não consome o limite. Caso contrário, a transação é aprovada e o valor é descontado do limite da conta.

**Tecnologias Utilizadas**
<br>
<br>
.NET 8 para desenvolvimento do backend
<br>
Dynamo AWS para persistência de dados na AWS
<br>
XUnit para testes unitários
<br>
MediatR para mediação de requisições e comandos
<br>
Moq para mocking em testes unitários
<br>

**Testes Unitários**
<br>
O projeto inclui testes unitários utilizando o framework xUnit. Os testes garantem a correta funcionalidade do sistema de gestão de limites.

**Contribuições**
<br>
Se você deseja contribuir para o projeto, fique à vontade para criar um fork, fazer melhorias e enviar um pull request.

**Importante**
<br>
Para utilizar o Dynamo AWS, criei uma imagem docker com sua instância e para realizar os testes da API, utilizei o Rancher Desktop e apenas rodava a imagem do banco de dados.
