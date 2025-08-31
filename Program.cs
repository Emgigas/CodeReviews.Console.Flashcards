using Flashcards.Emgigas;
using Flashcards.Emgigas.Views;

var dataAccess = new DataAccess();

dataAccess.CreateTables();
SeedData.SeedRecords();

UserInterface.MainMenu();