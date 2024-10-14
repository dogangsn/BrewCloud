#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Farm/BrewCloud.Farm.Api/BrewCloud.Farm.Api.csproj", "Services/Farm/BrewCloud.Farm.Api/"]
COPY ["Services/Farm/BrewCloud.Farm.Application/BrewCloud.Farm.Application.csproj", "Services/Farm/BrewCloud.Farm.Application/"]
COPY ["Shared/BrewCloud.Shared/BrewCloud.Shared.csproj", "Shared/BrewCloud.Shared/"]
COPY ["Services/Farm/BrewCloud.Farm.Domain/BrewCloud.Farm.Domain.csproj", "Services/Farm/BrewCloud.Farm.Domain/"]
COPY ["Services/Farm/BrewCloud.Farm.Infrastructure/BrewCloud.Farm.Infrastructure.csproj", "Services/Farm/BrewCloud.Farm.Infrastructure/"]
RUN dotnet restore "Services/Farm/BrewCloud.Farm.Api/BrewCloud.Farm.Api.csproj"
COPY . .
WORKDIR "/src/Services/Farm/BrewCloud.Farm.Api"
RUN dotnet build "BrewCloud.Farm.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BrewCloud.Farm.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BrewCloud.Farm.Api.dll"]