# Projeto: API Web para Gerenciamento de Produtos e Produção

## Visão Geral
Este projeto oferece uma API Web para o gerenciamento de produtos e processos de produção. Inclui endpoints para recuperar informações de produtos, gerenciar pedidos de produção, iniciar, finalizar e cancelar processos de produção.

## Tecnologias Utilizadas
- **ASP.NET Core**: Framework para construção de APIs.
- **C#**: Linguagem utilizada para lógica backend.
- **Entity Framework Core**: Para acesso aos dados e interação com o banco de dados.
- **Swagger**: Ferramenta de documentação de API.
- **Serilog**: Para logging estruturado.

## Estrutura
O projeto é composto por vários componentes:
- **Controllers**: Gerenciam requisições HTTP recebidas e delegam para os serviços correspondentes.
- **Services**: Implementam a lógica de negócio e interagem com os repositórios.
- **DTOs**: Objetos de Transferência de Dados utilizados para transferir dados entre camadas.
- **Repositories**: Interfaces e classes para operações de acesso aos dados.

## Controladores

### ProductController
Gerencia endpoints relacionados ao gerenciamento de produtos.

- **GET /api/produto**
  - Recupera todos os produtos com seus ingredientes.
  - Retorna HTTP 200 OK em caso de sucesso com uma lista de produtos.
  - Retorna HTTP 400 Bad Request em caso de falha com uma mensagem de erro.

- **GET /api/produto/{productId}/ingredientes**
  - Recupera os ingredientes de um produto específico identificado pelo productId.
  - Retorna HTTP 200 OK em caso de sucesso com os detalhes do produto.
  - Retorna HTTP 400 Bad Request em caso de falha com uma mensagem de erro.

### ProductionController
Gerencia processos e pedidos de produção.

- **POST /api/producao/ReceberPedido**
  - Recebe um pedido para produção.
  - Aceita um payload JSON do tipo ReceivingOrderDto.
  - Retorna HTTP 200 OK em caso de sucesso com uma mensagem de sucesso.
  - Retorna HTTP 400 Bad Request em caso de falha com uma mensagem de erro.

- **POST /api/producao/IniciarProducao**
  - Inicia a produção para um productionId específico.
  - Retorna HTTP 200 OK em caso de sucesso com uma mensagem de sucesso.
  - Retorna HTTP 400 Bad Request em caso de falha com uma mensagem de erro.

- **POST /api/producao/FinalizarProducao**
  - Marca a produção como finalizada para um productionId específico.
  - Retorna HTTP 200 OK em caso de sucesso com uma mensagem de sucesso.
  - Retorna HTTP 400 Bad Request em caso de falha com uma mensagem de erro.

- **POST /api/producao/CancelarProducao**
  - Cancela a produção para um productionId específico.
  - Retorna HTTP 200 OK em caso de sucesso com uma mensagem de sucesso.
  - Retorna HTTP 400 Bad Request em caso de falha com uma mensagem de erro.

## Configuração e Uso
Para rodar o projeto localmente:

1. Clone este repositório.
2. Abra o projeto na sua IDE preferida.
3. Certifique-se de ter o .NET SDK instalado.
4. Configure a conexão com o banco de dados conforme necessário.
5. Execute a aplicação e utilize o Swagger para explorar os endpoints disponíveis.

## Contribuição
Contribuições são bem-vindas! Sinta-se à vontade para propor melhorias via pull requests.

![Imagem do Projeto](https://github.com/user-attachments/assets/9d67a6f6-5da1-4138-9fb7-bb332ab2eceb)
