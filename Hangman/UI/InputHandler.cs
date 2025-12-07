using Hangman.Models;

namespace Hangman.UI;

public class InputHandler
{
    public string AskForDifficulty()
    {
        var options = Enum.GetValues(typeof(Difficulty)).Cast<Difficulty>().ToDictionary(d => (int)d, d => d.ToString());
        while (true)
        {
            char key = Console.ReadKey(intercept: true).KeyChar;
            
            if (char.IsDigit(key))
            {
                int value = key - '0';
                
                if(options.ContainsKey(value)){
                    return options[value];
                }
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
    
    public string AskForName()
    {
        while (true)
        {
            string? input = Console.ReadLine();
            
            if (!string.IsNullOrWhiteSpace(input))
            {
                return input.Trim();
            }
        }
    }
    
    public bool AskPlayAgain()
    {
        while (true)
        {
            char key = Console.ReadKey(intercept: true).KeyChar;
        
            if (char.ToUpper(key) == 'Y') return true;
            if (char.ToUpper(key) == 'N') return false;
        }
    }
}
