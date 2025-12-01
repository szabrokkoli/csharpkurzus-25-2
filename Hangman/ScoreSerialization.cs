using System.Text.Json;

namespace Hangman;

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

        try
        {
            string scoresJson = JsonSerializer.Serialize(scores, Options);
            File.WriteAllText(FileName, scoresJson);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Error] Failed to save score: {ex.Message}");
        }
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
        catch
        {
            Console.WriteLine("[Error] Failed to read scores. Returning empty list.");
            return new List<GameScore>();
        }
    }
}