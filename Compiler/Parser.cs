using static MusicForge.Scanner;   

namespace MusicForge;


public static class Parser
{
    // Parsing whole prog
    public static Block Parse()
    {
        var result = new Block();
        while (kind != NOTHING)
            result.Add(ParseStatement());
        return result;
    }


    static Block ParseBlock()
    {
        var result = new Block();
        while (kind != NOTHING && !(kind == WORD && token == "end"))
            result.Add(ParseStatement());
        Check(WORD, "end");
        Scan();
        return result;
    }

    // Parse one statment
    static Node ParseStatement()
    {
        Check(WORD);
        string word = token;
        Scan();

        switch (word)
        {
            case "play":       return ParsePlay();
            case "sleep":      return new Sleep(ParseNumber());
            case "bpm":        return new Bpm(ParseNumber());
            case "instrument": return new Instrument(ParseAtom());
            case "repeat":     return ParseRepeat();
            case "loop":       return ParseLoop();
            case "define":     return ParseDefine();
            default:           return new Call(word);    
        }
    }



    static Play ParsePlay()
    {
        var note = ParseAtom();
        Const? duration = null;
        if (kind == SYMBOL && token == ",")
        {
            Scan();
            duration = ParseNumber();
        }
        return new Play(note, duration);
    }


    static Repeat ParseRepeat()
    {
        var count = ParseNumber();
        Check(WORD, "do");
        Scan();
        var body = ParseBlock();
        return new Repeat(count, body);
    }


    static Loop ParseLoop()
    {
        Check(WORD, "do");
        Scan();
        var body = ParseBlock();
        return new Loop(body);
    }


    static Define ParseDefine()
    {
        Check(WORD);
        string name = token;
        Scan();
        Check(WORD, "do");
        Scan();
        var body = ParseBlock();
        return new Define(name, body);
    }


    static Const ParseNumber()
    {
        Check(NUMBER);
        double value = double.Parse(token, System.Globalization.CultureInfo.InvariantCulture);
        Scan();
        return new Const(value);
    }


    static Atom ParseAtom()
    {
        Check(ATOM);
        string name = token;
        Scan();
        return new Atom(name);
    }
}
