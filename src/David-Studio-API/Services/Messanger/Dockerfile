#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Messanger/Messanger.csproj", "Services/Messanger/"]
COPY ["EventBus/EventBus/EventBus.csproj", "EventBus/EventBus/"]
COPY ["EventBus/EventBusRabbitMQ/EventBusRabbitMQ.csproj", "EventBus/EventBusRabbitMQ/"]
COPY ["Services/Services.Common/Services.Common.csproj", "Services/Services.Common/"]
RUN dotnet restore "Services/Messanger/Messanger.csproj"
COPY . .
WORKDIR "/src/Services/Messanger"
RUN dotnet build "Messanger.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Messanger.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Messanger.dll"]
