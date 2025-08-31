using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;
using Flashcards.Emgigas.Models;

namespace Flashcards.Emgigas;

public class DataAccess
{
    // Get connection string for database
    IConfiguration configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build();

    private string? connectionString;

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

    // Method to insert new Stack into SQL DB
    internal void InsertStack(Stack stack)
    {
        try
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string insertQuery = @"
            INSERT INTO Stacks (Name) VALUES (@Name)";

                connection.Execute(insertQuery, new { stack.Name });
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"There was a problem inserting the stack: {ex.Message}");
        }
    }

    // Method to pull back all Stack Names & StackID for use when creating Flashcards. IEnumerable type is used for LINQ later on

    internal IEnumerable<Stack> GetStacks()
    {
        try
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = @"SELECT Id, Name FROM Stacks;";

                return connection.Query<Stack>(selectQuery);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"There was a problem retrieving the stacks: {ex.Message}");
            return Enumerable.Empty<Stack>();
        }
    }

    // Method to create Flashcard and assign to Stack using GetStack method

    internal void InsertFlashcard(Flashcard flashcard)
    {
        try
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string insertQuery = @"
                INSERT INTO Flashcards (Question, Answer, StackId) VALUES (@Question, @Answer, @StackId)";

                connection.Execute(insertQuery, new { flashcard.Question, flashcard.Answer, flashcard.StackId });
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"There was a problem inserting the flashcard: {ex.Message}");
        }
    }

    internal void InsertSeedData(List<Stack> stacks, List<Flashcard> flashcards)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    connection.Execute("INSERT INTO Stacks (Name) VALUES (@Name)", stacks);
                    connection.Execute("INSERT INTO Flashcards (Question, Answer, StackId) VALUES (@Question, @Answer, @StackId)", flashcards);
                    
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); // Rolls back transaction in the event something goes wrong
                    Console.WriteLine($"There was a problem inserting the seed data: {ex.Message}");
                    throw;
                }
            }
        }
    }
}