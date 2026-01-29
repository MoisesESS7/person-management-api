![Status](https://img.shields.io/badge/status-em%20desenvolvimento-yellow?style=for-the-badge)
![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![CSharp](https://img.shields.io/badge/C%23-12-239120?style=for-the-badge&logo=csharp&logoColor=white)
![Architecture](https://img.shields.io/badge/Architecture-Clean%20Architecture-blue?style=for-the-badge)
![CQRS](https://img.shields.io/badge/Pattern-CQRS-informational?style=for-the-badge)
![MediatR](https://img.shields.io/badge/MediatR-Enabled-orange?style=for-the-badge)
![MongoDB](https://img.shields.io/badge/MongoDB-Integrated-47A248?style=for-the-badge&logo=mongodb&logoColor=white)
![Swagger](https://img.shields.io/badge/OpenAPI-Swagger-85EA2D?style=for-the-badge&logo=swagger&logoColor=black)
![Docker](https://img.shields.io/badge/Docker-Planned-2496ED?style=for-the-badge&logo=docker&logoColor=white)
![CI/CD](https://img.shields.io/badge/CI%2FCD-GitHub%20Actions-lightgrey?style=for-the-badge&logo=githubactions)

---

# Person Service API

Este repositório contém um microsserviço backend desenvolvido em .NET 8, responsável pelo domínio **Person**, aplicando **Clean Architecture**, **CQRS**, **testes unitários com xUnit** e princípios de separação de responsabilidades utilizados em ambientes corporativos.

A solução implementa operações de gestão de pessoas com foco em **robustez**, **manutenibilidade** e **escalabilidade**, refletindo práticas adotadas por times .NET modernos em **sistemas distribuídos**.

---

## 📌 Visão Geral

A solução contempla **validações de regras de negócio**, **logging estruturado**, **tratamento global de exceções** e **padrões arquiteturais modernos**, evidenciando **maturidade técnica** no desenvolvimento de **APIs RESTful**.

**Objetivos do projeto:**

- Aplicar **Clean Architecture** de forma pragmática
- Garantir **baixo acoplamento** e **alta coesão**
- Demonstrar **boas práticas de mercado em .NET**
- Preparar a aplicação para **crescimento, testes e automação**

---

## 💼 Perfil Profissional

Experiência prática aplicada neste projeto:

- **ASP.NET Core (.NET 8)**
- **APIs RESTful** orientadas a domínio
- **Clean Architecture** e **CQRS**
- **MediatR** para orquestração de casos de uso
- **MongoDB** como banco NoSQL
- **FluentValidation**, **logging estruturado** e **tratamento de exceções**
- Preparação para **microsserviços**, **CI/CD** e **Docker**

---

## 🏗 Decisões de Arquitetura e Nomeação

O projeto foi estruturado para atuar como serviço independente dentro de um **ecossistema distribuído**, seguindo os princípios de **Clean Architecture** e **CQRS**.

Todos os projetos utilizam o prefixo **`PersonService`**, tornando o **limite do serviço explícito** e alinhado às práticas reais de **microsserviços no ecossistema .NET**.

---

### Por que `PersonService`?

- Representa uma **capacidade de negócio**
- Unidade clara de:
  - **deploy**
  - **versionamento**
  - **CI/CD**
  - **testes**
- Recursos da API permanecem no plural (ex.: `/api/persons`)

---

## 📁 Estrutura da Solução

```text
PersonService.sln
├── src
│   ├── PersonService.Api
│   ├── PersonService.Application
│   ├── PersonService.Domain
│   ├── PersonService.Infra.Data
│   ├── PersonService.Infra.Ioc
│   └── PersonService.Shared
└── tests
    └── PersonService.Tests
```

Essa estrutura permite extração do serviço para repositório ou pipeline dedicado, sem necessidade de refatoração.

---

## 🏗️ Arquitetura

O projeto segue os princípios da Clean Architecture, com responsabilidades bem definidas:

- **PersonService.API:** Controllers, DTOs, Middlewares e Swagger

- **PersonService.Application:** Commands, Queries, Handlers, validações e regras de aplicação

- **PersonService.Domain:** Entidades, Value Objects, regras de negócio e contratos

- **PersonService.Infra.Data:** Persistência, integrações externas e implementações técnicas

- **PersonService.Infra.Ioc:** Injeção de dependências e configuração de serviços

- **PersonService.Shared:** Exceções, Results, utilitários e contratos compartilhados

---

## 🧠 Tecnologias e padrões aplicados

- **CQRS** (separação de leitura e escrita)

- **MediatR** (desacoplamento entre camadas)

- **FluentValidation**

- **AutoMapper**

- **ILogger / Logging estruturado**

- **Polly**

- **RFC 7807 (Problem Details)**

---

## 🧪 Estratégia de Testes

A estratégia de testes foi pensada para ambientes reais de produção, contemplando testes unitários e de integração.

**Estrutura planejada de testes:**

```text
tests
├── PersonService.Tests
│   ├── Unit
│   │   ├── Domain
│   │   ├── Application
│   │   └── Api
│   ├── Integration
│   │   └── Api
│   └── Common
│       ├── Builders
│       ├── Fixtures
│       └── Fakes

```

- **Testes Unitários:** regras de negócio, handlers, validações

- **Testes de Integração:** API + banco de dados

- **Fixtures e Builders:** isolamento e reutilização

- **xUnit:** como framework de testes

Arquivos .http também são utilizados para testes manuais e exploratórios.

---

## 🚀 CI/CD & Docker (Planejado)

A aplicação está preparada para automação completa de build, testes e deploy.

**Planejamento de CI/CD:**

- GitHub Actions

**Pipelines separados para:**

- Build

- Testes

- Docker

**Execução automática de:**

- Testes unitários

- Testes de integração

- Análise de build

**Docker & Containers:**

- Dockerfile para a API

**Docker Compose para:**

- API

- MongoDB

**Preparação para ambientes:**

- dev

- staging

- production

Essa abordagem facilita deploy contínuo, observabilidade e escalabilidade.

---

## ✅ Funcionalidades Implementadas

- Operações essenciais de gestão da entidade Person

- Validação de duplicidade de Nome, CPF e RG

- Repository Pattern com RepositoryExecutor

- Retry com Polly

- Logging estruturado com ILogger

- Tratamento global de exceções (RFC 7807)

- Integração com MongoDB

- AutoMapper configurado

- Swagger / OpenAPI

- Paginação com links de navegação

- Uso do padrão Result

---

## 🔜 Funcionalidades Planejadas

- Testes automatizados (unitários e integração)

- Versionamento de API

- Autenticação e autorização

- Docker e Docker Compose

- CI/CD com GitHub Actions

- Implementação de HATEOAS

- Mensageria com Google Cloud Pub/Sub

---

## 📦 Requisitos

- .NET SDK 8+
- MongoDB
- Git

---

## 👤 Autor

**Moisés do Espírito Santo Silva**  
**.NET Backend Developer**

[![LinkedIn](https://img.shields.io/badge/LinkedIn-blue?logo=linkedin&logoColor=white)](https://www.linkedin.com/in/moises-do-espirito-santo-silva/)

---

## 📎 Observações

Este projeto demonstra práticas reais de mercado aplicadas ao desenvolvimento de APIs .NET, com foco em arquitetura, organização e qualidade de código.
