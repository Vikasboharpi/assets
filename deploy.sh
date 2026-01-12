#!/bin/bash

# Asset Management API Deployment Script

set -e

echo "🚀 Starting Asset Management API Deployment..."

# Check if .env file exists
if [ ! -f ".env" ]; then
    echo "❌ .env file not found. Please create one based on .env.example"
    echo "   cp .env.example .env"
    echo "   Then edit .env with your production values"
    exit 1
fi

# Load environment variables
source .env

# Validate required environment variables
required_vars=("DB_PASSWORD" "JWT_SECRET_KEY")
for var in "${required_vars[@]}"; do
    if [ -z "${!var}" ]; then
        echo "❌ Required environment variable $var is not set"
        exit 1
    fi
done

# Create logs directory if it doesn't exist
mkdir -p logs

# Stop existing containers
echo "🛑 Stopping existing containers..."
docker-compose down

# Remove old images (optional - uncomment if you want to force rebuild)
# echo "🗑️  Removing old images..."
# docker-compose down --rmi all

# Build and start containers
echo "🔨 Building and starting containers..."
docker-compose up --build -d

# Wait for services to be healthy
echo "⏳ Waiting for services to be healthy..."
sleep 30

# Check if API is responding
echo "🔍 Checking API health..."
max_attempts=30
attempt=1

while [ $attempt -le $max_attempts ]; do
    if curl -f http://localhost:8080/health > /dev/null 2>&1; then
        echo "✅ API is healthy!"
        break
    else
        echo "⏳ Attempt $attempt/$max_attempts - API not ready yet..."
        sleep 10
        ((attempt++))
    fi
done

if [ $attempt -gt $max_attempts ]; then
    echo "❌ API failed to become healthy within expected time"
    echo "📋 Container logs:"
    docker-compose logs api
    exit 1
fi

# Show running containers
echo "📊 Running containers:"
docker-compose ps

echo "🎉 Deployment completed successfully!"
echo "📍 API is available at: http://localhost:8080"
echo "📍 Database is available at: localhost:5432"
echo "📍 Health check: http://localhost:8080/health"

if [ "${ASPNETCORE_ENVIRONMENT:-Production}" = "Development" ]; then
    echo "📍 Swagger UI: http://localhost:8080/swagger"
fi

echo ""
echo "📝 Useful commands:"
echo "   View logs: docker-compose logs -f"
echo "   Stop services: docker-compose down"
echo "   Restart services: docker-compose restart"
echo "   View API logs: docker-compose logs -f api"
echo "   View DB logs: docker-compose logs -f postgres"