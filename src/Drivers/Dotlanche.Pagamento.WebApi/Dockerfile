FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release

COPY ["src/Drivers/Dotlanche.Pagamento.WebApi/Dotlanche.Pagamento.WebApi.csproj", "src/Drivers/Dotlanche.Pagamento.WebApi/"]
RUN dotnet restore "./src/Drivers/Dotlanche.Pagamento.WebApi/Dotlanche.Pagamento.WebApi.csproj"
COPY . .
WORKDIR "/src/Drivers/Dotlanche.Pagamento.WebApi"
RUN dotnet build "./Dotlanche.Pagamento.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Dotlanche.Pagamento.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dotlanche.Pagamento.WebApi.dll"]