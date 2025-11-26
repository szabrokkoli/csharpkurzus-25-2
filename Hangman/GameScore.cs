namespace Hangman;

public record GameScore(string PlayerName, string Word, int Mistakes, bool IsWin, DateTime Date);