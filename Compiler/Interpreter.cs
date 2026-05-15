namespace MusicForge;



public class MusicalEvent
{
    public double startBeat;        // measures beats w/ 1st note
    public double durationBeats;    // how much lasts
    public int midiNote;            // 0..127, or -1 for an unpitched sample (noise)
    public string instrument;

    public MusicalEvent(double s, double d, int m, string i)
    {
        startBeat = s; 
        durationBeats = d; 
        midiNote = m; 
        instrument = i;
    }
}


public static class Interpreter
{
    public static double time = 0;           
    public static double bpm = 120;
    public static string instrument = "sine";

    public static List<MusicalEvent> events = new();
    public static Dictionary<string, Define> subroutines = new();

    
    public static void Reset()
    {
        time = 0;
        bpm = 120;
        instrument = "sine";
        events = new List<MusicalEvent>();
        subroutines = new Dictionary<string, Define>();
    }

    
    public static void AddNote(string atom, double duration)
    {
        int midi = ParseNote(atom);
        if (midi >= 0)
            events.Add(new MusicalEvent(time, duration, midi, instrument));
        else
            events.Add(new MusicalEvent(time, duration, -1, atom));
    }

    // Convert a note name into a MIDI number. Returns -1 if it is not a note.
    //   c4  -> 60       (do 1st octave)
    //   a4  -> 69     440gz default mezinarodny оперный камертон
    static int ParseNote(string atom)
    {
        if (atom.Length < 2) return -1;

        // letter a semitone offset within the octave
        int letter = "cdefgab".IndexOf(char.ToLower(atom[0]));
        if (letter < 0) return -1;
        int[] offsets = { 0, 2, 4, 5, 7, 9, 11 };
        int semitone = offsets[letter];

        int i = 1;
        if (i < atom.Length && atom[i] == '#') { semitone = semitone + 1; i = i + 1; }
        else if (i < atom.Length && atom[i] == 'b' && i + 1 < atom.Length)
        {
            // if b is si
            semitone = semitone - 1; i = i + 1;
        }

        if (i >= atom.Length) return -1;

        // обработка минусовой октавы С-1 которая 0 код
        int sign = 1;
        if (atom[i] == '-') { sign = -1; i = i + 1; }
        if (i >= atom.Length) return -1;

        int octave = 0;
        while (i < atom.Length && atom[i] >= '0' && atom[i] <= '9')
        {
            octave = octave * 10 + (atom[i] - '0'); // ascii
            i = i + 1;
        }
        octave = octave * sign;

        // MIDI convention: C-1 is note 0, C0 is 12, C4 is 60.
        int midi = (octave + 1) * 12 + semitone;
        if (midi < 0 || midi > 127) return -1;
        return midi;
    }
}
