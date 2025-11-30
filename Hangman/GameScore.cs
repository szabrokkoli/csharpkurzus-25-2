namespace Hangman;

public record GameScore(
    //string PlayerName,  //todo: add player name
    string Word, 
    int Mistakes, 
    bool IsWin, 
    DateTime Date
);