#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Services/IdentityServer/BrewCloud.IdentityServer/BrewCloud.IdentityServer.csproj", "Services/IdentityServer/BrewCloud.IdentityServer/"]
COPY ["Services/IdentityServer/BrewCloud.IdentityServer.Application/BrewCloud.IdentityServer.Application.csproj", "Services/IdentityServer/BrewCloud.IdentityServer.Application/"]
COPY ["Shared/BrewCloud.Shared/BrewCloud.Shared.csproj", "Shared/BrewCloud.Shared/"]
COPY ["Services/IdentityServer/BrewCloud.IdentityServer.Infrastructure/BrewCloud.IdentityServer.Infrastructure.csproj", "Services/IdentityServer/BrewCloud.IdentityServer.Infrastructure/"]
RUN dotnet restore "Services/IdentityServer/BrewCloud.IdentityServer/BrewCloud.IdentityServer.csproj"
COPY . .
WORKDIR "/src/Services/IdentityServer/BrewCloud.IdentityServer"
RUN dotnet build "BrewCloud.IdentityServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BrewCloud.IdentityServer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BrewCloud.IdentityServer.dll"]