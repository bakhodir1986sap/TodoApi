## TodoList API

TodoList API is a simple RESTful API that allows users to manage and interact with a todo list. It provides the ability to create, read, update and delete todo items. The API is built on ASP.NET Core, Entity Framework Core and MySQL.

Each todo item has two properties: Title and Description.

### Prerequisites

- [.NET Core 3.1 SDK](https://dotnet.microsoft.com/download)
- [MySQL Server](https://dev.mysql.com/downloads/installer/)

### Setup Database

Before using the API, set up a MySQL database and update the connection string in the `Startup.cs` file.

Example connection string:  
`Server=localhost;Database=Tododb;User=root;Password=my-secret-pw;`

### Running the Application

1. Navigate to the root directory of the project where the ".csproj" file resides.
2. Run the following command in your terminal to restore the required packages: `dotnet restore`
3. Run the following command in your terminal to build the project: `dotnet build`
4. To run the project, execute the following command: `dotnet run`
5. Open a web browser and navigate to `http://localhost:5000/api/TodoItems` to interact with the API.

### API Endpoints

- `GET /api/TodoItems`: Get all todo items.
- `GET /api/TodoItems/{id}`: Get a single todo item by its id.
- `POST /api/TodoItems`: Create a new todo item. Send a JSON body with `Title` and `Description`.
- `PUT /api/TodoItems/{id}`: Update an existing todo item identified by its id. Send a JSON body with updated `Title` or `Description`.
- `DELETE /api/TodoItems/{id}`: Delete an existing todo item by its id.

It is recommended to use an API Client like Postman or Insomnia to interact with the API Endpoints.

### Note
This API does not include provisions for authentication and authorization for simplicity. You should consider introducing these in a production-level API.
