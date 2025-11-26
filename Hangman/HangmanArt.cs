namespace Hangman;

public static class HangmanArt
{
    public static string WinFrame1 => 
        @" . \O/ .
 .  |  .
 . / \ .".Replace('.', ' '); 
    
    public static string WinFrame2 => 
        @" .  O  .
 . /|\ .
 . < >  .".Replace('.', ' ');

    public static string DrawHangman(int mistakes)
    {
        return mistakes switch
        {
            0 => "", 
            1 => "________",
            2 => "      |\n      |\n      |\n      |\n________",
            3 => "      _\n      |\n      |\n      |\n      |\n________",
            4 => "  _____\n      |\n      |\n      |\n      |\n________",
            5 => "  _____\n  |   |\n      |\n      |\n      |\n________",
            6 => "  _____\n  |   |\n  O   |\n      |\n      |\n________",
            7 => "  _____\n  |   |\n  O   |\n  |   |\n      |\n________",
            8 => "  _____\n  |   |\n  O   |\n /|   |\n      |\n________",
            9 => "  _____\n  |   |\n  O   |\n /|\\  |\n      |\n________",
            10 => "  _____\n  |   |\n  O   |\n /|\\  |\n / \\  |\n________",
            _ =>  "  _____\n  |   |\n  X   |\n /|\\  |\n / \\  |\n________"
        };
    }
}