namespace Hangman;

public class InputHandler
{
    public Difficulty GetDifficulty()
    {
        var options = Enum.GetValues<Difficulty>();
        while (true)
        {
            char key = Console.ReadKey(intercept: true).KeyChar;
            if (int.TryParse(key.ToString(), out int val) && val > 0 && val <= options.Length)
            {
                return (Difficulty)(val - 1);
            }
        }
    }

    public char AskForLetter()
    {
        while (true)
        {
            char key = Console.ReadKey(intercept: true).KeyChar;
            if (char.IsLetter(key)) 
            {
                return key;
            }
        }
    }
}