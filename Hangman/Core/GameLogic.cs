using Hangman.Models;

namespace Hangman.Core;

public class GameLogic (string word, int maxMistakes = 10)
{
    private readonly HashSet<char> _guessedLetters = [];
    
    public IReadOnlySet<char> GuessedLetters => _guessedLetters;
    public string Word { get; } = word.ToUpper();
    public int MaxMistakes { get; } = maxMistakes;
    public int CurrentMistakes { get; private set; }

    public GuessResult Guess(char letter)
    {
        letter = char.ToUpper(letter);
        
        if (!_guessedLetters.Add(letter)) return GuessResult.Duplicate;
        if (Word.Contains(letter))  return GuessResult.Success;
        
        CurrentMistakes++;
        return GuessResult.Miss;
    }

    public bool IsGameOver() 
    {
        return IsWin() || CurrentMistakes >= MaxMistakes;
    }

    public bool IsWin()
    {
        return Word.All(_guessedLetters.Contains);
    }
}