using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;

namespace Flashcards.Emgigas.Models;

public class DataAccess
{
    // Get connection string for database
    IConfiguration configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build();

    private string connectionString;

    public DataAccess()
    {
        connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    // Create tables in Database 'Flashcards'
    internal void CreateTables()
    {
        try
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // create table for Stacks
                string createStackTable =
                    @"IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Stacks')
                    CREATE TABLE Stacks (
                        Id int IDENTITY(1,1) NOT NULL,
                        Name NVARCHAR(30) NOT NULL UNIQUE,
                        PRIMARY KEY (Id)
                    );";
                connection.Execute(createStackTable);
                // create table for Flashcards
                string createFlashcardTable =
                    @"IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Flashcards')
                    CREATE TABLE Flashcards (
                        Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
                        Question NVARCHAR(30) NOT NULL,
                        Answer NVARCHAR(30) NOT NULL,
                        StackId int NOT NULL
                            FOREIGN KEY
                            REFERENCES Stacks(Id)
                            ON DELETE CASCADE
                            ON UPDATE CASCADE
                    );";
                connection.Execute(createFlashcardTable);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"There was an error creating the tables: {ex.Message}");
        }
    }
}