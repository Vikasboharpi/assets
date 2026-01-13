# Asset Management API (.NET 8)

A comprehensive Asset Management system built with .NET 8, Entity Framework Core, and PostgreSQL.

## 🚀 Quick Start

### Prerequisites
- .NET 8 SDK
- Docker and Docker Compose
- PostgreSQL (optional, included in Docker setup)

### Run Locally
```bash
# Clone the repository
git clone <your-repo-url>
cd AssetManagement

# Restore packages
dotnet restore

# Run the API
dotnet run --project AssetManagement.API
```

The API will be available at: `http://localhost:5224`

### Run with Docker
```bash
# Copy environment template
cp .env.example .env

# Edit .env with your settings
# Set DB_PASSWORD and JWT_SECRET_KEY

# Deploy
./deploy.sh  # Linux/macOS
deploy.bat   # Windows
```

## 📋 API Endpoints

### Health Check
- `GET /health` - Application health status

### Authentication
- `POST /api/auth/login` - User login
- `POST /api/auth/register` - User registration

### Asset Management
- `GET /api/assets` - List all assets
- `POST /api/assets` - Create new asset
- `PUT /api/assets/{id}` - Update asset
- `DELETE /api/assets/{id}` - Delete asset

### User Management
- `GET /api/users` - List users
- `POST /api/users` - Create user
- `PUT /api/users/{id}` - Update user
- `DELETE /api/users/{id}` - Delete user

### Categories & Roles
- `GET /api/categories` - List categories
- `GET /api/roles` - List roles

## 🏗️ Architecture

The project follows Clean Architecture principles:

- **AssetManagement.Domain** - Core business entities
- **AssetManagement.Application** - Business logic and services
- **AssetManagement.Infrastructure** - Data access and external services
- **AssetManagement.API** - Web API controllers and configuration

## 🛠️ Technology Stack

- **.NET 8** - Framework
- **ASP.NET Core 8.0** - Web API
- **Entity Framework Core 8.0** - ORM
- **PostgreSQL** - Database
- **AutoMapper** - Object mapping
- **FluentValidation** - Input validation
- **Serilog** - Logging
- **JWT Bearer** - Authentication
- **Swagger/OpenAPI** - API documentation
- **Docker** - Containerization

## 🔧 Development

### Database Migrations
```bash
# Add new migration
dotnet ef migrations add MigrationName --project AssetManagement.Infrastructure --startup-project AssetManagement.API

# Update database
dotnet ef database update --project AssetManagement.Infrastructure --startup-project AssetManagement.API
```

### Build and Test
```bash
# Clean and restore
dotnet clean
dotnet restore

# Build
dotnet build

# Run tests
dotnet test
```

## 🐳 Docker Deployment

The project includes complete Docker configuration:

- **Dockerfile** - Multi-stage build for the API
- **docker-compose.yml** - Full stack with PostgreSQL
- **nginx.conf** - Reverse proxy configuration
- **.env.example** - Environment variables template

### Environment Variables
```bash
# Required
DB_PASSWORD=your_secure_password
JWT_SECRET_KEY=your_32_character_secret_key

# Optional
DB_NAME=assetdb
DB_USER=postgres
JWT_ISSUER=AssetManagementAPI
JWT_AUDIENCE=AssetManagementClient
CORS_ORIGINS=https://yourdomain.com
```

## 📊 Features

- **User Management** - Role-based access control
- **Asset Tracking** - Complete asset lifecycle management
- **Categories & Brands** - Organized asset classification
- **Location Management** - Asset location tracking
- **Audit Trail** - Created/Updated timestamps
- **Health Monitoring** - Application health checks
- **Logging** - Structured logging with Serilog
- **API Documentation** - Swagger/OpenAPI integration
- **Docker Ready** - Production-ready containerization

## 🔒 Security

- JWT Bearer token authentication
- Role-based authorization
- CORS configuration
- Security headers via Nginx
- Environment-based configuration
- Password hashing (SHA256)

## 📚 Documentation

- [Deployment Guide](DEPLOYMENT.md) - Complete deployment instructions
- [API Documentation](http://localhost:5224/swagger) - Interactive API docs (Development only)

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests if applicable
5. Submit a pull request

## 📄 License

This project is licensed under the MIT License.