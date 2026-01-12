@echo off
setlocal enabledelayedexpansion

echo 🚀 Starting Asset Management API Deployment...

REM Check if .env file exists
if not exist ".env" (
    echo ❌ .env file not found. Please create one based on .env.example
    echo    copy .env.example .env
    echo    Then edit .env with your production values
    exit /b 1
)

REM Create logs directory if it doesn't exist
if not exist "logs" mkdir logs

REM Stop existing containers
echo 🛑 Stopping existing containers...
docker-compose down

REM Build and start containers
echo 🔨 Building and starting containers...
docker-compose up --build -d

REM Wait for services to be healthy
echo ⏳ Waiting for services to be healthy...
timeout /t 30 /nobreak > nul

REM Check if API is responding
echo 🔍 Checking API health...
set max_attempts=30
set attempt=1

:health_check_loop
curl -f http://localhost:8080/health > nul 2>&1
if !errorlevel! equ 0 (
    echo ✅ API is healthy!
    goto :health_check_success
)

echo ⏳ Attempt !attempt!/!max_attempts! - API not ready yet...
timeout /t 10 /nobreak > nul
set /a attempt+=1

if !attempt! leq !max_attempts! goto :health_check_loop

echo ❌ API failed to become healthy within expected time
echo 📋 Container logs:
docker-compose logs api
exit /b 1

:health_check_success
REM Show running containers
echo 📊 Running containers:
docker-compose ps

echo 🎉 Deployment completed successfully!
echo 📍 API is available at: http://localhost:8080
echo 📍 Database is available at: localhost:5432
echo 📍 Health check: http://localhost:8080/health
echo 📍 Swagger UI: http://localhost:8080/swagger (if in Development mode)
echo.
echo 📝 Useful commands:
echo    View logs: docker-compose logs -f
echo    Stop services: docker-compose down
echo    Restart services: docker-compose restart
echo    View API logs: docker-compose logs -f api
echo    View DB logs: docker-compose logs -f postgres

pause