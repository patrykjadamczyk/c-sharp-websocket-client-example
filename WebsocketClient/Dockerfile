FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["WebsocketClient/WebsocketClient.csproj", "WebsocketClient/"]
RUN dotnet restore "WebsocketClient/WebsocketClient.csproj"
COPY . .
WORKDIR "/src/WebsocketClient"
RUN dotnet build "WebsocketClient.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebsocketClient.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebsocketClient.dll"]
