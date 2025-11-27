using System.Text.Json;

namespace Hangman;

public static class WordService
{
    private const string FileName = "words.json";
    
    
    public static string GetRandomWord(Difficulty difficulty)
    {
        string[] words = LoadWords(difficulty);
        if (words.Length == 0)
        {
            throw new Exception("No words found :(");
        }
        return words[Random.Shared.Next(words.Length)].ToUpper();
        
    }

    private static string[] LoadWords(Difficulty difficulty)
    {
        if (!File.Exists(FileName)) return Array.Empty<string>();

        try
        {
            string jsonString = File.ReadAllText(FileName);
            var wordData = JsonSerializer.Deserialize<Dictionary<string, string[]>>(jsonString);

            string key = difficulty.ToString();

            if (wordData != null && wordData.TryGetValue(key, out string[]? words))
            {
                return words;
            }
        }
        catch (Exception)
        {
            return Array.Empty<string>();
        }
        return Array.Empty<string>();
    }
}