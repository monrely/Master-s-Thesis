using System.IO;
using NAudio.Wave;

namespace MusicForge;

// We render the list of MusicalEvents into a buffer of float samples,
// then hand the 16-bit PCM bytes to NAudio's WaveOutEvent for playback.
// basically measure the length, making sin, float -> 16-bit pCM
public static class Audio
{
    const int sampleRate = 44100;

    static WaveOutEvent? waveOut;
    static MemoryStream? pcmStream;

    // playing Interpreter.events
    public static void Play()
    {
        Stop();

        byte[] pcm = Render();
        pcmStream = new MemoryStream(pcm);

        var source = new RawSourceWaveStream(
                         pcmStream,
                         new WaveFormat(sampleRate, 16, 1));

        waveOut = new WaveOutEvent();
        waveOut.Init(source);
        waveOut.Play();
    }


    public static void Stop()
    {
        if (waveOut != null)
        {
            waveOut.Stop();
            waveOut.Dispose();
            waveOut = null;
        }
        if (pcmStream != null)
        {
            pcmStream.Dispose();
            pcmStream = null;
        }
    }


    static byte[] Render()
    {
        double maxEnd = 0;
        foreach (var e in Interpreter.events)
        {
            double end = (e.startBeat + e.durationBeats) * 60.0 / Interpreter.bpm;
            if (end > maxEnd) maxEnd = end;
        }
        int total = (int)((maxEnd + 0.25) * sampleRate);
        if (total < sampleRate / 2) total = sampleRate / 2;




        var buffer = new float[total];
        int fade = (int)(0.005 * sampleRate);    
        foreach (var e in Interpreter.events)
        {
            if (e.midiNote < 0) continue;      

            double freq = 440.0 * System.Math.Pow(2.0, (e.midiNote - 69) / 12.0);
            int start  = (int)(e.startBeat     * 60.0 / Interpreter.bpm * sampleRate);
            int length = (int)(e.durationBeats * 60.0 / Interpreter.bpm * sampleRate);
            if (length < 1) length = 1;

            for (int i = 0; i < length; i++)
            {
                int idx = start + i;
                if (idx < 0 || idx >= buffer.Length) continue;

                double t = (double)i / sampleRate;
                double sample = System.Math.Sin(2 * System.Math.PI * freq * t);

                double env = 0.6;
                if (i < fade)            env *= (double)i / fade;
                if (i > length - fade)   env *= (double)(length - i) / fade;

                buffer[idx] += (float)(sample * env);
            }
        }





        float peak = 0;
        for (int i = 0; i < buffer.Length; i++)
        {
            float a = buffer[i] >= 0 ? buffer[i] : -buffer[i];
            if (a > peak) peak = a;
        }
        if (peak > 1.0f)
            for (int i = 0; i < buffer.Length; i++) buffer[i] /= peak;






        var pcm = new byte[buffer.Length * 2];
        for (int i = 0; i < buffer.Length; i++)
        {
            short s = (short)(buffer[i] * 30000);
            pcm[2 * i]     = (byte)(s & 0xFF);
            pcm[2 * i + 1] = (byte)((s >> 8) & 0xFF);
        }
        return pcm;
    }
}
