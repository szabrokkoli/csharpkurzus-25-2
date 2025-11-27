namespace Hangman;

public class InputHandler
{
    public Difficulty GetDifficulty()
    {
        while (true)
        {
            char key = Console.ReadKey(intercept: true).KeyChar;
            
            switch (key)
            {
                case '1': return Difficulty.Rookie;
                case '2': return Difficulty.Nerd;
                case '3': return Difficulty.Paleontologist;
            }
        }
    }

    public char AskForLetter() // TODO: Add validation for letters
    {
        return Console.ReadKey(intercept: true).KeyChar;
    }
}