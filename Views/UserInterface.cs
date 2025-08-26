using Flashcards.Emgigas.Controllers;
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
        }
    }
    // Displays stack menu using Spectre Prompts
    
    // Displays flashcard menu using Spectre Prompts
    
}