namespace Hangman;

public class Display
{
    public void ShowDifficultyMenu()
    {
        string art = Art.DrawScene(0);
        
        Rendering.RenderLayout("CHOOSE A DIFFICULTY!", art, () =>
        {
            var options = Enum.GetValues<Difficulty>();
            
            foreach (var option in options)
            {
                Rendering.PrintCentered($"{(int)option + 1}. {option}");
            }
        });
    }

    public void ShowGame(GameLogic logic, GuessResult? result)
    {
        string art = Art.DrawScene(logic.CurrentMistakes);
        
        Rendering.RenderLayout("", art, () =>
        {
            Rendering.PrintCentered("Guess a letter\n");
            Rendering.PrintCentered(GetMaskedWord(logic.Word, logic.GuessedLetters), ConsoleColor.Cyan);
            Console.WriteLine();
            Rendering.PrintCentered($"Mistakes: {logic.CurrentMistakes}/{logic.MaxMistakes}");
            
            if (result.HasValue) PrintStatus(result.Value);
        });
    }

    public void ShowVictory()
    {
        int frame = 0;
        int sleepTime = 1000;
        while (!Console.KeyAvailable)
        {
            string art = Art.GetVictoryFrame(frame);
            
            Rendering.RenderLayout("", art, () =>
            {
                Rendering.PrintCentered("CONGRATULATIONS!", ConsoleColor.Green);
                Console.WriteLine();
                Rendering.PrintCentered("THE DINOSAURS HAVE BEEN SAVED!");
                Console.WriteLine();
                Rendering.PrintCentered("Press any key to exit...", ConsoleColor.DarkGray);
            });
            
            Thread.Sleep(sleepTime);
            frame++;
        }
        Console.ReadKey(true);
    }

    public void ShowDefeat(string secretWord)
    {
        string art = Art.GetDefeatScene();
        
        Rendering.RenderLayout("", art, () =>
        {
            Rendering.PrintCentered("THE DINOSAURS ARE TOASTS NOW...", ConsoleColor.Red);
            Console.WriteLine();
            Rendering.PrintCentered($"The word was: {secretWord}");
            Console.WriteLine();
            Rendering.PrintCentered("Press any key to exit...", ConsoleColor.DarkGray);
        });
        Console.ReadKey(true);
    }
    
    public string GetMaskedWord(string word, IReadOnlySet<char> guessedLetters)
    {
        return string.Join(" ", word.Select(c => guessedLetters.Contains(c) ? c : '_'));
    }

    private void PrintStatus(GuessResult result)
    {
        Console.WriteLine();
        var (msg, color) = result switch
        {
            GuessResult.Success => ("Good Job!", ConsoleColor.Green),
            GuessResult.Miss => ("Missed!", ConsoleColor.Red),
            GuessResult.Duplicate => ("Already used.", ConsoleColor.Yellow),
            _ => ("", ConsoleColor.White)
        };
        Rendering.PrintCentered(msg, color);
    }
}