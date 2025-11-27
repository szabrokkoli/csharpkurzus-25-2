using System.Text;

namespace Hangman;

public static class Art // TODO: clean it up, it works but it's ugly
{
    private const int SceneWidth = 60; 
    private const int SkyHeight = 10;
    private const int DinoPad = 22; 

    public static string DrawScene(int mistakes)
    {
        int safeMistakes = Math.Min(mistakes, SkyHeight - 1);
        var skyLines = new List<string>();
        
        for (int i = 0; i < SkyHeight; i++)
        {
            if (mistakes > 0 && i == safeMistakes - 1)
            {
                int distanceFromImpact = (SkyHeight + 2) - i;
                int indent = 10 + (int)(distanceFromImpact * 1.5);
                
                skyLines.Add(new string(' ', indent) + "( @ )");
            }
            else
            {
                skyLines.Add(""); 
            }
        }
        return RenderFrame(skyLines.Concat(DinoLookUp), padding: DinoPad);
    }

    public static string GetVictoryFrame(int frameIndex)
    {
        var dinoFrame = (frameIndex % 2 == 0) ? DinoIdle : DinoRoar;
        var sky = Enumerable.Repeat("", SkyHeight / 2); 
        return RenderFrame(sky.Concat(dinoFrame), padding: DinoPad);
    }

    public static string GetDefeatScene()
    {
        return RenderFrame(Explosion, padding: 12);
    }
    
    private static string RenderFrame(IEnumerable<string> lines, int padding)
    {
        var sb = new StringBuilder();
        string padStr = new string(' ', padding);

        foreach (var line in lines)
        {
            sb.AppendLine((padStr + line).PadRight(SceneWidth));
        }
        return sb.ToString();
    }

    private static readonly string[] DinoIdle = 
    [
        "               __", 
        "              ( ^ )", 
        "     _.----._/ /", 
        "    /         /", 
        " __/ (  / (  |", 
        "/__.-'|_|--|_|"
    ];

    private static readonly string[] DinoRoar = 
    [
        "               __    *rawr*", 
        "              ( ^(   ) ) ) ", 
        "     _.----._/ /", 
        "    /         /", 
        " __/ (  / (  |", 
        "/__.-'|_|--|_|"
    ];

    private static readonly string[] DinoLookUp = 
    [
        "               __", 
        "              / _)", 
        "     _.----._/ /", 
        "    /         /", 
        " __/ (  | (  |", 
        "/__.-'|_|--|_|"          
    ];

    private static readonly string[] Explosion =
    [
        "           _.-^^---....,,--", 
        "       _--                  --_", 
        "      <                        >)", 
        "      |                        |", 
        "       \\._                  _./", 
        "          ```--. . , ; .--'''", 
        "                | |   |",          
        "             .-=||  | |=-.", 
        "             `-=#$%&%$#=-'", 
        "                | ;  :|",          
        "       _____.,-#%&$@%#&#~,._____"
    ];
}