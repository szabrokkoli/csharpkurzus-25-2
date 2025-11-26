using static Hangman.DisplayHelper;

namespace Hangman;

public class Display
{
    public void RefreshScreen(GameLogic logic, GuessResult? lastResult = null)
    {
        Reset();
        PrintDivider("HANGMAN");
        PrintMultilineCentered(HangmanArt.DrawHangman(logic.CurrentMistakes), fixedHeight: 7);
        VerticalSpace(2);
        PrintDivider("Guess a letter");
        VerticalSpace(1);
        PrintLine(logic.GetMaskedWord(), ConsoleColor.Cyan);
        VerticalSpace(1);
        PrintLine($"Mistakes: {logic.CurrentMistakes}/{logic.MaxMistakes}");

        if (lastResult != null)
        {
            var (msg, color) = GetStatusMessage(lastResult.Value);
            VerticalSpace(1);
            Console.ForegroundColor = color;
            PrintDivider(msg);
            Console.ResetColor();
        }
        else
        {
            VerticalSpace(1);
            PrintDivider("");
        }
    }

    public char AskForLetter()
    {
        Console.Write("\n> Your Input: ");
        return Console.ReadKey(intercept: true).KeyChar;
    }

    public void ShowEndGame(GameLogic logic)
    {
        if (logic.IsWin())
        {
            int frameCounter = 0;
            
            while (!Console.KeyAvailable)
            {
                Reset();
                PrintDivider("VICTORY!");
                VerticalSpace(1);
                
                string frame = (frameCounter % 2 == 0) ? HangmanArt.WinFrame1 : HangmanArt.WinFrame2;
                PrintMultilineCentered(frame, fixedHeight: 7);

                VerticalSpace(2);
                PrintLine("CONGRATULATIONS! YOU DIDN'T GET HANGED TODAY!", ConsoleColor.Green);
                VerticalSpace(1);
                
                PrintDivider("");

                VerticalSpace(2);
                PrintLine("Press any key to exit...", ConsoleColor.Gray);
                
                Thread.Sleep(1000);
                frameCounter++;
            }

            Console.ReadKey(intercept: true);
        }
        else
        {
            Reset();
            PrintDivider("GAME OVER");
            VerticalSpace(2);
            PrintMultilineCentered(HangmanArt.DrawHangman(logic.MaxMistakes), fixedHeight: 7);
            VerticalSpace(1);
            PrintLine("YOU DIED via CONSOLE APP", ConsoleColor.Red);
            PrintLine($"The word was: {logic.SecretWord}");
            PrintDivider("");
        }
        
        VerticalSpace(2);
        PrintLine("Press any key to exit...", ConsoleColor.Gray);
        Console.ReadKey();
    }

    private (string, ConsoleColor) GetStatusMessage(GuessResult result) => result switch
    {
        GuessResult.Success   => ("Good job!", ConsoleColor.Green),
        GuessResult.Miss      => ("Missed!", ConsoleColor.Red),
        GuessResult.Duplicate => ("Already tried...", ConsoleColor.Yellow),
        _                     => ("", ConsoleColor.White)
    };
}