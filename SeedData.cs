using Flashcards.Emgigas.Models;

namespace Flashcards.Emgigas;

internal static class SeedData
{
    internal static void SeedRecords()
    {
        List<Stack> stacks = new()
        {
            new Stack { Name = "French" },
            new Stack { Name = "Spanish" },
            new Stack { Name = "German" },
            new Stack { Name = "Italian" },
            new Stack { Name = "Polish" }
        };

        List<Flashcard> flashcards = new()
        {
            new Flashcard { StackId = 1, Question = "Salut", Answer = "Hi" },
            new Flashcard { StackId = 2, Question = "Hola", Answer = "Hi" },
            new Flashcard { StackId = 3, Question = "Hallo", Answer = "Hi" },
            new Flashcard { StackId = 4, Question = "Ciao", Answer = "Hi" },
            new Flashcard { StackId = 5, Question = "Cześć", Answer = "Hi" },
            new Flashcard { StackId = 1, Question = "Bonjour", Answer = "Good morning" },
            new Flashcard { StackId = 2, Question = "Buenos Dias", Answer = "Good morning" },
            new Flashcard { StackId = 3, Question = "Guten morgen", Answer = "Good morning" },
            new Flashcard { StackId = 4, Question = "Buongiorno", Answer = "Good morning" },
            new Flashcard { StackId = 5, Question = "Dzień dobry", Answer = "Good morning" },
            new Flashcard { StackId = 1, Question = "Merci", Answer = "Thank you" },
            new Flashcard { StackId = 2, Question = "Gracias", Answer = "Thank you" },
            new Flashcard { StackId = 3, Question = "Danke", Answer = "Thank you" },
            new Flashcard { StackId = 4, Question = "Grazie", Answer = "Thank you" },
            new Flashcard { StackId = 5, Question = "Dziękuję", Answer = "Thank you" },
        };
        
        var dataAccess = new DataAccess();
        dataAccess.InsertSeedData(stacks, flashcards);
}