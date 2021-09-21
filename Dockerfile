FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

# Copiar csproj e restaurar dependencias
COPY src/*/*.csproj ./
RUN dotnet restore ZeDelivery.Backend.Challenge.Api.csproj

# Build da aplicacao
COPY . ./
RUN dotnet publish ZeDelivery.Backend.Challenge.sln -c Release -o out

# Build da imagem
FROM mcr.microsoft.com/dotnet/sdk:5.0
WORKDIR /app
COPY --from=build-env /app/out .
EXPOSE 8081
ENTRYPOINT ["dotnet", "ZeDelivery.Backend.Challenge.Api.dll", "--server.urls", "http://+:8081"]