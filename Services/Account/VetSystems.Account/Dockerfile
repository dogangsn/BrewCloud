#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Account/VetSystems.Account/VetSystems.Account.Api.csproj", "Services/Account/VetSystems.Account/"]
COPY ["Services/Account/VetSystems.Account.Application/VetSystems.Account.Application.csproj", "Services/Account/VetSystems.Account.Application/"]
COPY ["Shared/VetSystems.Shared/VetSystems.Shared.csproj", "Shared/VetSystems.Shared/"]
COPY ["Services/Account/VetSystems.Account.Domain/VetSystems.Account.Domain.csproj", "Services/Account/VetSystems.Account.Domain/"]
COPY ["Services/Account/VetSystems.Account.Infrastructure/VetSystems.Account.Infrastructure.csproj", "Services/Account/VetSystems.Account.Infrastructure/"]
RUN dotnet restore "Services/Account/VetSystems.Account/VetSystems.Account.Api.csproj"
COPY . .
WORKDIR "/src/Services/Account/VetSystems.Account"
RUN dotnet build "VetSystems.Account.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VetSystems.Account.Api.csproj" -c Release -o /app/publish 

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VetSystems.Account.Api.dll"]