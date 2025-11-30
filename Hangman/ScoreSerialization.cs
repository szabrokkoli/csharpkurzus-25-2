using System.Text.Json;

namespace Hangman;

public class ScoreSerialization(string word, int mistakes, bool isWin)
{
    private readonly string _scoresFilePath = "scores.json";
    
    private readonly JsonSerializerOptions _options = new() 
    { 
        WriteIndented = true,
        PropertyNameCaseInsensitive = true
    };

    public void WriteScore()
    {
        GameScore score = CreateGameScore();   
        try
        {
            List<GameScore> scores = ReadAllScores();
            scores.Add(score);

            string jsonString = JsonSerializer.Serialize(scores, _options);
            File.WriteAllText(_scoresFilePath, jsonString);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[IO Error] Failed to save score: {ex.Message}");
        }
    }

    public List<GameScore> ReadAllScores()
    {
        if (!File.Exists(_scoresFilePath))
        {
            return new List<GameScore>();
        }

        try
        {
            string jsonContent = File.ReadAllText(_scoresFilePath);
            
            if (string.IsNullOrWhiteSpace(jsonContent))
            {
                return new List<GameScore>();
            }

            return JsonSerializer.Deserialize<List<GameScore>>(jsonContent, _options) 
                   ?? new List<GameScore>();
        }
        catch (JsonException)
        {
            Console.WriteLine("Data corruption detected. Returning empty log.");
            return new List<GameScore>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[IO Error] Failed to read scores: {ex.Message}");
            return new List<GameScore>();
        }
    }

    public GameScore CreateGameScore()
    {
        return new GameScore(word, mistakes, isWin, DateTime.Now);
    }
}