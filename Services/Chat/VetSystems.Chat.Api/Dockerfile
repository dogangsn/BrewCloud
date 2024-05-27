#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Chat/VetSystems.Chat.Api/VetSystems.Chat.Api.csproj", "Services/Chat/VetSystems.Chat.Api/"]
COPY ["Services/Chat/VetSystems.Chat.Application/VetSystems.Chat.Application.csproj", "Services/Chat/VetSystems.Chat.Application/"]
COPY ["Shared/VetSystems.Shared/VetSystems.Shared.csproj", "Shared/VetSystems.Shared/"]
COPY ["Services/Chat/VetSystems.Chat.Infrastructure/VetSystems.Chat.Infrastructure.csproj", "Services/Chat/VetSystems.Chat.Infrastructure/"]
RUN dotnet restore "Services/Chat/VetSystems.Chat.Api/VetSystems.Chat.Api.csproj"
COPY . .
WORKDIR "/src/Services/Chat/VetSystems.Chat.Api"
RUN dotnet build "VetSystems.Chat.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VetSystems.Chat.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VetSystems.Chat.Api.dll"]