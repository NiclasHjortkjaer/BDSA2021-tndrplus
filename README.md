# BDSA2021-tndrplus
# This software was developed as part of the course: Analysis, Design and Software Architecture (2021)

# Run the program
Prerequisites:
1. Have docker desktop installed on your machine: https://docs.docker.com/get-docker/
2. Clone the repository

# Option 1: Docker Compose.
1. Generate a self signed certificate using the following commands: 

Windows: 
```powershell  
dotnet dev-certs https --clean
dotnet dev-certs https --export-path $env:USERPROFILE\.aspnet\https\aspnetapp.pfx --password localhost --trust
dotnet dev-certs https --trust
```
Mac/linux: 
```zsh
dotnet dev-certs https --clean
dotnet dev-certs https -ep ${HOME}/.aspnet/https/aspnetapp.pfx -p localhost
dotnet dev-certs https --trust
```
2. Create a new folder: ".local" in the same folder as the "docker-compose.yml" from the projectbank folder and add the sectret files:
From within the ProjectBank folder on the local machine:
```powershell
mkdir .local
cd .local
```
Create the files:
- Database password
```powershell
$password = New-Guid
echo "$password" > db_password.txt
```
- Database ConnectionString
```powershell
echo "Server=projectbank-db-1;Database=ProjectBank;User Id=sa;Password=$password;Trusted_Connection=False;Encrypt=False" > connection_string.txt
```
- Azure Storage key 
- *(Optimally this should be omitted in favor of using AAD for authentication or azure key vault. However we did not succeed in getting that to work -)*
- *(- the key is alternativly avaliable in the report appendices)*
```Access the storage account key using the ProjectBank tenant on Azure Hub.
1. Login and access azure hub https://portal.azure.com/
2. Navigate to the ProjectBank directory by clicking the topright corner and choosing "Switch directory". 
   - Locate the directory "ProjectBank" and hit switch
3. Navigate to "Storage Accounts" using the Azure´s searchbar
4. Choose the account "projectbankstorage"
5. Using the search bar on the left. Search "Access Keys".
6. Use the show keys option and copy one of the secre keys
```

```powershell
echo "<Secret key>" > storage_key.txt
```

3. Navigate back to the ProjectBank folder.
- (If you are running an arm64 machine open the "docker-compose.yml" file and switch the mssql image to the outcommented azure-sql-edge image.)
Run the program:
```powershell
docker-compose up
```

# Run the program. Option 2: start-application.ps1
-Prerequisites: Have the dotnet 6 sdk and runtime installed.
```
1. Navigate to the ProjectBank folder
2. Access the the azure storage key the same way as described above.
3. Navigate to the ProjectBank/server/appsetting.json file
4. Change the "StorageAccountKey" value to the string received from the storage account on Azure hub.
- (If you are running an arm64 machine open the "start-application.ps1" file and switch the mssql image to the outcommented azure-sql-edge image.)
6. Enter the command:
```

```powershell
start-application.ps1 -SQL
```

