namespace MusicForge;

 

public abstract class Node
{
    public abstract void Interpret();
}


public class Block : Node
{
    public List<Node> items = new();

    public void Add(Node item) { items.Add(item); }

    public override void Interpret()
    {
        foreach (var item in items) item.Interpret();
    }
}


public class Const : Node
{
    public double value;
    public Const(double v) { value = v; }
    public override void Interpret() { }  
}


public class Atom : Node
{
    public string name;
    public Atom(string n) { name = n; }
    public override void Interpret() { }
}


public class Play : Node
{
    public Atom note;
    public Const? duration;

    public Play(Atom n, Const? d) { note = n; duration = d; }

    public override void Interpret()
    {
        double dur = duration != null ? duration.value : 1.0;
        Interpreter.AddNote(note.name, dur);
    }
}
 

public class Sleep : Node
{
    public Const amount;
    public Sleep(Const a) { amount = a; }
    public override void Interpret()
    {
        Interpreter.time = Interpreter.time + amount.value;
    }
}


public class Bpm : Node
{
    public Const value;
    public Bpm(Const v) { value = v; }
    public override void Interpret()
    {
        Interpreter.bpm = value.value;
    }
}


public class Instrument : Node
{
    public Atom name;
    public Instrument(Atom n) { name = n; }
    public override void Interpret()
    {
        Interpreter.instrument = name.name;
    }
}


public class Repeat : Node
{
    public Const count;
    public Block body;
    public Repeat(Const c, Block b) { count = c; body = b; }

    public override void Interpret()
    {
        int n = (int)count.value;
        for (int i = 0; i < n; i++) body.Interpret();
    }
}


public class Loop : Node
{
    public Block body;
    public Loop(Block b) { body = b; }

    public override void Interpret()
    {
        for (int i = 0; i < 32; i++) body.Interpret();
    }
}


public class Define : Node
{
    public string name;
    public Block body;
    public Define(string n, Block b) { name = n; body = b; }

    public override void Interpret()
    {
        Interpreter.subroutines[name] = this;
    }
}


public class Call : Node
{
    public string name;
    public Call(string n) { name = n; }

    public override void Interpret()
    {
        if (!Interpreter.subroutines.ContainsKey(name))
            throw new System.Exception($"unknown command '{name}'");
        Interpreter.subroutines[name].body.Interpret();
    }
}
