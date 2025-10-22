# XUnitDemo

A comprehensive xUnit demonstration project using C# and .NET 8, showcasing clean architecture, organized tests with xUnit, mocking with Moq, dependency injection, and code coverage using coverlet.collector. Includes a GitHub Actions CI workflow with coverage reporting and optional publishing of the demo console app.

## Project Structure
- `src/XUnitDemo.Domain` — Domain entities (`Product`, `OrderItem`, `Order`).
- `src/XUnitDemo.Application` — Service abstractions and implementations (`ICalculatorService`, `IOrderService`, `CalculatorService`, `OrderService`).
- `src/XUnitDemo.Infrastructure` — In-memory repository (`InMemoryOrderRepository`).
- `src/XUnitDemo.Console` — Console app demonstrating DI registration and usage.
- `tests/XUnitDemo.Tests` — xUnit tests organized with `Fact` and `Theory`, mocking via `Moq`, DI tests, and runsettings for coverlet.collector.
- `.github/workflows/ci.yml` — GitHub Actions workflow for build, tests, coverage, and publish artifact.

## Prerequisites
- `.NET SDK` `8.0.x` installed.
- PowerShell or any terminal.

## Setup & Run
- Restore and build:
  - `dotnet restore`
  - `dotnet build -c Debug`
- Run tests with coverage (collector):
  - `dotnet test --settings tests/XUnitDemo.Tests/XUnitDemo.runsettings --collect:"XPlat Code Coverage" --results-directory tests/XUnitDemo.Tests/TestResults`
- Generate HTML coverage report locally:
  - `dotnet tool restore`
  - `dotnet tool run reportgenerator -reports:tests/XUnitDemo.Tests/TestResults/**/coverage.cobertura.xml -targetdir:tests/XUnitDemo.Tests/TestResults/CoverageReport -reporttypes:HtmlInline_AzurePipelines`
- View coverage:
  - Open `tests/XUnitDemo.Tests/TestResults/CoverageReport/index.html` in a browser.

## Tests Overview
- `CalculatorServiceTests`
  - `Fact` examples: positive cases for `Add`, `Multiply`, edge case for `Divide` throwing on zero.
  - `Theory` examples: multiple `InlineData` to validate `Add` across varied inputs.
- `OrderServiceTests`
  - Mocking: `Moq` verifies `IOrderRepository.SaveAsync` is called once.
  - DI: `ServiceCollection` resolves `IOrderService` and repository.
  - Validation: ensures invalid quantities and null orders throw appropriate exceptions.

## Code Coverage
- Collector configuration in `tests/XUnitDemo.Tests/XUnitDemo.runsettings`:
  - Format: `cobertura`
  - Includes: `[XUnitDemo.*]*`
  - Excludes: `[xunit*]*,[Moq*]*`
- Threshold enforcement (CI-only) using msbuild integration:
  - `dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:Threshold=85 /p:ThresholdType=line`
- Example coverage snapshot (from a local run):
  - `CalculatorService` — 100% lines
  - `OrderService` — 100% lines
  - `InMemoryOrderRepository` — 100% lines

## CI/CD Workflow
- Triggers: on `push` and `pull_request` to `main`.
- Stages:
  - Build and test
  - Collect coverage via `coverlet.collector` and generate HTML report
  - Upload coverage artifact
  - Enforce coverage thresholds via `coverlet.msbuild`
  - Publish console app and upload as artifact
- Workflow file: `.github/workflows/ci.yml`

## Deployment (Demo)
- CI publishes the console app to `dist/XUnitDemo.Console` and uploads it as an artifact.
- Local publish:
  - `dotnet publish src/XUnitDemo.Console/XUnitDemo.Console.csproj -c Release -o dist/XUnitDemo.Console`

## Best Practices Followed
- Clean architecture separation: Domain, Application, Infrastructure, Host.
- Proper test organization with clear naming and logical categories.
- Meaningful test names: `Method_StateUnderTest_ExpectedBehavior`.
- Error handling: input validation and explicit exceptions in domain and services.
- Secure configuration: no secrets checked in; CI uses default runners and tools.

## Notes
- This demo uses `coverlet.collector` for collection and enforces thresholds through msbuild in CI to provide both accurate collection and strict gatekeeping.
- If you change project names or add new projects, update `Include`/`Exclude` patterns in `XUnitDemo.runsettings` accordingly.