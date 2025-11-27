namespace Hangman;

public class GameLogic (string word)
{
    public string Word { get; } = word.ToUpper();
    private readonly HashSet<char> _guessedLetters = [];
    public int MaxMistakes => 10;
    public int CurrentMistakes { get; private set; }

    public GuessResult Guess(char letter)
    {
        letter = char.ToUpper(letter);
        
        if (!_guessedLetters.Add(letter)) return GuessResult.Duplicate;
        if (Word.Contains(letter))  return GuessResult.Success;
        
        CurrentMistakes++;
        return GuessResult.Miss;
    }
    
    public string GetMaskedWord()
    {
        return string.Join(" ", Word.Select(c => _guessedLetters.Contains(c) ? c : '_'));
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