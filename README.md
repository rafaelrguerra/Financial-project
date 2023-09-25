# Bank API Project

Welcome to the Bank API project! This project is designed to provide a simple and efficient way to manage customer accounts and perform transactions within a bank. Tech used: ASP.NET 6.0 and Entity Framework 6.0

## Account Controller

### 1. Get Account Balance by Account Number
Endpoint: GET /api/account/getBalance/{accountNumber}

Description: Retrieve the balance for a specific account using its account number.

Request:

HTTP Method: GET
Parameters: accountNumber (string) - The account number for which you want to retrieve the balance.
Response:

Status Code: 200 OK

Response body:
```json
{
  "id": 1,
  "accountNumber": "0001",
  "balance": 49,
  "customerId": 1
}
```

### 2. Get Accounts by Customer ID
Endpoint: GET /api/account/getAccounts/{customerId}

Description: Retrieve all accounts belonging to a specific customer using their customer ID.

Request:

HTTP Method: GET
Parameters: customerId (int) - The customer's ID for whom you want to retrieve accounts.
Response:

Status Code: 200 OK

Response Body:
```json
[
  {
    "id": 1,
    "accountNumber": "0001",
    "balance": 49,
    "customerId": 1
  },
  {
    "id": 2,
    "accountNumber": "0002",
    "balance": 151,
    "customerId": 1
  }
]
```

### 3. Create Account
Endpoint: POST /api/account/CreateAccount

Description: Create a new bank account for a customer with an initial deposit amount.

Request:

HTTP Method: POST

Parameters:
- initialDeposit (decimal) - The initial deposit amount for the new account.
- 
Request Body:
```json
{
  "accountNumber": "002",
  "customerId": 1
}
```

Response:

Status Code: 200 OK
Response Body:
```json
{
  "id": 1,
  "accountNumber": "002",
  "balance": 1,
  "customerId": 1
}
```
## Customer Controller
### 1. Get All Customers
Endpoint: GET /api/customer/getAll

Description: Retrieve a list of all customers in the bank.

Request:

HTTP Method: GET
Response:

Status Code: 200 OK

Response Body:
```json
[
  {
    "id": 1,
    "name": "John Doe",
    "identityCard": "1234"
  },
  {
    "id": 2,
    "name": "Mary",
    "identityCard": "0002"
  }
]
```

### 2. Get Customer by Identity Card
Endpoint: GET /api/customer/getCustomer/{identityCard}

Description: Retrieve a customer's information using their identity card number.

Request:

HTTP Method: GET
Parameters: identityCard (string) - The customer's identity card number.
Response:

Status Code: 200 OK

Response Body:
```json
{
  "id": 1,
  "name": "John Doe",
  "identityCard": "1234"
}
```

### 3. Add Customer
Endpoint: POST /api/customer/addCustomer

Description: Add a new customer to the bank.

Request:

HTTP Method: POST
Request Body:
```json
{
  "name": "string",
  "identityCard": "string"
}
```

Response:

Status Code: 200 OK
Response Body:
```json
{
  "id": 8,
  "name": "Jack",
  "identityCard": "589042"
}
```

## TransferController
### 1. Get Transfers by Account Number
Endpoint: GET /api/transfer/getTransfers/{accountNumber}

Description: Retrieve transfer history for a specific account using its account number.

Request:

HTTP Method: GET
Parameters: accountNumber (string) - The account number for which you want to retrieve transfer history.
Response:

Status Code: 200 OK
Response Body:
```json
[
  {
    "id": 1,
    "amount": 50,
    "accountIdFrom": 1,
    "accountIdTo": 2,
    "createdAt": "2023-09-21T18:08:40.7109364"
  },
  {
    "id": 2,
    "amount": 1,
    "accountIdFrom": 1,
    "accountIdTo": 2,
    "createdAt": "2023-09-22T17:49:41.0378303"
  }
]
```
### 2. Create Transfer
Endpoint: POST /api/transfer/createTransfer

Description: Create a new transfer between two accounts.

Request:

HTTP Method: POST
Request Body:
```json
{
  "amount": 1,
  "accountIdFrom": 1,
  "accountIdTo": 5
}
```
Response:

Status Code: 200 OK
Response Body:
```json
{
  "id": 3,
  "amount": 1,
  "accountIdFrom": 1,
  "accountIdTo": 2,
  "createdAt": "2023-09-23T11:11:32.158298-03:00"
}
```

## Technologies Used
ASP.NET 6, Entity Framework 6.0.0 and MSTest.

## How to Run
- Clone the project
- Update the connection string to point to your database server
- Run the following command to apply Entity Framework migrations and create the database: `dotnet ef database update`
- Build and run the application