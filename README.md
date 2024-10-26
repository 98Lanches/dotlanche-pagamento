# 💸 Dotlanches Pagamento

[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=98Lanches_dotlanche-pagamento&metric=coverage)](https://sonarcloud.io/summary/new_code?id=98Lanches_dotlanche-pagamento)

Microsserviço de pagamentos Dotlanches. Responsável pelo controle de pagamentos dos pedidos e pela integração com provedores de pagamento.

# Funcionalidades
- Criação de registro de pagamento para um pedido.
- Confirmação de pagamento de pedido via QR Code (web hook).
- Consulta de situação de pagamento de pedido.

Provedores de pagamento disponíveis:
- QR Code: Fake Checkout. Serviço que simula geração de QR Code.

# Stack
- .NET 8.0
- EntityFramework
- Postgresql
- NUnit
- Moq
- Reqnroll

# Como executar o projeto

## Pré-requisitos
- Docker

1. Na raiz do projeto, execute o comando
```
docker compose up
```
2. Acesse o navegador o endereço http://localhost:8080/swagger/index.html
