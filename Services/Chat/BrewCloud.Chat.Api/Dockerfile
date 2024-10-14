#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Chat/BrewCloud.Chat.Api/BrewCloud.Chat.Api.csproj", "Services/Chat/BrewCloud.Chat.Api/"]
COPY ["Services/Chat/BrewCloud.Chat.Application/BrewCloud.Chat.Application.csproj", "Services/Chat/BrewCloud.Chat.Application/"]
COPY ["Shared/BrewCloud.Shared/BrewCloud.Shared.csproj", "Shared/BrewCloud.Shared/"]
COPY ["Services/Chat/BrewCloud.Chat.Infrastructure/BrewCloud.Chat.Infrastructure.csproj", "Services/Chat/BrewCloud.Chat.Infrastructure/"]
RUN dotnet restore "Services/Chat/BrewCloud.Chat.Api/BrewCloud.Chat.Api.csproj"
COPY . .
WORKDIR "/src/Services/Chat/BrewCloud.Chat.Api"
RUN dotnet build "BrewCloud.Chat.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BrewCloud.Chat.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BrewCloud.Chat.Api.dll"]