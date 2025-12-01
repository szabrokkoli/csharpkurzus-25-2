namespace Hangman.UI;

public static class Rendering
{
    public static void RenderLayout(string title2, string art, Action bodyContent)
    {
        string title1 = "HANGMAN: Save The Dinos";
        int width = Math.Max(Console.WindowWidth - 1, 20);
        int artHeight = 12;
        Console.Clear();
        
        PrintDivider(title1, width);
        
        if (!string.IsNullOrEmpty(art))
        {
            PrintArtBlock(art, width);
        }
        else
        {
            for(int i=0; i<artHeight; i++) Console.WriteLine(); 
        }
        
        PrintDivider(title2, width);
        
        bodyContent(); 
    }
    
    private static void PrintArtBlock(string art, int width)
    {
        int maxArtSize = 60;
        var lines = art.Split('\n');
        
        int paddingSize = Math.Max(0, (width - maxArtSize) / 2);
        string padding = new string(' ', paddingSize);

        foreach (var line in lines)
        {
            Console.WriteLine($"{padding}{line.TrimEnd()}");
        }
    }

    private static void PrintDivider(string text, int width)
    {
        int dashCount = Math.Max(0, (width - (text?.Length ?? 0) - 2) / 2);
        string dashes = new string('=', dashCount);
        
        if (string.IsNullOrEmpty(text))
        {
            Console.WriteLine(new string('=', width) + "\n");
        }
        else
        {
            Console.WriteLine($"{dashes} {text} {dashes}\n");
        }
    }

    public static void PrintCentered(string text, ConsoleColor color = ConsoleColor.White)
    {
        if (string.IsNullOrEmpty(text)) return;
        
        int width = Math.Max(Console.WindowWidth - 1, 20);
        int pad = Math.Max(0, (width - text.Length) / 2);
        
        Console.ForegroundColor = color;
        Console.WriteLine(new string(' ', pad) + text);
        Console.ResetColor();
    }
}