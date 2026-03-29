# PandoraAnalytics

PandoraAnalytics is an ASP.NET Core Web API for collecting and querying player session and trial analytics.

## Tech Stack

- .NET 9 Web API
- Entity Framework Core
- PostgreSQL
- Docker Compose

## Project Structure

- `PandoraAnalyticsAPI.API` - API layer (controllers, startup, configuration)
- `PandoraAnalyticsAPI.Application` - application services, DTOs, interfaces
- `PandoraAnalyticsAPI.Domain` - domain entities
- `PandoraAnalyticsAPI.Infrastructure` - EF Core DbContext, migrations, repository implementations

## API Endpoints

Base route: `/api/analytics`

- `POST /upload` - upload a session payload
- `POST /profile` - create or restore player profile
- `GET /players` - list players
- `GET /players/{phoneNumber}/sessions` - list sessions for a player
- `GET /players/{phoneNumber}/trials` - list trials for a player
- `GET /trials` - list all trials
- `GET /sessions/{sessionId}/trials` - list trials for a session
- `GET /health` - health check endpoint

## Run With Docker (Recommended)

1. Copy environment template:

```bash
cp .env.example .env
```

2. Edit `.env` and set secure values:

- `POSTGRES_DB`
- `POSTGRES_USER`
- `POSTGRES_PASSWORD`
- `TLS_CERT_PASSWORD`
- `TLS_CERT_PATH`

3. Ensure your TLS certificate file exists at the path in `TLS_CERT_PATH`.

4. Start services:

```bash
docker compose up --build -d
```

5. API will be available at:

- `https://localhost:5001`

## Development Notes

- Swagger UI is enabled in Development environment.
- HTTPS is enforced.
- Forwarded headers are enabled for reverse proxy scenarios.

## License

This project is licensed under the MIT License. See `LICENSE` for details.
