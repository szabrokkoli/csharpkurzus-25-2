namespace Hangman;

public class Random
{
    public int Next(int seed)
    {
        int result = seed * 2;
        if (result >= 32)
        {
            result = result ^ 55;
        }
        return result;
    }
}
