#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Integrations/VetSystems.Integrations.Api/VetSystems.Integrations.Api.csproj", "Services/Integrations/VetSystems.Integrations.Api/"]
COPY ["Services/Integrations/VetSystems.Integrations.Application/VetSystems.Integrations.Application.csproj", "Services/Integrations/VetSystems.Integrations.Application/"]
COPY ["Shared/VetSystems.Shared/VetSystems.Shared.csproj", "Shared/VetSystems.Shared/"]
COPY ["Services/Integrations/VetSystems.Integrations.Domain/VetSystems.Integrations.Domain.csproj", "Services/Integrations/VetSystems.Integrations.Domain/"]
COPY ["Services/Integrations/VetSystems.Integrations.Infrastructure/VetSystems.Integrations.Infrastructure.csproj", "Services/Integrations/VetSystems.Integrations.Infrastructure/"]
RUN dotnet restore "Services/Integrations/VetSystems.Integrations.Api/VetSystems.Integrations.Api.csproj"
COPY . .
WORKDIR "/src/Services/Integrations/VetSystems.Integrations.Api"
RUN dotnet build "VetSystems.Integrations.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VetSystems.Integrations.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VetSystems.Integrations.Api.dll"]