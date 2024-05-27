#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Mobile/VetSystems.Mobile.Api/VetSystems.Mobile.Api.csproj", "Services/Mobile/VetSystems.Mobile.Api/"]
RUN dotnet restore "Services/Mobile/VetSystems.Mobile.Api/VetSystems.Mobile.Api.csproj"
COPY . .
WORKDIR "/src/Services/Mobile/VetSystems.Mobile.Api"
RUN dotnet build "VetSystems.Mobile.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VetSystems.Mobile.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VetSystems.Mobile.Api.dll"]