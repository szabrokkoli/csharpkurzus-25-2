using System;

public class Random
{
    /// <summary>
    /// Linear Feedback Shift Register (LFSR) based pseudo-random number generator.
    /// Generates all numbers 1-31 in a full cycle.
    /// Algorithm: result = seed * 2, if result >= 32 then result = result XOR 35
    /// </summary>
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
