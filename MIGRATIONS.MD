# For PostgreSQL (Production)

$env:ASPNETCORE_ENVIRONMENT='Production'
dotnet ef migrations add AddNewFeature

# For SQL Server (Local) - with naming convention

$env:ASPNETCORE_ENVIRONMENT='Development'  
dotnet ef migrations add SqlServer_AddNewFeature
