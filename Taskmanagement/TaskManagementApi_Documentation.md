
# User API Endpoints Documentation

This documentation provides instructions on how to use the endpoints provided by the User API implemented in the `UserRepository` class. The User API allows you to create, read, update, and delete user information.

## Prerequisites

Before using the User API, ensure that you have:

1. Set up your development environment with the necessary dependencies, including the .NET Core SDK and a database.

2. Integrated the `UserRepository` class into your ASP.NET Core application.

## Create a User

**Endpoint:** `/api/users/create`

**HTTP Method:** `POST`

**Request Body:**
```json
{
  "Name": "John Doe",
  "Email": "john.doe@example.com"
}

Description: Create a new user with the specified name and email.

Response:

200 OK if the user is created successfully.
400 Bad Request if the request body is invalid.
409 Conflict if a user with the same email already exists.

## Read a User
Endpoint: /api/users/{userId}

HTTP Method: GET

Description: Retrieve user information by providing the user's unique identifier (userId).

Response:

200 OK with the user's details in the response body if the user exists.
404 Not Found if the user does not exist.

## Read All Users
Endpoint: /api/users

HTTP Method: GET

Description: Retrieve a list of all users.

Response:

200 OK with a list of user details in the response body.
Update User Details

## Endpoint: /api/users/{userId}
HTTP Method: PUT

Request Body:

json
Copy code
{
  "Name": "Updated Name",
  "Email": "updated.email@example.com"
}
Description: Update the details of an existing user identified by userId.

Response:

200 OK if the user details are updated successfully.
400 Bad Request if the request body is invalid.
404 Not Found if the user does not exist.
409 Conflict if the email provided is already associated with another user.
Delete User

## Endpoint: /api/users/{userId}
HTTP Method: DELETE

Description: Delete a user by providing their unique identifier (userId).

Response:

204 No Content if the user is deleted successfully.
404 Not Found if the user does not exist.
Error Handling
The API returns appropriate HTTP status codes and error messages for different types of errors, including validation errors, not found errors, and conflicts.

Examples
Here are some example API requests and responses:

Create a User Request
http
Copy code
POST /api/users/create
Content-Type: application/json

{
  "Name": "John Doe",
  "Email": "john.doe@example.com"
}
Create a User Response
http
Copy code
HTTP/1.1 200 OK
Content-Type: application/json

{
  "UserId": "12345",
  "Name": "John Doe",
  "Email": "john.doe@example.com"
}

## Read All Users Request
http
Copy code
GET /api/users
Read All Users Response
http
Copy code
HTTP/1.1 200 OK
Content-Type: application/json

[
  {
    "UserId": "12345",
    "Name": "John Doe",
    "Email": "john.doe@example.com"
  },
  {
    "UserId": "67890",
    "Name": "Jane Smith",
    "Email": "jane.smith@example.com"
  }
]
Feel free to expand on this documentation and include more details, such as authentication requirements or additional error scenarios. Make sure to adapt the documentation to your specific project's structure and requirements.