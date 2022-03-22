# Smart Charging

## Guide

Swagger runs when you run the project directly if you are using Rider or Visual Studio.

Here are default ports:

`> http://localhost:3428/swagger`

`> https://localhost:3434/swagger`

The project runs InMemory database as default. If you want to change that update Provider in "appsettings.Development.json".

**Provider:**

- InMemory
- SqlServer

**PS:** If you have a SqlServer and want to run on it please use docker-compose to run SqlServer on Docker or use your own database connection. After doing that please make sure that you updated the "DefaultConnection" in "appsettings.Development.json".

_**PS:** I have tried to run the whole application on Docker but there is a technical problem with the Apple Mac M1 (arm processor) so I couldn't test with SqlServer. You will see the commented parts on the Docker file related to SqlServer and Application._

### Database Migrations

If you want to re-create the database migrations delete "SmartCharging.Infrastructure/Migrations" folder then run that command (in "source" directory) in terminal.

`> dotnet ef migrations add InitialDb --project SmartCharging.Infrastructure --startup-project SmartCharging.Api --output-dir Migrations`

PS: Already there and when you changed Provider as 'SqlServer' migration runs automatically.

### 3rd Party Services

Please use the "docker-compose" before running the application for environments in terminal.

`> docker-compose up -d`

#### Environments

- ElasticSearch: <http://localhost:9200>
- Kibana: <http://localhost:5601>

PS: After building the Kibana that it takes a while to start Kibana UI.

## Framework and Tools

- .NET Core: <https://docs.microsoft.com/en-us/dotnet/core>
- ASP.NET Core: <https://docs.microsoft.com/en-us/aspnet/core>
- MediatR: <https://github.com/jbogard/MediatR>
- StackExchange.Redis: <https://github.com/StackExchange/StackExchange.Redis>
- Serilog: <https://github.com/serilog/serilog>
- NUnit: <https://github.com/nunit/nunit> | <https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-nunit>
- Docker: <https://www.docker.com>

## Storages & Monitoring

- ElasticSearch (Logging Storage): <https://www.elastic.co/elasticsearch>
- Kibana (Logging Monitoring): <https://www.elastic.co/kibana>

## Patterns and Practices

- SOLID Principles
- CQRS: <https://microservices.io/patterns/data/cqrs.html>
- Repository Pattern: <https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-design>
- Unit of Work: <https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application>
- Unit Tests (with simple examples)

### Additional Pattern and Practices

These patterns didn't apply in the application but can be useful for the approach.

- Microservices: <https://microservices.io>
- Backends for Frontends: <https://microservices.io/patterns/apigateway.html>
- API Gateway: <https://microservices.io/patterns/apigateway.html>
- Transactional Outbox: <https://microservices.io/patterns/data/transactional-outbox.html>
- Health Check: <https://microservices.io/patterns/observability/health-check-api.html>
- Unit Testing Best Practices: <https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices>

## Other Challenge Projects

- Railroad: <https://github.com/mstfcck/Railroad>
- EscapeMines: <https://github.com/mstfcck/EscapeMines>
