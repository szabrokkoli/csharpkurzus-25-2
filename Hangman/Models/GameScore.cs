namespace Hangman.Models;

public record GameScore(
    string PlayerName,
    Difficulty Difficulty,
    string Word, 
    int Mistakes, 
    bool IsWin, 
    DateTimeOffset Date
){
    public int Score => IsWin ? ((int)Difficulty) * (10 - Mistakes) : 0;
}