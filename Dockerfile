## syntax=docker/dockerfile:1
#FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
#WORKDIR /app
#
## Copy csproj and restore as distinct layers
#COPY . /app
#RUN dotnet restore
#
#
## Copy everything else and build
#RUN dotnet publish -c Release -o out
#
#
#
#
## Build runtime image
#FROM mcr.microsoft.com/dotnet/aspnet:6.0
#WORKDIR /app
#COPY --from=build-env /app/out .
#EXPOSE 7134
#ENTRYPOINT ["dotnet", "InventoryInCSharpAPI.dll"]

#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["InventoryInCSharpAPI/InventoryInCSharpAPI.csproj", "InventoryInCSharpAPI/"]
RUN dotnet restore "InventoryInCSharpAPI/InventoryInCSharpAPI.csproj"
COPY . .
WORKDIR "/src/InventoryInCSharpAPI"
RUN dotnet build "InventoryInCSharpAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InventoryInCSharpAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS run
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InventoryInCSharpAPI.dll"]