using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Hangman;

public static class FileServiceTest
{
    /// <summary>
    /// Tests FileService by:
    /// 1. Reading scores.json
    /// 2. If empty: writes a test score, verifies it, then clears it
    /// 3. If not empty: backs up scores, writes test score, verifies it, restores original scores
    /// </summary>
    public static void Run()
    {
        var fileService = new FileService();

        // Read existing scores
        GameScore[] originalScores = fileService.ReadAllScores();

        // Create a test score
        var testScore = new GameScore(
            PlayerName: "TestPlayer",
            Word: "TESTWORD",
            Mistakes: 3,
            IsWin: true,
            Date: DateTime.Now
        );

        if (originalScores.Length == 0)
        {
            // Test case 1: Empty scores.json
            fileService.WriteScore(testScore);

            // Verify write
            GameScore[] readScores = fileService.ReadAllScores();
            Debug.Assert(readScores.Length == 1, "Expected 1 score after write");
            Debug.Assert(readScores[0].PlayerName == "TestPlayer", "Test score not found");

            // Clear scores
            fileService.DeleteScores();
            GameScore[] clearedScores = fileService.ReadAllScores();
            Debug.Assert(clearedScores.Length == 0, "Scores not cleared");
        }
        else
        {
            // Test case 2: Non-empty scores.json
            List<GameScore> backupScores = new List<GameScore>(originalScores);

            fileService.WriteScore(testScore);

            // Verify write
            GameScore[] allScores = fileService.ReadAllScores();
            bool testScoreFound = false;
            foreach (var score in allScores)
            {
                if (score.PlayerName == "TestPlayer")
                {
                    testScoreFound = true;
                    break;
                }
            }
            Debug.Assert(testScoreFound, "Test score not found after write");

            // Clear and restore original scores
            fileService.DeleteScores();
            GameScore[] clearedScores = fileService.ReadAllScores();
            Debug.Assert(clearedScores.Length == 0, "Scores not cleared");

            foreach (var score in backupScores)
            {
                fileService.WriteScore(score);
            }

            // Verify restoration
            GameScore[] restoredScores = fileService.ReadAllScores();
            Debug.Assert(restoredScores.Length == backupScores.Count, 
                $"Restoration failed: expected {backupScores.Count}, got {restoredScores.Length}");
        }
    }
}
