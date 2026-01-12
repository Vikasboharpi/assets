# Asset Management API - Production Deployment Guide

## Prerequisites

- Docker and Docker Compose installed
- .NET 10 SDK (for development)
- PostgreSQL (if running without Docker)

## Quick Start with Docker

### 1. Clone and Setup Environment

```bash
# Clone the repository
git clone <your-repo-url>
cd AssetManagement

# Copy environment template
cp .env.example .env

# Edit .env with your production values
nano .env  # or use your preferred editor
```

### 2. Configure Environment Variables

Edit `.env` file with your production settings:

```bash
# Database Configuration
DB_PASSWORD=your_secure_database_password
JWT_SECRET_KEY=your_jwt_secret_key_minimum_32_characters_long

# Optional: Customize other settings
DB_NAME=assetdb
DB_USER=postgres
JWT_ISSUER=AssetManagementAPI
JWT_AUDIENCE=AssetManagementClient
CORS_ORIGINS=https://yourdomain.com
```

### 3. Deploy

**Linux/macOS:**
```bash
chmod +x deploy.sh
./deploy.sh
```

**Windows:**
```cmd
deploy.bat
```

### 4. Verify Deployment

- API Health: http://localhost:8080/health
- API Base: http://localhost:8080/api
- Swagger (Dev only): http://localhost:8080/swagger

## Manual Docker Commands

```bash
# Build and start all services
docker-compose up --build -d

# View logs
docker-compose logs -f

# Stop services
docker-compose down

# Restart specific service
docker-compose restart api

# View specific service logs
docker-compose logs -f api
docker-compose logs -f postgres
```

## Database Migrations

Migrations run automatically when the API starts. The following migrations will be applied:

1. `InitialCreated` - Creates all tables and relationships
2. Seeds default data (roles, categories, admin user)

### Manual Migration (if needed)

```bash
# Connect to API container
docker exec -it assetmanagement-api bash

# Run migrations manually
dotnet ef database update --project AssetManagement.Infrastructure --startup-project AssetManagement.API
```

## Environment Configuration

### Development
- Uses `appsettings.Development.json`
- Swagger UI enabled
- Detailed logging
- CORS allows localhost origins

### Production
- Uses `appsettings.Production.json`
- Swagger UI disabled
- Minimal logging
- Environment variables for sensitive data
- Security headers via Nginx

## Security Considerations

### Required for Production:

1. **Strong JWT Secret**: Minimum 32 characters, cryptographically secure
2. **Secure Database Password**: Use strong, unique password
3. **HTTPS**: Configure SSL certificates in Nginx
4. **Environment Variables**: Never commit secrets to source control
5. **Network Security**: Use Docker networks, firewall rules
6. **Regular Updates**: Keep Docker images and packages updated

### JWT Secret Generation:

```bash
# Generate secure JWT key
openssl rand -base64 32
```

## Monitoring and Logs

### Application Logs
```bash
# View API logs
docker-compose logs -f api

# Log files are also written to ./logs/ directory
tail -f logs/log-$(date +%Y%m%d).txt
```

### Database Logs
```bash
# View PostgreSQL logs
docker-compose logs -f postgres
```

### Health Monitoring
- Health endpoint: `/health`
- Database connectivity check included
- Use with monitoring tools like Prometheus/Grafana

## Backup and Recovery

### Database Backup
```bash
# Create backup
docker exec assetmanagement-db pg_dump -U postgres assetdb > backup_$(date +%Y%m%d).sql

# Restore backup
docker exec -i assetmanagement-db psql -U postgres assetdb < backup_20240112.sql
```

### Volume Backup
```bash
# Backup PostgreSQL data volume
docker run --rm -v assetmanagement_postgres_data:/data -v $(pwd):/backup alpine tar czf /backup/postgres_backup_$(date +%Y%m%d).tar.gz -C /data .
```

## Scaling and Performance

### Horizontal Scaling
```yaml
# In docker-compose.yml, scale API instances
api:
  # ... existing config
  deploy:
    replicas: 3
```

### Load Balancing
- Nginx configuration included for reverse proxy
- Configure upstream servers for multiple API instances
- Use external load balancer for production

### Database Optimization
- Connection pooling configured in EF Core
- Consider read replicas for high-traffic scenarios
- Monitor query performance and add indexes as needed

## Troubleshooting

### Common Issues

1. **Migration Errors**
   ```bash
   # Check database connectivity
   docker-compose logs postgres
   
   # Manually run migrations
   docker exec -it assetmanagement-api dotnet ef database update
   ```

2. **JWT Authentication Issues**
   - Verify JWT_SECRET_KEY is set and consistent
   - Check token expiration settings
   - Validate issuer/audience configuration

3. **CORS Issues**
   - Update CORS_ORIGINS in .env
   - Restart API container after changes

4. **Database Connection Issues**
   - Verify PostgreSQL is healthy: `docker-compose ps`
   - Check connection string format
   - Ensure database credentials are correct

### Debug Mode
```bash
# Run with debug logging
ASPNETCORE_ENVIRONMENT=Development docker-compose up
```

## API Endpoints

### Authentication
- `POST /api/auth/login` - User login
- `POST /api/auth/register` - User registration

### Users Management
- `GET /api/users` - List users
- `POST /api/users` - Create user
- `PUT /api/users/{id}` - Update user
- `DELETE /api/users/{id}` - Delete user

### Asset Management
- `GET /api/assets` - List assets
- `POST /api/assets` - Create asset
- `PUT /api/assets/{id}` - Update asset
- `DELETE /api/assets/{id}` - Delete asset

### Categories & Roles
- `GET /api/categories` - List categories
- `GET /api/roles` - List roles

## Support

For issues and questions:
1. Check logs: `docker-compose logs -f`
2. Verify environment configuration
3. Check database connectivity
4. Review API health endpoint