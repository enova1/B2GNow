
# MVC Project with .NET 8, C#, SQLite, and Entity Frameworks

This is a simple MVC (Model-View-Controller) project built using .NET 8, C#, SQLite, and Entity Frameworks. This README file will guide you through setting up and running the project on your local machine.

## Prerequisites

Before you begin, ensure you have the following installed on your machine:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/5.0)
- [Visual Studio](https://visualstudio.microsoft.com/) or any other C# IDE of your choice
- SQLite (The project uses SQLite as the database)

## Getting Started

Follow these steps to get the project up and running on your local machine:

1. **Clone the Repository**: 
   ```
   git clone git@github.com:enova1/MVC.git
   ```

2. **Open Solution in Visual Studio**:
   - Open Visual Studio.
   - Navigate to File > Open > Project/Solution.
   - Select the `.sln` file from the cloned repository.

3. **Build the Solution**:
   - Once the solution is loaded, build the solution to restore dependencies:
     ```
     Ctrl + Shift + B
     ```

4. **Database Setup**:
   - The project uses SQLite as the database, and the database file (`Database.db`) will be automatically created when you run the application for the first time.

5. **Run the Application**:
   - Set the startup project to the MVC project.
   - Press `F5` or click on the Start Debugging button to run the application.

6. **Explore the Application**:
   - Once the application is running, you can explore the features and functionalities provided by the MVC project.

## Additional Notes

- **Entity Frameworks**: The project utilizes Entity Frameworks for data access and management. You can find the data context and migrations in the project.
- **Configuration**: You can update the database connection string and other configurations in the `appsettings.json` file.
- **Contributing**: If you find any issues or would like to contribute to the project, feel free to create a pull request or open an issue on GitHub.

## Support

For any questions or issues, please feel free to reach out to [lazer8701@gmail.com](mailto:lazer8701@gmail.com).

