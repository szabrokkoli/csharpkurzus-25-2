using Hangman;

Display display = new Display();
InputHandler input = new InputHandler();

display.ShowNameMenu();
string playerName = input.AskForName();

display.ShowDifficultyMenu();
Difficulty diff = input.GetDifficulty();

string word = WordProvider.GetRandomWord(diff);
GameLogic logic = new GameLogic(word);
GuessResult? result = null;

while (!logic.IsGameOver())
{
    display.ShowGame(logic, result);
    
    char guess = input.AskForLetter();
    result = logic.Guess(guess);
}

GameScore score = new GameScore(playerName, word, logic.CurrentMistakes, logic.IsWin(), DateTimeOffset.Now);
ScoreSerialization.SaveScore(score);

if (logic.IsWin())
{
    display.ShowVictory();
}
else
{
    display.ShowDefeat(logic.Word);
}