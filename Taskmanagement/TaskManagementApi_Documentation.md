
# User API Endpoints Documentation.

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

## Delete User
Endpoint: /api/users/{userId}
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

#

# Task Management API Endpoints Documentation



## Create a Task
Endpoint: /api/tasks/create

HTTP Method: POST

Request Body:

json
Copy code
{
  "Title": "Task Title",
  "Description": "Task description goes here.",
  "Priority": "Medium",
  "Status": "Pending",
  "ProjectId": "ProjectIdHere",
  "CreatedByUserId": "UserIdHere"
}
Description: Create a new task with the specified title, description, priority, status, associated project, and the user who created it. The task's due date is automatically set to 48 hours from the current date.

Response:

200 OK if the task is created successfully.
400 Bad Request if the request body is invalid.
404 Not Found if the associated project or user does not exist.
Get Tasks by User ID

## Endpoint: /api/tasks/user/{userId}

HTTP Method: GET

Description: Retrieve a list of tasks created by a specific user identified by userId.

Response:

200 OK with a list of task details in the response body if tasks exist.
404 Not Found if no tasks are found for the user.

## Get Tasks by Project ID
Endpoint: /api/tasks/project/{projectId}

HTTP Method: GET

Description: Retrieve a list of tasks associated with a specific project identified by projectId.

Response:

200 OK with a list of task details in the response body if tasks exist.
404 Not Found if no tasks are found for the project.

## Update Task Details
Endpoint: /api/tasks/{taskId}

HTTP Method: PUT

Request Body:

json
Copy code
{
  "Title": "Updated Task Title",
  "Description": "Updated task description.",
  "Priority": "High",
  "Status": "In-Progress",
  "ProjectId": "UpdatedProjectId",
  "CreatedByUserId": "UpdatedUserId"
}
Description: Update the details of an existing task identified by taskId.

Response:

200 OK if the task details are updated successfully.
400 Bad Request if the request body is invalid.
404 Not Found if the task does not exist.
404 Not Found if the associated project or user does not exist.
409 Conflict if the email provided is already associated with another user.

## Delete Task
Endpoint: /api/tasks/{taskId}

HTTP Method: DELETE

Description: Delete a task by providing its unique identifier (taskId).

Response:

204 No Content if the task is deleted successfully.
404 Not Found if the task does not exist.

## Send Task Notifications
Endpoint: /api/tasks/send-notifications

HTTP Method: POST

Description: Send task notifications to users for tasks that are due within 48 hours and are not completed.

Response:

200 OK if notifications are sent successfully.
Error Handling
The API returns appropriate HTTP status codes and error messages for different types of errors, including validation errors, not found errors, and conflicts.

Examples
Here are some example API requests and responses:

Create a Task Request
http
Copy code
POST /api/tasks/create
Content-Type: application/json

{
  "Title": "Task Title",
  "Description": "Task description goes here.",
  "Priority": "Medium",
  "Status": "Pending",
  "ProjectId": "ProjectIdHere",
  "CreatedByUserId": "UserIdHere"
}
Create a Task Response
http
Copy code
HTTP/1.1 200 OK
Content-Type: application/json

{
  "Title": "Task Title",
  "Description": "Task description goes here.",
  "Priority": "Medium",
  "Status": "Pending",
  "ProjectId": "ProjectIdHere",
  "CreatedByUserId": "UserIdHere"
}

#

# Project Management API Endpoints Documentation

## Create a Project
Endpoint: /api/projects/create

HTTP Method: POST

Request Body:

json
Copy code
{
  "Name": "Project Name",
  "Description": "Project description goes here."
}
Description: Create a new project with the specified name and description.

Response:

200 OK if the project is created successfully.
400 Bad Request if the request body is invalid.

## Get Project by ID
Endpoint: /api/projects/{projectId}

HTTP Method: GET

Description: Retrieve project information by providing the project's unique identifier (projectId).

Response:

200 OK with project details in the response body if the project exists and is not deleted.
404 Not Found if the project does not exist or is deleted.

## Update Project Details
Endpoint: /api/projects/{projectId}

HTTP Method: PUT

Request Body:

json
Copy code
{
  "Name": "Updated Project Name",
  "Description": "Updated project description."
}
Description: Update the details of an existing project identified by projectId.

Response:

200 OK if the project details are updated successfully.
400 Bad Request if the request body is invalid.
404 Not Found if the project does not exist or is deleted.

## Delete Project
Endpoint: /api/projects/{projectId}

HTTP Method: DELETE

Description: Delete a project by providing its unique identifier (projectId).

Response:

204 No Content if the project is deleted successfully.
404 Not Found if the project does not exist or is deleted.
Error Handling
The API returns appropriate HTTP status codes and error messages for different types of errors, including validation errors and not found errors.

Examples
Here are some example API requests and responses:

Create a Project Request
http
Copy code
POST /api/projects/create
Content-Type: application/json

{
  "Name": "Project Name",
  "Description": "Project description goes here."
}
Create a Project Response
http
Copy code
HTTP/1.1 200 OK
Content-Type: application/json

{
  "Name": "Project Name",
  "Description": "Project description goes here."
}

#

# Additional Task Management API Endpoints Documentation

## Get Tasks by Status
Endpoint: /api/tasks/status/{status}

HTTP Method: GET

Description: Retrieve a list of tasks based on their status.

Response:

200 OK with a list of task details in the response body if tasks exist.
404 Not Found if no tasks are found with the specified status.

## Get Tasks by Priority
Endpoint: /api/tasks/priority/{priority}

HTTP Method: GET

Description: Retrieve a list of tasks based on their priority.

Response:

200 OK with a list of task details in the response body if tasks exist.
404 Not Found if no tasks are found with the specified priority.
Get Tasks Due for Current Week

## Endpoint: /api/tasks/week
HTTP Method: GET

Description: Retrieve a list of tasks that are due within the current week.

Response:

200 OK with a list of task details in the response body if tasks exist.
404 Not Found if no tasks are due within the current week.

## Assign Task to Project
Endpoint: /api/tasks/assign/{taskId}/{projectId}

HTTP Method: PUT

Description: Assign a task identified by taskId to a project identified by projectId.

Response:

200 OK if the task is assigned to the project successfully.
404 Not Found if the task or project does not exist.


## Remove Task from Project
Endpoint: /api/tasks/remove/{taskId}

HTTP Method: PUT

Description: Remove a task identified by taskId from its associated project.

Response:

200 OK if the task is successfully removed from the project.
404 Not Found if the task does not exist or is not associated with a project.

## Mark Notification as Read
Endpoint: /api/notifications/read/{notificationId}

HTTP Method: PUT

Description: Mark a notification identified by notificationId as read.

Response:

200 OK if the notification is marked as read successfully.
404 Not Found if the notification does not exist.

## Mark Notification as Unread
Endpoint: /api/notifications/unread/{notificationId}

HTTP Method: PUT

Description: Mark a notification identified by notificationId as unread.

Response:

200 OK if the notification is marked as unread successfully.
404 Not Found if the notification does not exist.
Error Handling
The API returns appropriate HTTP status codes and error messages for different types of errors, including validation errors and not found errors.

Examples
Here are some example API requests and responses:

Get Tasks by Status Request
http
Copy code
GET /api/tasks/status/In-Progress
Get Tasks by Status Response
http
Copy code
HTTP/1.1 200 OK
Content-Type: application/json

[
  {
    "Title": "Task Title",
    "Description": "Task description goes here.",
    "DueDate": "2023-09-19T14:30:00",
    "Priority": "Medium",
    "Status": "In-Progress",
    "ProjectId": "ProjectIdHere",
    "CreatedByUserId": "UserIdHere"
  },
  {
    "Title": "Another Task",
    "Description": "Another task description.",
    "DueDate": "2023-09-21T10:00:00",
    "Priority": "High",
    "Status": "In-Progress",
    "ProjectId": "ProjectIdHere",
    "CreatedByUserId": "UserIdHere"
  }
]

# 

# Notification Management API Endpoints Documentation

## Create a Notification
Endpoint: /api/notifications/create

HTTP Method: POST

Request Body:

json
Copy code
{
  "Type": "Notification Type",
  "Message": "Notification message goes here.",
  "Timestamp": "2023-09-19T14:30:00",
  "UserId": "UserIdHere",
  "TaskId": "TaskIdHere"
}
Description: Create a new notification with the specified type, message, timestamp, user ID, and task ID.

Response:

200 OK if the notification is created successfully.
400 Bad Request if the request body is invalid.

## Get Notification by ID
Endpoint: /api/notifications/{notificationId}

HTTP Method: GET

Description: Retrieve a notification by providing its unique identifier (notificationId).

Response:

200 OK with notification details in the response body if the notification exists.
404 Not Found if the notification does not exist.

## Get All Notifications
Endpoint: /api/notifications

HTTP Method: GET

Description: Retrieve a list of all notifications.

Response:

200 OK with a list of notification details in the response body if notifications exist.
404 Not Found if no notifications are found.

## Update Notification
Endpoint: /api/notifications/{notificationId}

HTTP Method: PUT

Request Body:

json
Copy code
{
  "Type": "Updated Notification Type",
  "Message": "Updated notification message.",
  "Timestamp": "2023-09-20T10:00:00"
}
Description: Update the details of an existing notification identified by notificationId.

Response:

200 OK if the notification details are updated successfully.
400 Bad Request if the request body is invalid.
404 Not Found if the notification does not exist.

## Delete Notification
Endpoint: /api/notifications/{notificationId}

HTTP Method: DELETE

Description: Delete a notification by providing its unique identifier (notificationId).

Response:

204 No Content if the notification is deleted successfully.
404 Not Found if the notification does not exist.
Error Handling
The API returns appropriate HTTP status codes and error messages for different types of errors, including validation errors and not found errors.

Examples
Here are some example API requests and responses:

Create a Notification Request
http
Copy code
POST /api/notifications/create
Content-Type: application/json

{
  "Type": "Due Date Reminder",
  "Message": "Task 'Task Title' is due soon.",
  "Timestamp": "2023-09-19T14:30:00",
  "UserId": "UserIdHere",
  "TaskId": "TaskIdHere"
}
Create a Notification Response
http
Copy code
HTTP/1.1 200 OK
Content-Type: application/json

{
  "Type": "Due Date Reminder",
  "Message": "Task 'Task Title' is due soon.",
  "Timestamp": "2023-09-19T14:30:00",
  "UserId": "UserIdHere",
  "TaskId": "TaskIdHere"
}