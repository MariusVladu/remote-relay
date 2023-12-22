FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app
EXPOSE 1883

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["HomeAssistant.MQTT/HomeAssistant.MQTT.csproj", "HomeAssistant.MQTT/"]
RUN dotnet restore "./HomeAssistant.MQTT/./HomeAssistant.MQTT.csproj"
COPY . .
WORKDIR "/src/HomeAssistant.MQTT"
RUN dotnet build "./HomeAssistant.MQTT.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./HomeAssistant.MQTT.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HomeAssistant.MQTT.dll"]