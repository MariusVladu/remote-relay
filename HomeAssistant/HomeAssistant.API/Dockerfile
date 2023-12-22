#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["HomeAssistant.API/HomeAssistant.API.csproj", "HomeAssistant.API/"]
COPY ["HomeAssistant.DI/HomeAssistant.DI.csproj", "HomeAssistant.DI/"]
COPY ["HomeAssistant.BusinessLogic/HomeAssistant.BusinessLogic.csproj", "HomeAssistant.BusinessLogic/"]
COPY ["HomeAssistant.Domain/HomeAssistant.Domain.csproj", "HomeAssistant.Domain/"]
COPY ["HomeAssistant.Services/HomeAssistant.Services.csproj", "HomeAssistant.Services/"]
RUN dotnet restore "./HomeAssistant.API/./HomeAssistant.API.csproj"
COPY . .
WORKDIR "/src/HomeAssistant.API"
RUN dotnet build "./HomeAssistant.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./HomeAssistant.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HomeAssistant.API.dll"]