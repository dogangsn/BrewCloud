#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Mobile/BrewCloud.Mobile.Api/BrewCloud.Mobile.Api.csproj", "Services/Mobile/BrewCloud.Mobile.Api/"]
RUN dotnet restore "Services/Mobile/BrewCloud.Mobile.Api/BrewCloud.Mobile.Api.csproj"
COPY . .
WORKDIR "/src/Services/Mobile/BrewCloud.Mobile.Api"
RUN dotnet build "BrewCloud.Mobile.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BrewCloud.Mobile.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BrewCloud.Mobile.Api.dll"]