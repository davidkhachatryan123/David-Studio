#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

ARG NODE_IMAGE=node:16-bullseye

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Install Node.js
RUN apt-get update && \
apt-get install -y wget && \
apt-get install -y gnupg2 && \
wget -qO- https://deb.nodesource.com/setup_18.x | bash - && \
apt-get install -y build-essential nodejs
# End Install

COPY ["Services/IdentityServer/IdentityServer.csproj", "Services/IdentityServer/"]

RUN dotnet restore "Services/IdentityServer/IdentityServer.csproj" -nowarn:msb3202,nu1503
COPY . .

WORKDIR "/src/Services/IdentityServer"
RUN dotnet build "IdentityServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IdentityServer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "IdentityServer.dll"]
