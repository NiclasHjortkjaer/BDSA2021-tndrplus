[CmdletBinding()]
param (
    [switch]
    $SQL
)

$project = "Server"

if ($SQL) {
    $password = New-Guid

    Write-Host "Starting SQL Server"
    docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=$password" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest
    $database = "ProjectBank"
    $connectionString = "Server=localhost;Database=$database;User Id=sa;Password=$password;Trusted_Connection=False;Encrypt=False"

    Write-Host "Configuring Connection String"
    dotnet user-secrets init --project $project
    dotnet user-secrets set "ConnectionStrings:ProjectBank" "$connectionString" --project $project
}

Write-Host "Starting App"
dotnet run --project $project