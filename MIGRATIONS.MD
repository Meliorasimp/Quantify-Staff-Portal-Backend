# For PostgreSQL (Production)

# 1. Create migration

$env:ASPNETCORE_ENVIRONMENT='Production'
dotnet ef migrations add PostgreSQL_NewFeature

# For SQL Server (Local) - with naming convention

$env:ASPNETCORE_ENVIRONMENT='Development'  
dotnet ef migrations add SqlServer_AddNewFeature
