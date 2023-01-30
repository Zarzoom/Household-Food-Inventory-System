# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY . /app
RUN dotnet restore


# Copy everything else and build
RUN dotnet publish -c Release -o out




# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-env /app/out .
EXPOSE 7134
ENTRYPOINT ["dotnet", "InventoryInCSharpAPI.dll"]