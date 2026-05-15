namespace MusicForge;


public static class Scanner
{
    public const int NOTHING = 0; 
    public const int NUMBER  = 1;  
    public const int WORD    = 2;   
    public const int SYMBOL  = 3;  
    public const int ATOM    = 4;   // zacatok od :note alebo :instrument

    // Globals
    public static string input = "";
    public static int index = 0;
    public static char look = '\0';    

    public static string token = "";
    public static int kind = NOTHING;
    public static int position = 0;

 
    public static void Next()
    {
        if (index >= input.Length)
            look = '\0';
        else
        {
            look = input[index];
            index = index + 1;
        }
    }

  
    public static void Scan()
    {
        
        while (look == ' ' || look == '\t' || look == '\n' || look == '\r' || look == '#')
        {
            if (look == '#')
                while (look != '\0' && look != '\n' && look != '\r') Next();
            else
                Next();
        }

        token = "";
        position = index - 1;

        if (look == '\0')
        {
            kind = NOTHING;
        }
        else if (IsDigit(look))
        {
            
            while (IsDigit(look)) { token = token + look; Next(); }
            if (look == '.')
            {
                token = token + look; 
                Next();
                while (IsDigit(look)) { token = token + look; Next(); }
            }
            kind = NUMBER;
        }
        else if (IsLetter(look) || look == '_')
        {
             
            while (IsLetter(look) || IsDigit(look) || look == '_')
            {
                token = token + look;
                Next();
            }
            kind = WORD;
        }
        else if (look == ':')
        {
             
            Next();    
            while (IsLetter(look) || IsDigit(look) || look == '_' || look == '#' || look == '-')
            {
                token = token + look;
                Next();
            }
            kind = ATOM;
        }
        else
        {
 
            token = look.ToString();
            Next();
            kind = SYMBOL;
        }
    }

     
    public static void Check(int expectedKind, string? expectedToken = null)
    {
        if (kind != expectedKind)
            throw new System.Exception(
                $"At position {position}: expected kind {expectedKind}, but got '{token}' (kind {kind})");

        if (expectedToken != null && token != expectedToken)
            throw new System.Exception(
                $"At position {position}: expected '{expectedToken}', but got '{token}'");
    }

    static bool IsDigit(char c)  => c >= '0' && c <= '9';
    static bool IsLetter(char c) => (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
}
