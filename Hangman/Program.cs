using Hangman;

Display display = new Display();
InputHandler input = new InputHandler();

display.ShowNameMenu();
string playerName = input.AskForName();

do
{
    display.ShowDifficultyMenu();
    Difficulty difficulty = input.AskForDifficulty();
    
    string word;
    
    try
    {
        word = WordProvider.GetRandomWord(difficulty);
    }
    catch (Exception ex)
    {
        display.ShowError(ex);
        break;
    }
    
    GameLogic logic = new GameLogic(word);
    GuessResult? result = null;

    while (!logic.IsGameOver())
    {
        display.ShowGame(logic, result);

        char guess = input.AskForLetter();
        result = logic.Guess(guess);
    }

    GameScore scoreRecord = new GameScore(playerName, difficulty, word, logic.CurrentMistakes, logic.IsWin(), DateTimeOffset.Now);

    try
    {
        ScoreSerialization.SaveScore(scoreRecord);
    }
    catch (Exception ex)
    {
        display.ShowError(ex);
        break;
    }

    if (logic.IsWin())
    {
        display.ShowVictory(scoreRecord, ScoreSerialization.GetHighScore());
    }
    else
    {
        display.ShowDefeat(logic.Word, scoreRecord, ScoreSerialization.GetHighScore());
    }
    
    display.ShowPlayAgainMenu();
    
} while (input.AskPlayAgain());
