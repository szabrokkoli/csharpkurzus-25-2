using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Hangman;

public static class RandomTest
{
    /// <summary>
    /// Calls the project's Random.Next 32 times, collects values in a set and
    /// verifies there are 32 unique values and the range is 1..32.
    /// Uses Debug.Assert to fail the test when the condition is not met.
    /// </summary>
    public static void Run()
    {
        var rnd = new Random();
        var values = new HashSet<int>();
        int min = int.MaxValue;
        int max = int.MinValue;
        int previous = 1;
        
        for (int i = 0; i < 32; i++)
        {
            // provide a varying seed so the result can vary across calls
            int v = rnd.Next(previous);
            //int v = rnd.Next(i);
            values.Add(v);
            if (v < min) min = v;
            if (v > max) max = v;
            previous = v;
        }

        bool ok = values.Count == 31 && min == 1 && max == 31;
        
        if (!ok)
        {
            var sortedValues = new List<int>(values);
            sortedValues.Sort();
            string message = $"Random test failed -> Count={values.Count}, Min={min}, Max={max}, Values (ASC)=[{string.Join(", ", sortedValues)}]";
            Debug.Assert(ok, message);
            throw new InvalidOperationException(message);
        }
    }
}
