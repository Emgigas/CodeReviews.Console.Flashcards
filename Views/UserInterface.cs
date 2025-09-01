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
    private static int ChooseStack(string message)
    {
        // Access database and get all stacks
        var dataAccess = new DataAccess();
        var stacks = dataAccess.GetStacks();
        
        // Convert stack names from Get Stacks into an Array only capturing name
        var stackArray = stacks.Select(s => s.Name).ToArray();
        // Prompt user to pick a stack by Name
        var stackOptions = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title(message)
                .AddChoices(stackArray));
        // Return Id relating to selected stack name
        var stackId = stacks.Single(s => s.Name == stackOptions).Id; // This works by returning the IEnumerable stack where Name == User choice and then returns the related Id
        return stackId;

    }

    private static void UpdateStack()
    {
        // Create new Stack object
        var stack = new Stack();
        
        // Get stack Id to update
        stack.Id = ChooseStack("Choose a stack to update: ");
        
        // Request new stack name, validate it is not blank
        stack.Name =  AnsiConsole.Ask<String>("What should the stack be called?");

        while (string.IsNullOrEmpty(stack.Name))
        {
           stack.Name = AnsiConsole.Ask<String>("Stack name cannot be empty. What should the stack be called?");
        }
        
        // Pass to database
        var dataAccess = new DataAccess();
        dataAccess.UpdateStack(stack);
    }

    private static void DeleteStack()
    {
        var id = ChooseStack("Choose a stack to delete");

        if (!AnsiConsole.Confirm("Are you sure you want to delete this stack?"))
            return;
        
        var dataAccess = new DataAccess();
        dataAccess.DeleteStack(id);
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
        flashcard.StackId = ChooseStack("Choose a stack to add a flashcard to.");
        
        // Ask user for question for this flashcard and validate
        flashcard.Question = GetQuestion("What is the question?");
        
        // Ask use for answer to the question for this flashcard and validate
        flashcard.Answer = GetAnswer("What is the answer?");
        
        // Access database methods
        var dataAccess = new DataAccess();
        dataAccess.InsertFlashcard(flashcard);
    }

    private static int ChooseFlashcard(string message, int stackId)
    {
        var dataAccess = new DataAccess();
        var flashcards = dataAccess.GetFlashcards(stackId);
        
        var flashcardsArray = flashcards.Select(f => f.Question).ToArray();
        var option = AnsiConsole.Prompt(new  SelectionPrompt<string>()
            .Title(message)
            .AddChoices(flashcardsArray));
        
        var flashcardId = flashcards.Single(f => f.Question == option).Id;
        
        return flashcardId;
    }

    private static void UpdateFlashcard()
    {
        var dataAccess = new DataAccess();
        Flashcard flashcard = new Flashcard();
        
        var stackId = ChooseStack("Choose a stack where the flashcard you wish to update is:");
        flashcard.Id = ChooseFlashcard("Choose the flashcard you wish to update:", stackId);
        
        flashcard.Question = GetQuestion($"The current question is: {dataAccess.GetCurrentQuestion(stackId, flashcard.Id)}. What is the question?");
        
        flashcard.Answer = GetAnswer($"The current answer is: {dataAccess.GetCurrentAnswer(stackId, flashcard.Id)}. What is the answer?");
        
        flashcard.StackId = ChooseStack("Choose the stack where the flashcard should be:");
        
        dataAccess.UpdateFlashcard(flashcard);

    }

    private static void DeleteFlashcard()
    {
        var stackId = ChooseStack("Which stack contains the Flashcard you wish to delete?");
        var flashcard = ChooseFlashcard("Choose a flashcard to delete", stackId);

        if (!AnsiConsole.Confirm("Are you sure you want to delete this flashcard?"))
            return;
        
        var dataAccess = new DataAccess();
        dataAccess.DeleteFlashcard(flashcard);
    }

    // Asks user for Flashcard question
    private static string GetQuestion(string message)
    {
        var question = AnsiConsole.Ask<string>(message);
        
        while (string.IsNullOrEmpty(question))
            {
            question = AnsiConsole.Ask<string>($"Question cannot be empty. {message}");
            }
        return question;
    }
    
    // Asks user for flashcard answer
    private static string GetAnswer(string message)
    {
        var answer = AnsiConsole.Ask<string>(message);
        while (string.IsNullOrEmpty(answer))
        {
            answer = AnsiConsole.Ask<string>($"Answer cannot be empty. {message}");
        }
        
        return answer;
    }

    internal static void StudyArea()
    {
        throw new NotImplementedException();
    }
    // Displays flashcard menu using Spectre Prompts
}