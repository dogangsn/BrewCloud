#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Account/BrewCloud.Account/BrewCloud.Account.Api.csproj", "Services/Account/BrewCloud.Account/"]
COPY ["Services/Account/BrewCloud.Account.Application/BrewCloud.Account.Application.csproj", "Services/Account/BrewCloud.Account.Application/"]
COPY ["Shared/BrewCloud.Shared/BrewCloud.Shared.csproj", "Shared/BrewCloud.Shared/"]
COPY ["Services/Account/BrewCloud.Account.Domain/BrewCloud.Account.Domain.csproj", "Services/Account/BrewCloud.Account.Domain/"]
COPY ["Services/Account/BrewCloud.Account.Infrastructure/BrewCloud.Account.Infrastructure.csproj", "Services/Account/BrewCloud.Account.Infrastructure/"]
RUN dotnet restore "Services/Account/BrewCloud.Account/BrewCloud.Account.Api.csproj"
COPY . .
WORKDIR "/src/Services/Account/BrewCloud.Account"
RUN dotnet build "BrewCloud.Account.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BrewCloud.Account.Api.csproj" -c Release -o /app/publish 

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BrewCloud.Account.Api.dll"]