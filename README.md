# TaskManagementApi

## Prerequisites
Before setting up and running the API, 
ensure you have the following prerequisites installed on your system:

1. .NET Core SDK (version 3.1 or higher)
2. Microsoft SQL Server (or any other database of your choice)
3. Visual Studio or Visual Studio Code 
4. SQL Server Management Studio

## Getting Started
Clone the Repository
First, clone this repository to your local machine:

bash
Copy code
git clone https://github.com/martinmatics100/TaskManagementApi.git 

## Database Setup

1. Launch your SQL Server Management Studio
2. Connect to your server
3. Copy your server name and update the connection string with your server name as it shows below

Update the database connection string in appsettings.json:

json
Copy code
{
  "ConnectionStrings": {
    "conn": "server=MARTINMATICS;database=TaskManagementDb;Trust Server Certificate=true; MultipleActiveResultSets=true;integrated security=true;"
  },
  // ...
}

## Build Solution and Run the api
Building solution will update all dependencies

## API Endpoints
The API provides the following endpoints:

1. GET /tasks: Get a list of all tasks.
2. GET /tasks/{id}: Get details of a specific task by ID.
3. POST /tasks: Create a new task.
4. PUT /tasks/{id}: Update an existing task by ID.
5. DELETE /tasks/{id}: Delete a task by ID.
6. GET /projects: Get a list of all projects.
7. GET /projects/{id}: Get details of a specific project by ID.
8. POST /projects: Create a new project.
9. PUT /projects/{id}: Update an existing project by ID.
10. DELETE /projects/{id}: Delete a project by ID.
11. GET /users: Get a list of all users.
12. GET /users/{id}: Get details of a specific user by ID.
13. POST /users: Create a new user.
14. PUT /users/{id}: Update an existing user by ID.
15. DELETE /users/{id}: Delete a user by ID.
16. GET /notifications: Get a list of all notifications.
17. GET /notifications/{id}: Get details of a specific notification by ID.
18. POST /notifications: Create a new notification.
19. PUT /notifications/{id}: Update an existing notification by ID.
20. DELETE /notifications/{id}: Delete a notification by ID.
21. GET /tasks/status/{status}: Get tasks by status (e.g., "pending", "in-progress").
22. GET /tasks/priority/{priority}: Get tasks by priority (e.g., "low", "medium", "high").
23. GET /tasks/due-this-week: Get tasks due for the current week.
24. POST /tasks/{taskId}/assign-project/{projectId}: Assign a task to a project.
25. DELETE /tasks/{taskId}/remove-from-project: Remove a task from a project.
26. PUT /notifications/mark-as-read/{id}: Mark a notification as read.
27. PUT /notifications/mark-as-unread/{id}: Mark a notification as unread.
For more details on how to use these endpoints, refer to the API documentation in the markdown file in the project


## Clean Architecture

The API follows the clean architecture principles, separating concerns into different layers:

Presentation or ApiLayer (Executable project): Contains the API controllers and view models.
CoreLayer: Contains the application services that handle business logic.
DataLayer: Contains the core domain entities, aggregates, and value objects.
Infrastructure: Contains data access, repositories, and external service implementations.

## Error Handling

Proper error handling is implemented for the API. The API returns appropriate HTTP status codes and error messages for different types of errors, including validation errors and not found errors.

## Documentation

For more details on how to use these endpoints, refer to the API documentation in the markdown file in the project

This README outlines the setup and basic usage of the Task Management API. Additional documentation and design decisions can be found in the project's documentation folder.
