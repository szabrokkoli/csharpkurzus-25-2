namespace Hangman;

public static class DisplayHelper
{
    private static int Width => Console.WindowWidth - 1;

    public static void PrintLine(string text, ConsoleColor color = ConsoleColor.White)
    {
        if (string.IsNullOrEmpty(text)) return;
        Console.ForegroundColor = color;
        int padding = Math.Max(0, (Width - text.Length) / 2);
        Console.WriteLine(text.PadLeft(padding + text.Length));
        Console.ResetColor();
    }

    public static void PrintDivider(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            Console.WriteLine(new string('=', Width));
            return;
        }
        int dashCount = Math.Max(0, (Width - (text.Length + 2)) / 2);
        string dashes = new string('=', dashCount);
        Console.WriteLine($"{dashes} {text} {dashes}");
    }

    public static void VerticalSpace(int lines)
    {
        if (lines > 0) Console.Write(new string('\n', lines));
    }

    public static void Reset()
    {
        try { Console.Clear(); } catch { }
        Console.SetCursorPosition(0, 0);
    }

    public static void PrintMultilineCentered(string art, int fixedHeight = 0)
    {
        var lines = string.IsNullOrEmpty(art) ? Array.Empty<string>() : art.Split('\n');
        
        if (fixedHeight > 0)
        {
            int missingLines = fixedHeight - lines.Length;
            if (missingLines > 0) VerticalSpace(missingLines);
        }

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) Console.WriteLine();
            else PrintLine(line);
        }
    }
}