FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["backend/DbMigrator/DbMigrator.csproj", "backend/DbMigrator/"]
RUN dotnet restore "backend/DbMigrator/DbMigrator.csproj"
COPY . .
WORKDIR "/src/backend/DbMigrator"
RUN dotnet build "DbMigrator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DbMigrator.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DbMigrator.dll"]