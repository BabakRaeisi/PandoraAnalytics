FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY PandoraAnalyticsAPI.API/PandoraAnalyticsAPI.API.csproj PandoraAnalyticsAPI.API/
COPY PandoraAnalyticsAPI.Application/PandoraAnalyticsAPI.Application.csproj PandoraAnalyticsAPI.Application/
COPY PandoraAnalyticsAPI.Infrastructure/PandoraAnalyticsAPI.Infrastructure.csproj PandoraAnalyticsAPI.Infrastructure/
COPY PandoraAnalyticsAPI.Domain/PandoraAnalyticsAPI.Domain.csproj PandoraAnalyticsAPI.Domain/
RUN dotnet restore PandoraAnalyticsAPI.API/PandoraAnalyticsAPI.API.csproj

COPY . .
WORKDIR /src/PandoraAnalyticsAPI.API
RUN dotnet publish PandoraAnalyticsAPI.API.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

# Run as non-root user for production hardening.
USER $APP_UID

COPY --from=build /app/publish .

EXPOSE 5001

ENTRYPOINT ["dotnet", "PandoraAnalyticsAPI.API.dll"]
