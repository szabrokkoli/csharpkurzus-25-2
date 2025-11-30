namespace Hangman;

public static class Art
{
    public static string DrawScene(int mistakes)
    {
        return Stages[Math.Min(mistakes, Stages.Length - 1)];
    }

    public static string GetVictoryFrame(int frameIndex)
    {
        return (frameIndex % 2 == 0) ? VictoryIdle : VictoryRoar;
    }

    public static string GetDefeatScene()
    {
        return Explosion;
    }
    
    // Art storage
    
    private static readonly string[] Stages = 
    [
        // 0.
        @"
          
          
          
          
          
                       __
                      ( ^ )
             _.----._/ /
            /         /
         __/ (  | (  |
        /__.-'|_|--|_|



        ",

        // 1.
        @"
           .
          
          
          
          
                       __
                      ( o )
             _.----._/ /
            /         /
         __/ (  | (  |
        /__.-'|_|--|_|



        ",

        // 2.
        @"
          
              o
          
          
          
                       __
                      ( o )
             _.----._/ /
            /         /
         __/ (  | (  |
        /__.-'|_|--|_|



        ",

        // 3.
        @"
          
                O
          
          
          
                       __
                      ( o )
             _.----._/ /
            /         /
         __/ (  | (  |
        /__.-'|_|--|_|



        ",

        // 4.
        @"
          
                 @
            
          
          
                       __
                      ( O )
             _.----._/ /
            /         /
         __/ (  | (  |
        /__.-'|_|--|_|



        ",

        // 5.
        @"
          
          
                  @
             
                             
                       __
                      ( O )
             _.----._/ /
            /         /
         __/ (  | (  |
        /__.-'|_|--|_|



        ",
        
        // 6.
        @"
          
          
                  (@)
             
                             !
                       __
                      ( O )
             _.----._/ /
            /         /
         __/ (  | (  |
        /__.-'|_|--|_|



        ",

        // 7.
        @"
          
          
          
                  ((@))
                
                       __     !!
                      ( @ )
             _.----._/ /
            /         /
         __/ (  | (  |
        /__.-'|_|--|_|



        ",

        // 8.
        @"
          
          
          
          
                  ((@))       
                       __     !!!
                      { @ }
             _.----._/ /
            /         /
         __/ (  | (  |
        /__.-'|_|--|_|



        ",

        // 9.
        @"
          
          
          
          
          
                 ((@)) __    *horrified roar*    
                      { O( 
             _.----._/ /
            /         /
         __/ (  | (  |
        /__.-'|_|--|_|



        ",
        
    ];

    
    private const string VictoryIdle = 
        @"





               __
              ( ^ )  
     _.----._/ /
    /         /
 __/ (  / (  |
/__.-'|_|--|_|



    ";

    private const string VictoryRoar = 
        @"





               __    *happy roar*
              ( ^(   ) ) ) 
     _.----._/ /
    /         /
 __/ (  / (  |
/__.-'|_|--|_|



    ";

    private const string Explosion = 
        @"
           _.-^^---....,,--
       _--                  --_
      <                       >)
      |                        |
       \._                  _./
          ```--. . , ; .--'''
                | |   |
             .-=||  | |=-.
             `-=#$%&%$#=-'
                | ;  :|
       _____.,-#%&$@%#&#~,._____



    ";
}