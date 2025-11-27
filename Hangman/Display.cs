namespace Hangman;

public class Display // todo: clean it up
{
    public void ShowDifficultyMenu()
    {
        RenderLayout("DIFFICULTY", "", () =>
        {
            PrintCentered("1. ROOKIE");
            PrintCentered("2. NERD");
            PrintCentered("3. PALEONTOLOGIST");
            Console.WriteLine();
            PrintCentered("Select your destiny...", ConsoleColor.DarkGray);
        });
    }

    public void ShowGame(GameLogic logic, GuessResult? result)
    {
        string art = Art.DrawScene(logic.CurrentMistakes);
        
        RenderLayout("HANGMAN: Save The Dinos", art, () =>
        {
            PrintCentered("Guess a letter");
            Console.WriteLine();
            PrintCentered(logic.GetMaskedWord(), ConsoleColor.Cyan);
            Console.WriteLine();
            PrintCentered($"Mistakes: {logic.CurrentMistakes}/{logic.MaxMistakes}");
            
            if (result.HasValue) PrintStatus(result.Value);
        });
    }

    public void ShowVictoryAnimation()
    {
        int frame = 0;
        while (!Console.KeyAvailable)
        {
            string art = Art.GetVictoryFrame(frame);
            
            RenderLayout("VICTORY!", art, () =>
            {
                PrintCentered("CONGRATULATIONS!", ConsoleColor.Green);
                PrintCentered("THE DINOSAURS HAVE BEEN SAVED!");
                Console.WriteLine("\n");
                PrintCentered("Press any key to exit...", ConsoleColor.DarkGray);
            });
            
            Thread.Sleep(500);
            frame++;
        }
        Console.ReadKey(true);
    }

    public void ShowDefeat(string secretWord)
    {
        string art = Art.GetDefeatScene();
        
        RenderLayout("EXTINCTION", art, () =>
        {
            PrintCentered("THE DINOSAURS ARE TOASTS NOW...", ConsoleColor.Red);
            PrintCentered($"The word was: {secretWord}");
            Console.WriteLine("\n");
            PrintCentered("Press any key to exit...", ConsoleColor.DarkGray);
        });
        Console.ReadKey(true);
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
        PrintCentered(msg, color);
    }

    private void RenderLayout(string title, string art, Action bodyContent)
    {
        Console.Clear();
        PrintDivider(title);
        
        if (!string.IsNullOrEmpty(art))
        {
            PrintArtBlock(art);
        }
        else
        {
            for(int i=0; i<12; i++) Console.WriteLine(); 
        }
        
        PrintDivider(""); 
        Console.WriteLine();
        
        bodyContent(); 
    }
    
    private void PrintArtBlock(string art)
    {
        var lines = art.Split('\n');
        
        int windowWidth = Math.Max(Console.WindowWidth, 60);
        int padLeft = Math.Max(0, (windowWidth - 60) / 2);
        string padding = new string(' ', padLeft);

        foreach (var line in lines)
        {
            string cleanLine = line.Replace("\r", "").TrimEnd(); 
            
            if (!string.IsNullOrWhiteSpace(cleanLine))
            {
                Console.WriteLine(padding + cleanLine);
            }
            else
            {
                Console.WriteLine();
            }
        }
    }

    private void PrintDivider(string text)
    {
        int width = Math.Max(Console.WindowWidth - 1, 20);
        int dashCount = Math.Max(0, (width - (text?.Length ?? 0) - 2) / 2);
        string dashes = new string('=', dashCount);
        
        if (string.IsNullOrEmpty(text))
        {
            Console.WriteLine(new string('=', width));
        }
        else
        {
            Console.WriteLine($"{dashes} {text} {dashes}");
        }
    }

    private void PrintCentered(string text, ConsoleColor color = ConsoleColor.White)
    {
        if (string.IsNullOrEmpty(text)) return;
        
        int width = Math.Max(Console.WindowWidth - 1, 20);
        int pad = Math.Max(0, (width - text.Length) / 2);
        
        Console.ForegroundColor = color;
        Console.WriteLine(new string(' ', pad) + text);
        Console.ResetColor();
    }
}