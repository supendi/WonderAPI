# WonderAPI
Senja Solusi Test - Web API

## Description And Examples
This is a web API written in C# + ASP .NET Core 3.0. It has the following functionalties:
1. Authentication. 
   It returns a JWT Access Token.
   Request: POST /api/auth 
   Body:
   ```
   {
    "email": "joe@email.com",
    "password": "joe123"
   }
   ```
   Success Response: 200
   ```
   {
    "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxIiwiZ2l2ZW5fbmFtZSI6ImpvZSIsImJpcnRoZGF0ZSI6IjAxLzAxLzE5ODAgMDA6MDA6MDAiLCJnZW5kZXIiOiJsYWtpIiwibmJmIjoxNTgxMzA0NzE3LCJleHAiOjE1ODEzMDgzMTd9.PiywXr43NImXf-1PT4JIfgYbLzQy9Q_D2uaQH_4Si-M",
    "refreshToken": "e36a4c0a-8543-47ab-a0e9-4befe9317866"
   }
    ```
    Validation Error Response: 400
    ```
    {
    "message": "One or more validation error should be fixed.",
    "errors": [
        {
            "field": "Email",
            "errorMessage": "The Email field is required.",
            "subErrors": []
        },
        {
            "field": "Password",
            "errorMessage": "The Password field is required.",
            "subErrors": []
        }
    ]
    }
    ```
    Invalid Auth Response: 400
    ```
    {
    "message": "Invalid email or password.",
    "errors": []
    }
    ```
    
2. Register.
   Registers a new member.
   Request: POST /api/members/register
   Body:
   ```
   { 
    "name": "Andri",
    "email": "andri@email.com",
    "optionalEmail": "",
    "password": "joe123",
    "mobileNumber": "0813",
    "gender": "laki",
    "dateOfBirth": "1980-01-01T00:00:00"
   }
   ```
   Success Response: 200
   ```
   {
    "id": 2,
    "name": "Andri",
    "email": "andri@email.com",
    "optionalEmail": "",
    "mobileNumber": "0813",
    "gender": "laki",
    "dateOfBirth": "1980-01-01T00:00:00"
   }
   ```
   Validation Error Response: 400
   ```
   {
    "message": "One or more validation error should be fixed.",
    "errors": [
        {
            "field": "Name",
            "errorMessage": "The Name field is required.",
            "subErrors": []
        },
        {
            "field": "Email",
            "errorMessage": "The Email field is required.",
            "subErrors": []
        },
        {
            "field": "Password",
            "errorMessage": "The Password field is required.",
            "subErrors": []
        }
    ]
   }
   ```
   Other Error Response: 400
   ```
   {
    "message": "Email 'joe@email.com' already registered",
    "errors": []
   }
   ```
3. Update member.
   Updates an existing member.
   Request: PUT /api/members
   Authorization Header: Bearer {JWTAccessToken}
   ```
   {
    "id": 2,
    "name": "Andri Tawakal",
    "email": "andri@email.com",
    "optionalEmail": "",
    "mobileNumber": "0813",
    "gender": "laki",
    "dateOfBirth": "1980-01-01T00:00:00"
   }
   ```
   Success Response: 200
   ```
   {
    "id": 2,
    "name": "Andri Tawakal",
    "email": "andri@email.com",
    "optionalEmail": "",
    "mobileNumber": "0813",
    "gender": "laki",
    "dateOfBirth": "1980-01-01T00:00:00"
   }
   ```
   Error Unauthorized: 401 
   
4. Get user info
   Gets user info from access token
   Request: GET /api/members/me
   Authorization Header: Bearer {JWTAccessToken}
   Success Response: 
   ```
   {
    "id": 1,
    "name": "joe",
    "email": "joe@email.com",
    "optionalEmail": "",
    "mobileNumber": "0813",
    "gender": "laki",
    "dateOfBirth": "1980-01-01T00:00:00"
    }
   ```
   Error Unauthorized: 401 
5. Refresh Token
   Renew or request a new access token
   Request: POST /api/auth/refresh-token
   Body:
   ```
   {
    "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIyIiwiZ2l2ZW5fbmFtZSI6ImpvZSIsImJpcnRoZGF0ZSI6IjAxLzAxLzE5ODAgMDA6MDA6MDAiLCJnZW5kZXIiOiJsYWtpIiwibmJmIjoxNTgwNzg5Mzc1LCJleHAiOjE1ODA3OTI5NzV9.7U0RECg74x8S17WWOKtoIKjRE9VcZcr4YvXwYjfGC28",
    "refreshToken": "6e70ef7d-36d7-453f-b2ac-90003716920b"
    }
    ```
   SuccessResponse:
   ```
   {
    "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxIiwiZ2l2ZW5fbmFtZSI6ImpvZSIsImJpcnRoZGF0ZSI6IjAxLzAxLzE5ODAgMDA6MDA6MDAiLCJnZW5kZXIiOiJsYWtpIiwibmJmIjoxNTgxMzA2MDE3LCJleHAiOjE1ODEzMDk2MTd9.YfYSpprQ_kC8Uc_-67R_X4m3-kdvr128P3-MprtwLJg",
    "refreshToken": "09a21081-67e0-449e-ae22-5df65c5ca129"
   }
   ```
   Error Response:
   ```
   {
    "message": "Invalid refresh token.",
    "errors": []
   }
   ```
   
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
   - CLI : run 'dotnet build' it will also automatically resolves the project dependencies.
2. Run migration.
   - Visual Studio (I use VS2019) : run 'update-database' command in package manager console.
   - For CLI : run 'dotnet ef database update'. Make sure you are in 'WonderAPI' project directory while running the command line.
     For troubleshoot see: https://devblogs.microsoft.com/dotnet/announcing-entity-framework-core-3-0-preview-4/ 
3. Make sure environment variables has been set.
   Or if you want to manually hard coded this one, here's the files:
   - WonderDBContext.cs : set the connection string
   - JWTGenerator.cs : set the jwt secret key
4. Run the project

## Unit test
1. VS2019 : Open test explorer. Run all test.
2. CLI : run command 'dotnet test' under project WonderApiTests

## Postman collection
Yes, There is postman collection on this repository.
