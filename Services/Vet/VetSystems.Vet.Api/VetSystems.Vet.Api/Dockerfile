#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Vet/VetSystems.Vet.Api/VetSystems.Vet.Api/VetSystems.Vet.Api.csproj", "Services/Vet/VetSystems.Vet.Api/VetSystems.Vet.Api/"]
COPY ["Services/Vet/VetSystems.Vet.Application/VetSystems.Vet.Application/VetSystems.Vet.Application.csproj", "Services/Vet/VetSystems.Vet.Application/VetSystems.Vet.Application/"]
COPY ["Shared/VetSystems.Shared/VetSystems.Shared.csproj", "Shared/VetSystems.Shared/"]
COPY ["Services/Vet/VetSystems.Vet.Domain/VetSystems.Vet.Domain/VetSystems.Vet.Domain.csproj", "Services/Vet/VetSystems.Vet.Domain/VetSystems.Vet.Domain/"]
COPY ["Services/Vet/VetSystems.Vet.Infrastructure/VetSystems.Vet.Infrastructure/VetSystems.Vet.Infrastructure.csproj", "Services/Vet/VetSystems.Vet.Infrastructure/VetSystems.Vet.Infrastructure/"]
RUN dotnet restore "Services/Vet/VetSystems.Vet.Api/VetSystems.Vet.Api/VetSystems.Vet.Api.csproj"
COPY . .
WORKDIR "/src/Services/Vet/VetSystems.Vet.Api/VetSystems.Vet.Api"
RUN dotnet build "VetSystems.Vet.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VetSystems.Vet.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VetSystems.Vet.Api.dll"]