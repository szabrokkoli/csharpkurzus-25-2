using Hangman;
using Hangman.Core;
using Hangman.Data;
using Hangman.Models;
using Hangman.UI;

Display display = new Display();
InputHandler input = new InputHandler();

display.ShowNameMenu();
string playerName = input.AskForName();

do
{
    display.ShowDifficultyMenu();
    string difficulty = input.AskForDifficulty();
    Difficulty score_difficulty = Enum.Parse<Difficulty>(difficulty);
    
    string word;
    
    try
    {
        word = WordProvider.GetRandomWord((string)difficulty);
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

    GameScore scoreRecord = new GameScore(playerName, score_difficulty, word, logic.CurrentMistakes, logic.IsWin(), DateTimeOffset.Now);

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
