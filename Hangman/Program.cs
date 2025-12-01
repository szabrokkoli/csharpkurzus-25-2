using Hangman;

Display display = new Display();
InputHandler input = new InputHandler();

display.ShowNameMenu();
string playerName = input.AskForName();

do
{
    display.ShowDifficultyMenu();
    Difficulty diff = input.AskForDifficulty();
    
    string word;
    
    try
    {
        word = WordProvider.GetRandomWord(diff);
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

    GameScore score = new GameScore(playerName, word, logic.CurrentMistakes, logic.IsWin(), DateTimeOffset.Now);

    try
    {
        ScoreSerialization.SaveScore(score);
    }
    catch (Exception ex)
    {
        display.ShowError(ex);
        break;
    }

    if (logic.IsWin())
    {
        display.ShowVictory();
    }
    else
    {
        display.ShowDefeat(logic.Word);
    }
    
    display.ShowPlayAgainMenu();
    
} while (input.AskPlayAgain());
