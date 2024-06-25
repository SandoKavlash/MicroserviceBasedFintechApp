#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MicroserviceBasedFintechApp.PaymentService.Api/MicroserviceBasedFintechApp.PaymentService.Api.csproj", "MicroserviceBasedFintechApp.PaymentService.Api/"]
COPY ["MicroserviceBasedFintechApp.PaymentService.Core/MicroserviceBasedFintechApp.PaymentService.Core.csproj", "MicroserviceBasedFintechApp.PaymentService.Core/"]
COPY ["MicroserviceBasedFintechApp.PaymentService.Persistence/MicroserviceBasedFintechApp.PaymentService.Persistence.csproj", "MicroserviceBasedFintechApp.PaymentService.Persistence/"]
RUN dotnet restore "./MicroserviceBasedFintechApp.PaymentService.Api/MicroserviceBasedFintechApp.PaymentService.Api.csproj"
COPY . .
WORKDIR "/src/MicroserviceBasedFintechApp.PaymentService.Api"
RUN dotnet build "./MicroserviceBasedFintechApp.PaymentService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MicroserviceBasedFintechApp.PaymentService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MicroserviceBasedFintechApp.PaymentService.Api.dll"]