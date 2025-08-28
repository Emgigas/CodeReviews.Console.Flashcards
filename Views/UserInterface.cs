using Spectre.Console;
using static Flashcards.Emgigas.Controllers.Enums;

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
                    isProgramRunning = false; // Using MainMenu here causes the program to create more while loops whereas changing this local variable closes this while loop only.
                    break;
            }
        }
    }

    private static void ViewStacks()
    {
        throw new NotImplementedException();
    }
    
    private static void AddStack()
    {
        throw new NotImplementedException();
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
                    isProgramRunning = false; // Using MainMenu here causes the program to create more while loops whereas changing this local variable closes this while loop only.
                    break;
            }
        }
    }

    private static void ViewFlashcards()
    {
        throw new NotImplementedException();
    }

    private static void AddFlashcard()
    {
        throw new NotImplementedException();
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