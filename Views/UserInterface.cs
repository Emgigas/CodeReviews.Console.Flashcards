using Spectre.Console;
using static Flashcards.Emgigas.Controllers.Enums;
using Flashcards.Emgigas.Models;

namespace Flashcards.Emgigas.Views;

internal class UserInterface
{
    // Displays main menu for accessing stack, flashcard and study menus using Spectre Prompts
    internal static void MainMenu()
    {
        var isProgramRunning = true;

        while (isProgramRunning)
        {
            var userInput = AnsiConsole.Prompt(
                new SelectionPrompt<MainMenuOptions>()
                    .Title("What would you like to do?")
                    .AddChoices(
                        MainMenuOptions.StacksMenu,
                        MainMenuOptions.FlashcardsMenu,
                        MainMenuOptions.StudyArea,
                        MainMenuOptions.Quit)
            );

            switch (userInput)
            {
                case MainMenuOptions.StacksMenu:
                    StacksMenu();
                    break;
                case MainMenuOptions.FlashcardsMenu:
                    FlashcardsMenu();
                    break;
                case MainMenuOptions.StudyArea:
                    StudyArea();
                    break;
                case MainMenuOptions.Quit:
                    Console.WriteLine("Goodbye!");
                    isProgramRunning = false;
                    break;
            }
        }
    }

    // Displays stack menu using Spectre Prompts
    internal static void StacksMenu()
    {
        var isProgramRunning = true;

        while (isProgramRunning)
        {
            var userInput = AnsiConsole.Prompt(
                new SelectionPrompt<StackMenuOptions>()
                    .Title("What would you like to do?")
                    .AddChoices(
                        StackMenuOptions.ViewStacks,
                        StackMenuOptions.AddStack,
                        StackMenuOptions.UpdateStack,
                        StackMenuOptions.DeleteStack,
                        StackMenuOptions.MainMenu
                    )
            );

            switch (userInput)
            {
                case StackMenuOptions.ViewStacks:
                    ViewStacks();
                    break;
                case StackMenuOptions.AddStack:
                    AddStack();
                    break;
                case StackMenuOptions.UpdateStack:
                    UpdateStack();
                    break;
                case StackMenuOptions.DeleteStack:
                    DeleteStack();
                    break;
                case StackMenuOptions.MainMenu:
                    isProgramRunning =
                        false; // Using MainMenu here causes the program to create more while loops whereas changing this local variable closes this while loop only.
                    break;
            }
        }
    }

    private static void ViewStacks()
    {
        throw new NotImplementedException();
    }

    // Gets name of stack from user and inserts into table Stacks
    private static void AddStack()
    {
        // Initialise stack object
        Stack stack = new Stack();

        // Request stack name
        stack.Name = AnsiConsole.Ask<String>("What should the stack be called?");
        // Validate stack name
        while (string.IsNullOrEmpty(stack.Name))
        {
            stack.Name = AnsiConsole.Ask<String>("Stack's name cannot be blank. Please provide a stack name.");
        }

        // Insert Stack into DB
        var dataAccess = new DataAccess();
        dataAccess.InsertStack(stack);
    }

    // Allows user to choose a stack from table of stacks. Id then used to create flashcards.
    private static int ChooseStack()
    {
        // Access database and get all stacks
        var dataAccess = new DataAccess();
        var stacks = dataAccess.GetStacks();
        
        // Convert stack names from Get Stacks into an Array only capturing name
        var stackArray = stacks.Select(s => s.Name).ToArray();
        // Prompt user to pick a stack by Name
        var stackOptions = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choose a stack.")
                .AddChoices(stackArray));
        // Return Id relating to selected stack name
        var stackId = stacks.Single(s => s.Name == stackOptions).Id; // This works by returning the IEnumerable stack where Name == User choice and then returns the related Id
        return stackId;

    }

    private static void UpdateStack()
    {
        throw new NotImplementedException();
    }

    private static void DeleteStack()
    {
        throw new NotImplementedException();
    }

    internal static void FlashcardsMenu()
    {
        var isProgramRunning = true;

        while (isProgramRunning)
        {
            var userInput = AnsiConsole.Prompt(
                new SelectionPrompt<FlashcardsMenuOptions>()
                    .Title("What would you like to do?")
                    .AddChoices(
                        FlashcardsMenuOptions.ViewFlashcards,
                        FlashcardsMenuOptions.AddFlashcard,
                        FlashcardsMenuOptions.UpdateFlashcard,
                        FlashcardsMenuOptions.DeleteFlashcard,
                        FlashcardsMenuOptions.MainMenu
                    )
            );

            switch (userInput)
            {
                case FlashcardsMenuOptions.ViewFlashcards:
                    ViewFlashcards();
                    break;
                case FlashcardsMenuOptions.AddFlashcard:
                    AddFlashcard();
                    break;
                case FlashcardsMenuOptions.UpdateFlashcard:
                    UpdateFlashcard();
                    break;
                case FlashcardsMenuOptions.DeleteFlashcard:
                    DeleteFlashcard();
                    break;
                case FlashcardsMenuOptions.MainMenu:
                    isProgramRunning =
                        false; // Using MainMenu here causes the program to create more while loops whereas changing this local variable closes this while loop only.
                    break;
            }
        }
    }

    private static void ViewFlashcards()
    {
        throw new NotImplementedException();
    }

    // Gets StackId, Question and Answer from the user and inserts into Flashcards table
    private static void AddFlashcard()
    {
        // Initialise Flashcard object
        Flashcard flashcard = new Flashcard();
        
        // Get user to provide StackId by selecting name from a prompt
        flashcard.StackId = ChooseStack();
        
        // Ask user for question for this flashcard and validate
        flashcard.Question = AnsiConsole.Ask<string>("What is the question?");

        while (string.IsNullOrEmpty(flashcard.Question))
        {
            flashcard.Question = AnsiConsole.Ask<string>("Question cannot be blank. What is the question?");
        }
        
        // Ask use for answer to the question for this flashcard and validate
        flashcard.Answer = AnsiConsole.Ask<string>("What is the answer?");

        while (string.IsNullOrEmpty(flashcard.Answer))
        {
            flashcard.Answer = AnsiConsole.Ask<string>("Answer cannot be blank. What is the answer?");
        }
        
        // Access database methods
        var dataAccess = new DataAccess();
        dataAccess.InsertFlashcard(flashcard);
    }

    private static void UpdateFlashcard()
    {
        throw new NotImplementedException();
    }

    private static void DeleteFlashcard()
    {
        throw new NotImplementedException();
    }

    internal static void StudyArea()
    {
        throw new NotImplementedException();
    }
    // Displays flashcard menu using Spectre Prompts
}