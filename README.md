## Description


Welcome to the FinFlex API! This API serves as the backend for the FinFlex application, providing endpoints for managing financial transactions, budgeting, and expense tracking. It offers a comprehensive solution for users to organize their finances effectively.

## Table of Contents
- [Features](#Features)
- [Installation](#Installation)
- [Usage](#Usage)
- [Endpoints](#Endpoints)
- [Contributing](#contributing)

## Features

The FinFlex API offers the following features:

- **Stocks Management**: Allows users to create, update, and delete financial transactions, providing flexibility and control over their financial records and make it's own portfolio.
- **Portfolios Tracking**: Enables users to track their Portfolio efficiently, categorizing them for better analysis and management.
- **Comments Management**: Offers to user to comment for stocks to share the experience with this stock.
- **Authentication**: Implements JWT-based authentication to secure endpoints, ensuring data privacy and user authentication.
- **Comprehensive Documentation**: Provides comprehensive API documentation using Swagger UI, making it easier for developers to understand and utilize the available endpoints.
## Installation

To run the FinFlex API locally, follow these steps:

1. Clone this repository:

    ```bash
    git clone https://github.com/yousefsaad12/FinFlex-Api.git
    ```

2. Navigate to the project directory:

    ```bash
    cd FinFlex-Api
    ```

3. Start the server:

    ```bash
    dotnet watch run
    ```

The server will start running at `http://localhost:3000` by default.

## Usage

To use the FinFlex API, follow these guidelines:

- **Authentication**: Obtain a JWT token by making a POST request to `/api/Account/login` or `api/Account/register` with valid credentials.
- **Endpoints**: Utilize various endpoints to manage transactions, budgets, and categories. Refer to the API documentation for details on available endpoints and their usage.



## Endpoints


### Authentication

- **POST /api/Account/register**
  - Description: Authenticate a user and generate a JWT token for accessing protected endpoints.
  - Request Body:
    ```json
    {
        "userName": "string",
        "email": "user@example.com",
        "password": "string"
    }
    ```
  - Response:
    ```json
    { 
        "UserName": "userexample",
        "Email": "user@example.com",
        "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
    }
    ```

- **POST /api/Account/login**
  - Description: Authenticate a user and generate a JWT token for accessing protected endpoints.
  - Request Body:
    ```json
    {
        "email": "user@example.com",
        "password": "password123"
    }
    ```
  - Response:
    ```json
    { 
        "UserName": "userexample",
        "Email": "user@example.com",
        "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
    }
    ```

### Stocks

- **GET /api/Stocks**
  - Description: Get all Stocks.
  - You can apple filter and pagination 
  - Authorization: Bearer Token (JWT)
  - Response:
    ```json
    [
      {
        "id": 1,
        "symbol": "TSLA",
        "companyName": "TESLA",
        "purchase": 1000000000,
        "lastDiv": 100,
        "industry": "string",
        "marketCap": 5000000000,
        "comments": [
            {
                "id": 1,
                "titel": "Good Stock",
                "content": "string",
                "createdOn": "2024-02-01T16:47:17.435881",
                "createdBy": "User1",
                "stockId": 1
            }
    ]
      },
      {
        "id": 2,
        "symbol": "PLTR",
        "companyName": "Palantir Technologies",
        "purchase": 15000000,
        "lastDiv": 100,
        "industry": "string",
        "marketCap": 190000000,
        "comments": []
      }
    ]
    ```

- **POST /api/Stock**
  - Description: Create a new transaction.
  - Authorization: Bearer Token (JWT)
  - Request Body:
    ```json
    {
        "symbol": "string",
        "companyName": "string",
        "purchase": 1000000000,
        "lastDiv": 100,
        "industry": "string",
        "marketCap": 5000000000
    }
    ```
  - Response:
    ```json
    {   
        "Id": "1"
        "symbol": "string",
        "companyName": "string",
        "purchase": 1000000000,
        "lastDiv": 100,
        "industry": "string",
        "marketCap": 5000000000
    }
    ```

- **PUT /api/Stock/:id**
  - Description: Update an existing Stock by ID.
  - Authorization: Bearer Token (JWT)
  - Request Body:
    ```json
    {
        "symbol": "string",
        "companyName": "string",
        "purchase": 1000000000,
        "lastDiv": 100,
        "industry": "string",
        "marketCap": 5000000000
    }
    ```
  - Response:
    ```json
    {
        "id": "1",
        "symbol": "string",
        "companyName": "string",
        "purchase": 1000000000,
        "lastDiv": 100,
        "industry": "string",
        "marketCap": 5000000000
    }
    ```



- **GET /api/Stock/:id**
  - Description: Get a Stock by ID.
  - Authorization: Bearer Token (JWT)
  - Response:
    ```json
    {
        "id": "1",
        "symbol": "string",
        "companyName": "string",
        "purchase": 1000000000,
        "lastDiv": 100,
        "industry": "string",
        "marketCap": 5000000000
    }
    ```


- **DELETE /api/Stock/:id**
  - Description: Delete a Stock by ID.
  - Authorization: Bearer Token (JWT)
  - Response:
   No Content

### Comments

- **GET /api/Comment**
  - Description: Get all Comments.
  - Authorization: Bearer Token (JWT)
  - Response:
    ```json
    [
      {
        "id": 1,
        "titel": "Good Stock",
        "content": "string",
        "createdOn": "2024-02-01T16:47:17.435881",
        "createdBy": "User1",
        "stockId": 1
      },
      
    ]
    ```

- **GET /api/Comment/:id**
  - Description: Get Comment by Id.
  - Authorization: Bearer Token (JWT)
  - Response:
    ```json
    [
      {
        "id": 1,
        "titel": "Good Stock",
        "content": "string",
        "createdOn": "2024-02-01T16:47:17.435881",
        "createdBy": "User1",
        "stockId": 1
      },
      
    ]
    ```


- **POST /api/Comment/:stockId**
  - Description: Create a new Comment.
  - Authorization: Bearer Token (JWT)
  - Request Body:
    ```json
    {
        "titel": "string",
        "content": "string"
    }
    ```
  - Response:
    ```json
    {
        "id": 1,
        "titel": "Good Stock",
        "content": "string",
        "createdOn": "2024-02-01T16:47:17.435881",
        "createdBy": "User1",
        "stockId": 1
    }
    ```

- **PUT /api/Comment/:id**
  - Description: Update an existing Comment by ID.
  - Authorization: Bearer Token (JWT)
  - Request Body:
    ```json
    {
        "titel": "string",
        "content": "string"
    }
    ```
  - Response:
    ```json
    {
        "id": 1,
        "titel": "Good Stock",
        "content": "string",
        "createdOn": "2024-02-01T16:47:17.435881",
        "createdBy": "User1",
        "stockId": 1
    }
    ```

- **DELETE /api/Comment/:id**
  - Description: Delete a Comment by ID.
  - Authorization: Bearer Token (JWT)
  - Response:
    ```json
    {
        "id": 1,
        "titel": "Good Stock",
        "content": "string",
        "createdOn": "2024-02-01T16:47:17.435881",
        "createdBy": "User1",
        "stockId": 1
    }
    ```

### Portfolio

- **GET /api/Portfolio**
  - Description: Get all Portfolios for login user.
  - Authorization: Bearer Token (JWT)
  - Response:
    ```json
    [
       {
         "id": 1,
         "symbol": "TSLA",
         "companyName": "TESLA",
         "purchase": 1000000000,
         "lastDiv": 100,
         "industry": "string",
         "marketCap": 5000000000,
         "comments": [],
         "portfolios": []
        },
        {
        "id": 2,
        "symbol": "PLTR",
        "companyName": "Palantir Technologies",
        "purchase": 15000000,
        "lastDiv": 100,
        "industry": "string",
        "marketCap": 190000000,
        "comments": [],
        "portfolios": []
        }
    ]
    ```

- **POST /api/Portfolio/:StockSymbol**
  - Description: Create Portfolio for login user by stock symbol.
  - Authorization: Bearer Token (JWT)
  - Response:
    ```json
    {
        "message": "Stock has been added"
    }
    ```

- **DELETE /api/Portfolio/:StockSymbol**
  - Description: Delete a Portfolio for login user by stock symbol.
  - Authorization: Bearer Token (JWT)
  
