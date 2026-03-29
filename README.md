# PandoraAnalytics

PandoraAnalytics is an ASP.NET Core Web API for collecting and querying player session and trial analytics.

## Tech Stack

- .NET 10 Web API
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

## Deploy On AWS With Docker (EC2)

This repository includes an AWS-oriented compose file for running behind AWS TLS termination.

1. Launch an EC2 instance (Ubuntu 22.04 is fine).
2. Open inbound Security Group rules:
   - `22` (SSH) from your IP
   - `80` and `443` from internet (if using ALB or reverse proxy)
   - for direct testing only: `8080` from your IP
3. Install Docker + Compose plugin on EC2.
4. Clone this repository on EC2.
5. Create environment file from template:

```bash
cp .env.aws.example .env
```

6. Edit `.env` with a strong database password.
7. Start the AWS compose stack:

```bash
docker compose -f docker-compose.aws.yml up --build -d
```

8. Verify containers:

```bash
docker compose -f docker-compose.aws.yml ps
```

9. Health check:

```bash
curl http://<EC2_PUBLIC_IP>:8080/health
```

Notes:

- `docker-compose.aws.yml` exposes API on HTTP `:8080` and sets `EnforceHttps=false`.
- In production, terminate TLS at an AWS Application Load Balancer and forward traffic to port `8080`.
- If you use ALB + HTTPS, set `EnforceHttps=true` if you want app-level HTTPS redirection behavior.

## Development Notes

- Swagger UI is enabled in Development environment.
- HTTPS is enforced.
- Forwarded headers are enabled for reverse proxy scenarios.

## License

This project is licensed under the MIT License. See `LICENSE` for details.
