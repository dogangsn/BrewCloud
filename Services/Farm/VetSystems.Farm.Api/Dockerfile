#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Farm/VetSystems.Farm.Api/VetSystems.Farm.Api.csproj", "Services/Farm/VetSystems.Farm.Api/"]
COPY ["Services/Farm/VetSystems.Farm.Application/VetSystems.Farm.Application.csproj", "Services/Farm/VetSystems.Farm.Application/"]
COPY ["Shared/VetSystems.Shared/VetSystems.Shared.csproj", "Shared/VetSystems.Shared/"]
COPY ["Services/Farm/VetSystems.Farm.Domain/VetSystems.Farm.Domain.csproj", "Services/Farm/VetSystems.Farm.Domain/"]
COPY ["Services/Farm/VetSystems.Farm.Infrastructure/VetSystems.Farm.Infrastructure.csproj", "Services/Farm/VetSystems.Farm.Infrastructure/"]
RUN dotnet restore "Services/Farm/VetSystems.Farm.Api/VetSystems.Farm.Api.csproj"
COPY . .
WORKDIR "/src/Services/Farm/VetSystems.Farm.Api"
RUN dotnet build "VetSystems.Farm.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VetSystems.Farm.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VetSystems.Farm.Api.dll"]