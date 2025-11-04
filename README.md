# PDI - .NET API CRUD com MongoDB e GCP Pub/Sub

## Descrição

Este projeto implementa uma **API RESTful** utilizando **.NET 8**, com **MongoDB** como banco de dados e integração futura com **Google Cloud Pub/Sub**.  
O sistema realiza operações CRUD para a entidade **Person**, aplicando **boas práticas de mercado** como:

- Clean Architecture
- Injeção de dependência
- Validações de domínio
- Tratamento centralizado de exceções
- Logging estruturado
- Swagger/OpenAPI para documentação

> Este projeto foi desenvolvido como portfólio, demonstrando práticas profissionais de desenvolvimento em .NET.

---

## Tecnologias

- .NET 8
- C#
- MongoDB
- AutoMapper
- Polly (retry policy)
- Swagger/OpenAPI
- GCP Pub/Sub (planejado para implementação futura)

---

## Arquitetura

O projeto segue **Clean Architecture**, dividido em camadas:

```text
├── Api
│   ├── Controllers
│   ├── Requests
│   ├── Responses
│   ├── Mappers
│   └── Tests
│       └── Requests
│          └── Persons.http
├── Application
│   ├── Commands
│   ├── Interfaces
│   ├── Services
│   └── Mappers
├── Domain
│   ├── Entities
│   ├── ValueObjects
│   └── Exceptions
├── Infrastructure
│   ├── Data
│   │   ├── Context
│   │   ├── Extensions
│   │   └── Indexes
│   ├── Repositories
│   ├── Common
│   └── Exceptions
├── Shared
    ├── Enums
    ├── Exceptions
    └── Utils



