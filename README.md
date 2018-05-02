### Simple CRUD API

This project demonstrates production-level code I write in everyday work.
Simple task includes:
1. Production-ready Api infrastructure
2. CRUD functionality of primitive Web shop for Products and Sales combinations

The API implemented utilizing classic N-tier application architecture and TDD approach.
Every row of the code writen test-first.

***Used tools and open-source dependencies :***
### Infrustructure
- The Service utilizes `Autofac` nuget for Dependency injection
- Exceptions are handled and logged using `NLog`
- Authorization implemented via `Auth0` jwt tokens and proved by [integration tests](https://github.com/SergeyVolodko/CoolBlueTask/blob/master/CoolBlueTask.Tests/AuthorizationIntegrationTests.cs)
- Configurations consumed from `Web.config`

### Functionality implementation
- Testing libs `xUnit`, `AutoFixture`, `NSubstitute` and `ApprovalTests`
- Mappings handled using `AutoMapper` lib
- Validations implemented with `FluentValidations`
- Repository layer works against SQLite database (InMemory for better flexibility). Functionality proved with [integration tests](https://github.com/SergeyVolodko/CoolBlueTask/blob/master/CoolBlueTask.Tests/Products/ProductRepositoryIntegrationTests.cs)
