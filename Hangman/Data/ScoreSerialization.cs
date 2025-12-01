using System.Text.Json;
using Hangman.Models;

namespace Hangman.Data;

public static class ScoreSerialization
{
    private const string FileName = "scores.json";

    private static readonly JsonSerializerOptions Options = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true
    };

    public static void SaveScore(GameScore score)
    {
        List<GameScore> scores = LoadScores();
        scores.Add(score);
        
        string scoresJson = JsonSerializer.Serialize(scores, Options);
        
        File.WriteAllText(FileName, scoresJson);
    }

    public static List<GameScore> LoadScores()
    {
        if (!File.Exists(FileName)) return new List<GameScore>();

        try
        {
            string scoresJson = File.ReadAllText(FileName);

            if (string.IsNullOrWhiteSpace(scoresJson)) return new List<GameScore>();

            return JsonSerializer.Deserialize<List<GameScore>>(scoresJson, Options) ?? new List<GameScore>();
        }
        catch (JsonException)
        {
            throw new InvalidDataException("Scores file is corrupt");
        }
        catch (IOException)
        {
            throw new IOException("Could not access the score file");
        }
    }

    public static GameScore? GetHighScore()
    {
        List<GameScore> scores = LoadScores();
        
        return scores
            .Where(s => s.IsWin)
            .OrderByDescending(s => s.Score)
            .FirstOrDefault();
    }
}