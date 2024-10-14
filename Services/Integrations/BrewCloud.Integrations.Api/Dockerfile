#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Integrations/BrewCloud.Integrations.Api/BrewCloud.Integrations.Api.csproj", "Services/Integrations/BrewCloud.Integrations.Api/"]
COPY ["Services/Integrations/BrewCloud.Integrations.Application/BrewCloud.Integrations.Application.csproj", "Services/Integrations/BrewCloud.Integrations.Application/"]
COPY ["Shared/BrewCloud.Shared/BrewCloud.Shared.csproj", "Shared/BrewCloud.Shared/"]
COPY ["Services/Integrations/BrewCloud.Integrations.Domain/BrewCloud.Integrations.Domain.csproj", "Services/Integrations/BrewCloud.Integrations.Domain/"]
COPY ["Services/Integrations/BrewCloud.Integrations.Infrastructure/BrewCloud.Integrations.Infrastructure.csproj", "Services/Integrations/BrewCloud.Integrations.Infrastructure/"]
RUN dotnet restore "Services/Integrations/BrewCloud.Integrations.Api/BrewCloud.Integrations.Api.csproj"
COPY . .
WORKDIR "/src/Services/Integrations/BrewCloud.Integrations.Api"
RUN dotnet build "BrewCloud.Integrations.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BrewCloud.Integrations.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BrewCloud.Integrations.Api.dll"]