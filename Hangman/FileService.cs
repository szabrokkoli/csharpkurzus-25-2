using System.Text.Json;

namespace Hangman;

public class FileService
{
    private readonly string _wordsFilePath;
    private readonly string _scoresFilePath;

    public FileService(string wordsFilePath = "words.json", string scoresFilePath = "scores.json")
    {
        _wordsFilePath = wordsFilePath;
        _scoresFilePath = scoresFilePath;
    }

    public string[] ReadWordsByDifficulty(string difficulty)
    {
        try
        {
            if (!File.Exists(_wordsFilePath))
            {
                Console.WriteLine($"Error: {_wordsFilePath} not found.");
                return Array.Empty<string>();
            }

            string jsonContent = File.ReadAllText(_wordsFilePath);
            using (JsonDocument doc = JsonDocument.Parse(jsonContent))
            {
                JsonElement root = doc.RootElement;

                if (root.TryGetProperty(difficulty, out JsonElement wordsElement))
                {
                    List<string> words = new List<string>();
                    foreach (JsonElement word in wordsElement.EnumerateArray())
                    {
                        var wordStr = word.GetString();
                        if (wordStr != null)
                        {
                            words.Add(wordStr);
                        }
                    }
                    return words.ToArray();
                }
            }

            Console.WriteLine($"Error: Difficulty '{difficulty}' not found in {_wordsFilePath}.");
            return Array.Empty<string>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading words file: {ex.Message}");
            return Array.Empty<string>();
        }
    }

    public void WriteScore(GameScore score)
    {
        try
        {
            // Read existing scores
            GameScore[] existingScores = ReadAllScores();
            List<GameScore> scoresList = new List<GameScore>(existingScores);
            scoresList.Add(score);

            // Write as JSON array
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            string jsonArray = JsonSerializer.Serialize(scoresList, options);
            File.WriteAllText(_scoresFilePath, jsonArray);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing score to file: {ex.Message}");
        }
    }

    public GameScore[] ReadAllScores()
    {
        try
        {
            if (!File.Exists(_scoresFilePath))
            {
                return Array.Empty<GameScore>();
            }

            string jsonContent = File.ReadAllText(_scoresFilePath).Trim();
            
            // Handle empty file
            if (string.IsNullOrEmpty(jsonContent))
            {
                return Array.Empty<GameScore>();
            }

            // Try to deserialize as JSON array
            try
            {
                List<GameScore> scores = JsonSerializer.Deserialize<List<GameScore>>(jsonContent) ?? new List<GameScore>();
                return scores.ToArray();
            }
            catch
            {
                // If array parsing fails, try parsing as line-delimited JSON objects
                List<GameScore> scores = new List<GameScore>();
                string[] lines = jsonContent.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
                foreach (string line in lines)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        var score = JsonSerializer.Deserialize<GameScore>(line);
                        if (score != null)
                        {
                            scores.Add(score);
                        }
                    }
                }
                return scores.ToArray();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading scores file: {ex.Message}");
            return Array.Empty<GameScore>();
        }
    }

    public void DeleteScores()
    {
        try
        {
            File.WriteAllText(_scoresFilePath, "");
            Console.WriteLine("All scores have been deleted.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting scores: {ex.Message}");
        }
    }
}