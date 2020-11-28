param(
    [boolean]$Recreate = $False,
    [boolean]$Rebuild = $True,
    [boolean]$Migration = $True
)

$env:ASPNETCORE_ENVIRONMENT="Development"
$DatabaseName="BiggyBank"
$DbUserName="SA"
$DbPass="f#Vf8sYBF2"
$MigrationTool="RunMigration/bin/Debug/netcoreapp3.1/RunMigration.dll"

Set-Location ../backend

if ($Recreate) {
	Write-Host "Recreate Database $DatabaseName"
    docker exec -it bank_mssql /opt/mssql-tools/bin/sqlcmd -S localhost -U $DbUserName -P "$DbPass" -Q "DROP DATABASE [$DatabaseName]"
    docker exec -it bank_mssql /opt/mssql-tools/bin/sqlcmd -S localhost -U $DbUserName -P "$DbPass" -Q "CREATE DATABASE [$DatabaseName]"
}

if ($Rebuild) {
    dotnet build
}
if ($Migration) {
    dotnet $MigrationTool --profile Development
}

Set-Location ../docker-env
