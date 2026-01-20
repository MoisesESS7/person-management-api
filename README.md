![Status](https://img.shields.io/badge/status-em%20desenvolvimento-yellow)

Person Management API

API RESTful desenvolvida em .NET 8, aplicando Clean Architecture, CQRS e boas práticas amplamente adotadas no mercado. O projeto demonstra uma implementação profissional de operações CRUD sobre a entidade Person, com foco em separação de responsabilidades, robustez, escalabilidade e manutenibilidade.

Esta API foi pensada como um projeto de portfólio com nível profissional, refletindo padrões reais utilizados em times .NET modernos.

📌 Visão Geral

A solução contempla validações de regras de negócio, logging estruturado, tratamento de exceções em múltiplas camadas e padrões arquiteturais modernos, evidenciando maturidade técnica no desenvolvimento de APIs RESTful.

Principais objetivos do projeto:

Demonstrar domínio de arquitetura limpa em .NET

Aplicar boas práticas de design e organização de código

Garantir baixo acoplamento e alta coesão entre camadas

Preparar a aplicação para crescimento e evolução

🏗️ Arquitetura

O projeto segue os princípios da Clean Architecture, com responsabilidades bem definidas entre as camadas:

API: Camada de apresentação (Controllers, DTOs, Middlewares, Swagger)

Application: Casos de uso, Commands, Queries, Handlers, validações e regras de aplicação

Domain: Entidades, Value Objects, regras de negócio e contratos

Infrastructure: Persistência, integrações externas e implementações técnicas

Shared: Exceções, resultados, utilitários e contratos compartilhados

Além disso, a aplicação utiliza:

CQRS para separação de leitura e escrita

MediatR para desacoplamento entre camadas

FluentValidation para validações de entrada

✅ Funcionalidades Implementadas

Arquitetura em camadas bem definidas (API, Application, Domain, Infrastructure e Shared)

CRUD completo da entidade Person

Validação de duplicidade de Nome, CPF e RG

Implementação do Repository Pattern, incluindo:

RepositoryExecutor

Políticas de retry com Polly

Logging estruturado com ILogger para rastreabilidade de operações

Tratamento global de exceções via Middleware, seguindo o padrão RFC 7807 (ProblemDetails)

Integração com MongoDB, incluindo:

Índices únicos

Convenções globais

AutoMapper configurado para mapear entre:

Requests → Commands

Commands → Domínio

Domínio → Responses

Swagger / OpenAPI integrado para documentação e testes dos endpoints

Arquivos de HTTP Requests organizados para testes manuais

Tests/Requests/Persons.http

Paginação com retorno de links de navegação entre páginas

Uso do padrão Result para controle de fluxos esperados e regras de negócio

Implementação de CQRS com MediatR e FluentValidation

🔜 Funcionalidades Planejadas

Testes unitários com xUnit

Versionamento de API

Autenticação e autorização

Docker e Docker Compose

Pipeline de CI/CD com GitHub Actions

Implementação de HATEOAS para uma API RESTful mais aderente a padrões de mercado

Mensageria e integração com Google Cloud Pub/Sub

🧪 Testes

Atualmente, o projeto conta com arquivos .http para testes manuais dos endpoints. A implementação de testes automatizados com xUnit faz parte do roadmap.

📄 Documentação

A documentação da API está disponível via Swagger, permitindo:

Visualização dos endpoints

Testes interativos

Análise dos contratos de request/response

👤 Autor

Moisés do Espírito Santo Silva
.NET Developer

LinkedIn: https://www.linkedin.com/in/moises-do-espirito-santo-silva/

📎 Observações

Este projeto foi desenvolvido com foco em boas práticas de mercado, servindo tanto como estudo avançado quanto como projeto de portfólio profissional, demonstrando organização, arquitetura e qualidade de código esperadas em ambientes reais de desenvolvimento .NET.



