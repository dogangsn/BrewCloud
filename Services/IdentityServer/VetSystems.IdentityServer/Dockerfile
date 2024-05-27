#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Services/IdentityServer/VetSystems.IdentityServer/VetSystems.IdentityServer.csproj", "Services/IdentityServer/VetSystems.IdentityServer/"]
COPY ["Services/IdentityServer/VetSystems.IdentityServer.Application/VetSystems.IdentityServer.Application.csproj", "Services/IdentityServer/VetSystems.IdentityServer.Application/"]
COPY ["Shared/VetSystems.Shared/VetSystems.Shared.csproj", "Shared/VetSystems.Shared/"]
COPY ["Services/IdentityServer/VetSystems.IdentityServer.Infrastructure/VetSystems.IdentityServer.Infrastructure.csproj", "Services/IdentityServer/VetSystems.IdentityServer.Infrastructure/"]
RUN dotnet restore "Services/IdentityServer/VetSystems.IdentityServer/VetSystems.IdentityServer.csproj"
COPY . .
WORKDIR "/src/Services/IdentityServer/VetSystems.IdentityServer"
RUN dotnet build "VetSystems.IdentityServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VetSystems.IdentityServer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VetSystems.IdentityServer.dll"]