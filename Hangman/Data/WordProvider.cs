using System.Text.Json;
using Hangman.Models;

namespace Hangman.Data;

public static class WordProvider
{
    private const string FileName = "words.json";
    private static Dictionary<string, string[]>? _cachedWords;
    
    public static string GetRandomWord(string difficulty)
    {
        string[] words = LoadWords(difficulty);
        
        return words[System.Random.Shared.Next(words.Length)].ToUpper();   
        
    }

    private static string[] LoadWords(string difficulty)
    {
        if (_cachedWords == null)
        {
            if (!File.Exists(FileName))
            {
                throw new FileNotFoundException($"'{FileName}' was not found.");
            }

            try
            {
                string jsonString = File.ReadAllText(FileName);
                _cachedWords = JsonSerializer.Deserialize<Dictionary<string, string[]>>(jsonString)
                               ?? new Dictionary<string, string[]>();
            }
            catch (JsonException ex)
            {
                throw new InvalidDataException("The JSON is malformed", ex);
            }
        }

        if (_cachedWords.TryGetValue(difficulty, out string[]? words) && words.Length > 0)
        {
            return words;
        }

        throw new KeyNotFoundException($"Difficulty '{difficulty}' is not defined in the JSON file.");
    }
}