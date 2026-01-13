# Use the official .NET 8 runtime as base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Use the official .NET 8 SDK for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project files
COPY ["AssetManagement.API/AssetManagement.API.csproj", "AssetManagement.API/"]
COPY ["AssetManagement.Application/AssetManagement.Application.csproj", "AssetManagement.Application/"]
COPY ["AssetManagement.Infrastructure/AssetManagement.Infrastructure.csproj", "AssetManagement.Infrastructure/"]
COPY ["AssetManagement.Domain/AssetManagement.Domain.csproj", "AssetManagement.Domain/"]

# Restore dependencies
RUN dotnet restore "AssetManagement.API/AssetManagement.API.csproj"

# Copy all source code
COPY . .

# Build the application
WORKDIR "/src/AssetManagement.API"
RUN dotnet build "AssetManagement.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the application
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "AssetManagement.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Create logs directory
RUN mkdir -p /app/Logs

ENTRYPOINT ["dotnet", "AssetManagement.API.dll"]