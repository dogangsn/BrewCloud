#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Vet/BrewCloud.Vet.Api/BrewCloud.Vet.Api/BrewCloud.Vet.Api.csproj", "Services/Vet/BrewCloud.Vet.Api/BrewCloud.Vet.Api/"]
COPY ["Services/Vet/BrewCloud.Vet.Application/BrewCloud.Vet.Application/BrewCloud.Vet.Application.csproj", "Services/Vet/BrewCloud.Vet.Application/BrewCloud.Vet.Application/"]
COPY ["Shared/BrewCloud.Shared/BrewCloud.Shared.csproj", "Shared/BrewCloud.Shared/"]
COPY ["Services/Vet/BrewCloud.Vet.Domain/BrewCloud.Vet.Domain/BrewCloud.Vet.Domain.csproj", "Services/Vet/BrewCloud.Vet.Domain/BrewCloud.Vet.Domain/"]
COPY ["Services/Vet/BrewCloud.Vet.Infrastructure/BrewCloud.Vet.Infrastructure/BrewCloud.Vet.Infrastructure.csproj", "Services/Vet/BrewCloud.Vet.Infrastructure/BrewCloud.Vet.Infrastructure/"]
RUN dotnet restore "Services/Vet/BrewCloud.Vet.Api/BrewCloud.Vet.Api/BrewCloud.Vet.Api.csproj"
COPY . .
WORKDIR "/src/Services/Vet/BrewCloud.Vet.Api/BrewCloud.Vet.Api"
RUN dotnet build "BrewCloud.Vet.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BrewCloud.Vet.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BrewCloud.Vet.Api.dll"]