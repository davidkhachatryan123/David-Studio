#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY ["Services/IdentityServer/IdentityServer.csproj", "Services/IdentityServer/"]
COPY ["Services/Services.Common/Services.Common.csproj", "Services/Services.Common/"]
COPY ["EventBus/EventBus/EventBus.csproj", "EventBus/EventBus/"]
COPY ["EventBus/EventBusRabbitMQ/EventBusRabbitMQ.csproj", "EventBus/EventBusRabbitMQ/"]

RUN dotnet restore "Services/IdentityServer/IdentityServer.csproj"

COPY . .
WORKDIR "/src/Services/IdentityServer"
RUN dotnet build "IdentityServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IdentityServer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

RUN apt-get -y update
RUN apt-get -y install curl

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "IdentityServer.dll"]
