using Hangman;

string word = "DEVELOPER"; // csak amíg nincs meg a words.json

GameLogic logic = new GameLogic(word);
Display display = new Display();
GuessResult? lastResult = null;

while (!logic.IsGameOver())
{
    display.RefreshScreen(logic, lastResult);
    
    char input = display.AskForLetter();
    
    lastResult = logic.Guess(input);
}

display.ShowEndGame(logic);

// TODO: Itt majd elmentjük az eredményt a FileService segítségével (scores.json)
// GameScore score = new GameScore("Player1", secretWord, logic.CurrentMistakes, logic.IsWin(), DateTime.Now);
// FileService.SaveScore(score);