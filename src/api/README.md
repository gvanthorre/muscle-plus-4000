# MusclePlus4000 API - Migrations Guide

## Why `dotnet ef` needs multiple parameters

In this solution, the EF `DbContext` lives in `MusclePlus4000.Infrastructure`, but runtime configuration (connection string, DI, provider setup) lives in `MusclePlus4000.Api`.

Because of that split, EF CLI often needs both:

- `--project`: where migrations are created (Infrastructure)
- `--startup-project`: project EF runs to resolve configuration (Api)

## Parameters: required vs optional

- `--project`
  - Required when you run the command outside the migrations project folder, or when you want migrations generated in Infrastructure explicitly.
  - Optional only if your current directory is `MusclePlus4000.Infrastructure` and that is the intended target.

- `--startup-project`
  - Required when startup/configuration is not in the current project.
  - In this solution, usually set to `MusclePlus4000.Api`.

- `--context`
  - Optional when there is only one `DbContext` discoverable.
  - Recommended for clarity and future-proofing if you add more contexts.

- `--output-dir`
  - Optional.
  - Use it if you want a stable, predictable folder for migration files.

## Recommended commands (always creates files in Infrastructure)

Run from `MusclePlus4000.Infrastructure` when possible:

```powershell
cd D:\Projects\muscle-plus-4000\src\api\MusclePlus4000.Infrastructure

dotnet ef migrations add InitialCreate `
  --startup-project ..\MusclePlus4000.Api\MusclePlus4000.Api.csproj `
  --context WorkoutDbContext `
  --output-dir Persistence\Migrations

dotnet ef database update `
  --startup-project ..\MusclePlus4000.Api\MusclePlus4000.Api.csproj `
  --context WorkoutDbContext
```

If you run from `src/api`, keep `--project` explicit:

```powershell
cd D:\Projects\muscle-plus-4000\src\api

dotnet ef migrations add InitialCreate `
  --project .\MusclePlus4000.Infrastructure\MusclePlus4000.Infrastructure.csproj `
  --startup-project .\MusclePlus4000.Api\MusclePlus4000.Api.csproj `
  --context WorkoutDbContext `
  --output-dir Persistence\Migrations
```

## Shorter command variant

If you are inside `MusclePlus4000.Infrastructure` and still want API startup:

```powershell
cd D:\Projects\muscle-plus-4000\src\api\MusclePlus4000.Infrastructure

dotnet ef migrations add InitialCreate --startup-project ..\MusclePlus4000.Api --context WorkoutDbContext
```

## Useful maintenance commands

```powershell
cd D:\Projects\muscle-plus-4000\src\api

dotnet ef migrations list `
  --project .\MusclePlus4000.Infrastructure\MusclePlus4000.Infrastructure.csproj `
  --startup-project .\MusclePlus4000.Api\MusclePlus4000.Api.csproj `
  --context WorkoutDbContext

dotnet ef migrations remove `
  --project .\MusclePlus4000.Infrastructure\MusclePlus4000.Infrastructure.csproj `
  --startup-project .\MusclePlus4000.Api\MusclePlus4000.Api.csproj `
  --context WorkoutDbContext
```

## Practical recommendation

For team consistency, keep using all explicit parameters in shared docs/scripts.
They are not always strictly required, but they prevent command ambiguity as the solution grows.

