
# This script is used to test the endpoints
param(
    [string]$environment = "Development",
    [string]$launchProfile = "https-Development",
    [string]$connectionStringKey = "BooksDb",
    [bool]$dropDatabase = $false,
    [bool]$createDatabase = $false
)

# Get the project name
$projectName = Get-ChildItem -Recurse -Filter "*.csproj" | Select-Object -First 1 | ForEach-Object { $_.Directory.Name } # Projectname can also be set manually

# Get the base URL of the project
$launchSettings = Get-Content -LiteralPath ".\$projectName\Properties\launchSettings.json" | ConvertFrom-Json
$baseUrl = ($launchSettings.profiles.$launchProfile.applicationUrl -split ";")[0] # Can also set manually -> $baseUrl = "https://localhost:7253"

#Install module SqlServer
if (-not (Get-Module -ErrorAction Ignore -ListAvailable SqlServer)) {
    Write-Verbose "Installing SqlServer module for the current user..."
    Install-Module -Scope CurrentUser SqlServer -ErrorAction Stop
}
Import-Module SqlServer

# Set the environment variable
$env:ASPNETCORE_ENVIRONMENT = $environment



# Read the connection string from appsettings.Development.json
$appSettings = Get-Content ".\$projectName\appsettings.$environment.json" | ConvertFrom-Json
$connectionString = $appSettings.ConnectionStrings.$connectionStringKey
Write-Host "Database Connection String: $connectionString" -ForegroundColor Blue


# Get the database name from the connection string
if ($connectionString -match "Database=(?<dbName>[^;]+)")
{
    $databaseName = $matches['dbName']
    Write-Host "Database Name: $databaseName" -ForegroundColor Blue
}else{
    Write-Host "Database Name not found in connection string" -ForegroundColor Red
    exit
}


# Check if the database exists
$conStringDbExcluded = $connectionString -replace "Database=[^;]+;", ""
$queryDbExists = Invoke-Sqlcmd -ConnectionString $conStringDbExcluded -Query "Select name FROM sys.databases WHERE name='$databaseName'"
if($queryDbExists){
    if($dropDatabase -or (Read-Host "Do you want to drop the database? (y/n)").ToLower() -eq "y") { 

        # Drop the database
        Invoke-Sqlcmd -ConnectionString $connectionString -Query  "USE master;ALTER DATABASE $databaseName SET SINGLE_USER WITH ROLLBACK IMMEDIATE;DROP DATABASE $databaseName;"
        Write-Host "Database $databaseName dropped." -ForegroundColor Green
    }
}

# Create the database from the model
if(Select-String -LiteralPath ".\$projectName\Program.cs" -Pattern "EnsureCreated()"){
    Write-Host "The project uses EnsureCreated() to create the database from the model." -ForegroundColor Yellow
} else {
    if($createDatabase -or (Read-Host "Should dotnet ef migrate and update the database? (y/n)").ToLower() -eq "y") { 

        dotnet ef migrations add "UpdateModelFromScript_$(Get-Date -Format "yyyyMMdd_HHmmss")" --project ".\$projectName\$projectName.csproj"
        dotnet ef database update --project ".\$projectName\$projectName.csproj"
    }
}

# Run the application
if((Read-Host "Start the server from Visual studio? (y/n)").ToLower() -ne "y") { 
    Start-Process -FilePath "dotnet" -ArgumentList "run --launch-profile $launchProfile --project .\$projectName\$projectName.csproj" -WindowStyle Normal    
    Write-Host "Wait for the server to start..." -ForegroundColor Yellow 
}

# Continue with the rest of the script
Read-Host "Press Enter to continue when the server is started..."



### =============================================================
### =============================================================
### =============================================================

# Send requests to the API endpoint


### ------------Post an author

Write-Host "`nCreate an Author"

$httpMethod = "Post"   ### "Get", "Post", "Put", "Delete"

$endPoint = "$baseUrl/api/Authors"

$json = '{
  "firstName": "Michel",
  "lastName": "Foucault"
}'

$response = Invoke-RestMethod -Uri $endPoint -Method $httpMethod -Body $json -ContentType "application/json"
$response | Format-Table

### ------------Post another author

Write-Host "`nCreate another Author"

$httpMethod = "Post"   ### "Get", "Post", "Put", "Delete"

$endPoint = "$baseUrl/api/Authors"

$json = '{
  "firstName": "Donna",
  "lastName": "Haraway"
}'

$response = Invoke-RestMethod -Uri $endPoint -Method $httpMethod -Body $json -ContentType "application/json"
$response | Format-Table

### ------------Post another author

Write-Host "`nCreate another Author"

$httpMethod = "Post"   ### "Get", "Post", "Put", "Delete"

$endPoint = "$baseUrl/api/Authors"

$json = '{
  "firstName": "Timothy",
  "lastName": "Morton"
}'

$response = Invoke-RestMethod -Uri $endPoint -Method $httpMethod -Body $json -ContentType "application/json"
$response | Format-Table


### ------------Post a book

Write-Host "`nCreate a book"

$httpMethod = "Post"   ### "Get", "Post", "Put", "Delete"

$endPoint = "$baseUrl/api/Books"

$json = '{
  "title": "The Order of Things",
  "isbn": "2070224848",
  "releaseYear": 1970,
  "authorIds": [
    1
  ],
  "rating": "string"
}'

$response = Invoke-RestMethod -Uri $endPoint -Method $httpMethod -Body $json -ContentType "application/json"
$response | Format-Table

### ------------Post another book

Write-Host "`nCreate another book"

$httpMethod = "Post"   ### "Get", "Post", "Put", "Delete"

$endPoint = "$baseUrl/api/Books"

$json = '{
  "title": "The Archaeology of Knowledge",
  "isbn": "123454321",
  "releaseYear": 1969,
  "authorIds": [
    1
  ],
  "rating": "Difficult"
}'

$response = Invoke-RestMethod -Uri $endPoint -Method $httpMethod -Body $json -ContentType "application/json"
$response | Format-Table

### ------------Post another book

Write-Host "`nCreate another book"

$httpMethod = "Post"   ### "Get", "Post", "Put", "Delete"

$endPoint = "$baseUrl/api/Books"

$json = '{
  "title": "The Cyborg Manifesto",
  "isbn": "202222202",
  "releaseYear": 1988,
  "authorIds": [
    2
  ],
  "rating": "Great"
}'

$response = Invoke-RestMethod -Uri $endPoint -Method $httpMethod -Body $json -ContentType "application/json"
$response | Format-Table

### ------------Post another book

Write-Host "`nCreate another book"

$httpMethod = "Post"   ### "Get", "Post", "Put", "Delete"

$endPoint = "$baseUrl/api/Books"

$json = '{
  "title": "Hyperobjects",
  "isbn": "303333302",
  "releaseYear": 2014,
  "authorIds": [
    3
  ],
  "rating": "Mind blowing"
}'


### ------------Post a library user

Write-Host "`nCreate a library user"

$httpMethod = "Post"   ### "Get", "Post", "Put", "Delete"

$endPoint = "$baseUrl/api/LibraryUsers"

$json = '{
  "firstName": "Laura",
  "lastName": "Palmer"
}'

$response = Invoke-RestMethod -Uri $endPoint -Method $httpMethod -Body $json -ContentType "application/json"
$response | Format-Table

### ------------Post another library user

Write-Host "`nCreate another library user"

$httpMethod = "Post"   ### "Get", "Post", "Put", "Delete"

$endPoint = "$baseUrl/api/LibraryUsers"

$json = '{
  "firstName": "Dale",
  "lastName": "Cooper"
}'

$response = Invoke-RestMethod -Uri $endPoint -Method $httpMethod -Body $json -ContentType "application/json"
$response | Format-Table


### ------------ Query all books from the database
$sqlResult = Invoke-Sqlcmd -ConnectionString $connectionString -Query "Select * FROM Books"
$sqlResult | Format-Table



### ------------ Query a book from the database by Id
$sqlResult = Invoke-Sqlcmd -ConnectionString $connectionString -Query "Select * FROM Books WHERE Id = 1"
$sqlResult | Format-Table


### ------------Post a book loan

Write-Host "`nCreate a book loan"

$httpMethod = "Post"   ### "Get", "Post", "Put", "Delete"

$endPoint = "$baseUrl/api/Checkouts/Borrow"

$json = '{
  "libraryUserId": 1,
  "bookIds": [
    1, 2, 4
  ]
}'

$response = Invoke-RestMethod -Uri $endPoint -Method $httpMethod -Body $json -ContentType "application/json"
$response | Format-Table


### ------------Post a book loan

Write-Host "`nCreate another book loan"

$httpMethod = "Post"   ### "Get", "Post", "Put", "Delete"

$endPoint = "$baseUrl/api/Checkouts/Borrow"

$json = '{
  "libraryUserId": 2,
  "bookIds": [
    3, 4
  ]
}'

$response = Invoke-RestMethod -Uri $endPoint -Method $httpMethod -Body $json -ContentType "application/json"
$response | Format-Table

### ------------Put a book return

Write-Host "`nCreate a book return"

$httpMethod = "Put"   ### "Get", "Post", "Put", "Delete"

$endPoint = "$baseUrl/api/Checkouts/Return/{id}"

$json = '{
  "id": 2
}'


### ------------Delete a library user

Write-Host "`nDelete a library user"

$httpMethod = "Delete"   ### "Get", "Post", "Put", "Delete"

$endPoint = "$baseUrl/api/LibraryUsers/{id}"

$json = '{
  "id": 2
}'


### ------------Delete a book

Write-Host "`nDelete a book"

$httpMethod = "Delete"   ### "Get", "Post", "Put", "Delete"

$endPoint = "$baseUrl/api/Books/{id}"

$json = '{
  "id": 2
}'

### ------------Delete a book

Write-Host "`nDelete an author"

$httpMethod = "Delete"   ### "Get", "Post", "Put", "Delete"

$endPoint = "$baseUrl/api/Authors/{id}"

$json = '{
  "id": 3
}'


###############
















# Write-Host "Running the script"

# # Define the API endpoint
# $baseUrl = "https://localhost:7145"

# # Making a POST request to LibraryUser
# $body = @{
#     libraryUserID = 0
#     libraryCardNumber = "ABC123"
#     ufirstName = "Sara"
#     lastName = "Kleppe"
# } | ConvertTo-Json

# $url = "https://localhost:7145/api/LibraryUsers"

# $response = Invoke-RestMethod -Method Post -Uri $url -Body $body -ContentType 'application/json'
# # Output the response
# $response

# # Making a GET request to a specific LibraryUser
# # With error handling
# try {
# $url = "https://localhost:7145/api/LibraryUsers/1"
# $response = Invoke-RestMethod -Uri $url 
# # Output the response
# $response
# } catch {
#     Write-Error "An error occurred: $_"
# }

