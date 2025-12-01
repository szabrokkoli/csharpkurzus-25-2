namespace Hangman;

public class InputHandler
{
    public Difficulty AskForDifficulty()
    {
        var options = Enum.GetValues<Difficulty>();
        while (true)
        {
            char key = Console.ReadKey(intercept: true).KeyChar;
            
            if (char.IsDigit(key))
            {
                int value = key - '0';
                
                if(value > 0 && value <= options.Length){
                    return (Difficulty)(value - 1);
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
