using Hangman;

Display display = new Display();
InputHandler input = new InputHandler();

// 1. Menu

display.ShowDifficultyMenu();
Difficulty diff = input.GetDifficulty();


// 2. Setup

string word = WordService.GetRandomWord(diff);
GameLogic logic = new GameLogic(word);
GuessResult? result = null;


// 3. Game loop

while (!logic.IsGameOver())
{
    display.ShowGame(logic, result);
    
    char guess = input.AskForLetter();
    result = logic.Guess(guess);
}


// 4. End

if (logic.IsWin())
{
    display.ShowVictoryAnimation();
}
else
{
    display.ShowDefeat(logic.Word);
}