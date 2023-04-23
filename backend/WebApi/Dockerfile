FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["backend/WebApi/WebApi.csproj", "backend/WebApi/"]
COPY ["backend/Application/Application.csproj", "backend/Application/"]
COPY ["backend/Domain/Domain.csproj", "backend/Domain/"]
COPY ["backend/Infrastructure/Infrastructure.csproj", "backend/Infrastructure/"]
RUN dotnet restore "backend/WebApi/WebApi.csproj"
COPY . .
WORKDIR "/src/backend/WebApi"
RUN dotnet build "WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApi.dll"]
