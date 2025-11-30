using System;
using System.Collections.Generic;

namespace Hangman;

public static class Tester
{
    private static List<(string Name, Action Test)> _tests = new();

    static Tester()
    {
        // Register all tests here
        _tests.Add(("RandomTest", RandomTest.Run));
        _tests.Add(("FileServiceTest", FileServiceTest.Run));
    }

    /// <summary>
    /// Runs all registered tests and reports results.
    /// </summary>
    public static void RunAll()
    {
        Console.WriteLine("═══════════════════════════════════════");
        Console.WriteLine("Running Tests...");
        Console.WriteLine("═══════════════════════════════════════");

        int passed = 0;
        int failed = 0;

        foreach (var (testName, testAction) in _tests)
        {
            try
            {
                Console.Write($"[{testName}] ");
                testAction();
                Console.WriteLine("✓ PASSED");
                passed++;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ FAILED: {ex.Message}");
                failed++;
            }
        }

        Console.WriteLine("═══════════════════════════════════════");
        Console.WriteLine($"Results: {passed} passed, {failed} failed");
        Console.WriteLine("═══════════════════════════════════════");

        if (failed > 0)
        {
            Environment.Exit(1);
        }
    }
}
