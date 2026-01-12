-- Asset Management Database Initialization Script
-- This script runs when PostgreSQL container starts for the first time

-- Create database if it doesn't exist (handled by POSTGRES_DB env var)
-- The database is automatically created by the PostgreSQL Docker image

-- Set timezone
SET timezone = 'UTC';

-- Create extensions if needed
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

-- Log initialization
DO $$
BEGIN
    RAISE NOTICE 'Asset Management Database initialized successfully at %', NOW();
END $$;