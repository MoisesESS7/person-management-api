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

├── Api
│ ├── Controllers
│ ├── Requests
│ ├── Responses
│ └── Mappers
├── Application
│ ├── Commands
│ ├── Interfaces
│ ├── Services
│ └── Mappers
├── Domain
│ ├── Entities
│ ├── ValueObjects
│ └── Exceptions
├── Infrastructure
│ ├── Data
│ │ ├── Context
│ │ ├── Extensions
│ │ └── Indexes
│ ├── Repositories
│ ├── Common
│ └── Exceptions
├── Shared
│ ├── Enums
│ ├── Exceptions
│ └── Utils
└── Tests
└── Requests
└── Persons.http

markdown
Copiar código

**Observações:**

- `Api`: camada de apresentação, responsável pelos endpoints.
- `Application`: regras de negócio, serviços, validações e mapeamentos.
- `Domain`: entidades e lógica de domínio.
- `Infrastructure`: persistência de dados, repositórios, MongoDB e exceções de infraestrutura.
- `Shared`: classes utilitárias, enums e exceções comuns.
- `Tests/Requests`: arquivos HTTP para testes rápidos via VSCode ou Thunder Client.

---

## Funcionalidades

- CRUD completo para **Person**
- Validações de domínio (CPF, RG, nome)
- Tratamento de exceções centralizado (`ExceptionHandlingMiddleware`)
- Logging estruturado usando `ILogger`
- Swagger/OpenAPI configurado
- Serialização JSON com `JsonHelper`
- Repositório genérico com retry policy usando **Polly**
- Implementação de índices no MongoDB (`MongoIndexInitializer`)

---

## Endpoints de Teste

Todos os endpoints podem ser testados através de `Tests/Requests/Persons.http`:

- **GET /api/persons**: Lista todas as pessoas
- **GET /api/persons/{id}**: Busca pessoa por ID
- **POST /api/persons**: Cria uma nova pessoa
- **PUT /api/persons/{id}**: Atualiza pessoa existente
- **DELETE /api/persons/{id}**: Deleta pessoa existente

---

## Como Executar

1. Clonar o repositório
```bash
git clone <repo-url>
cd <repo-folder>
Configurar o appsettings.json com MongoDB e URLs necessárias.

Executar a API:

bash
Copiar código
dotnet run --project Api
Acessar Swagger para testes:

bash
Copiar código
https://localhost:{port}/swagger/index.html
Boas Práticas Aplicadas
Clean Architecture para separação de responsabilidades

CQRS (em planejamento) para separação de comandos e consultas

Injeção de dependência com IServiceCollection

Mappers centralizados para conversão entre Requests, Commands e Responses

Tratamento de exceções centralizado usando Middleware

Logs estruturados para monitoramento

Retry policy com Polly em operações de banco

Próximos Passos
Implementar CQRS com MediatR

Adicionar integração completa com GCP Pub/Sub

Criar testes unitários e de integração

Configurar pipelines de CI/CD (GitHub Actions ou Azure DevOps)

Contato
Moisés do Espírito Santo Silva
Email: moisessilvagsp@outlook.com
https://github.com/MoisesESS7/

Referências
Clean Architecture

ASP.NET Core Documentation

MongoDB C# Driver

AutoMapper Documentation

Polly Documentation

yaml
Copiar código

---

Se você quiser, posso também gerar uma **versão resumida para o GitHub**, com badges de build, cobertura e .NET, que dá um toque mais profissional e moderno para o README.  

Quer que eu faça isso também?
