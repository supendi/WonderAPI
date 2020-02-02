# WonderAPI
Senja Solusi Test - Web API

## Env
There are two variables can/should be set in environment variables:
1. Connection String. 
   It contains database connection string. If null, default value will be used, and it's hard coded.
   Env name/key = 'WonderDB'
2. Jwt Secret key.
   Contains secret key used for encoding the JWT token. If null, default value will be used.
   Env name/key = 'JwtSecret'

## Step to run the project
1. Build the project.
   - Visual Studio (I use VS2019): Build and let the package manager resolves the project dependencies.
   - CLI : run 'dotnet build' it will also resolves automatically resolves the project dependencies.
2. Run migration.
   - Visual Studio (I use VS2019) : run 'update-database' command in package manager console.
   - For CLI : run 'dotnet ef database update'. Make sure you are in 'WonderAPI' project directory while running the command line.
     For troubleshoot see: https://devblogs.microsoft.com/dotnet/announcing-entity-framework-core-3-0-preview-4/ 
3. Make sure environment variables has been set.
   Or if you want to manually hard coded this one, here's the files:
   - WonderDBContext.cs : set the connection string
   - JWTGenerator.cs : set the jwt secret key
4. Run the project

## Postman collection
Yes, There is postman collection on this repository.
