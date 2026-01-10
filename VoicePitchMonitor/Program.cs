using System;
using NAudio.Wave;

class Program
{
    static void Main()
    {
        Console.WriteLine("Starting microphone test...");

        var waveIn = new WaveInEvent
        {
            WaveFormat = new WaveFormat(44100, 1) // 44.1kHz mono
        };

        waveIn.DataAvailable += OnDataAvailable;
        waveIn.StartRecording();

        Console.WriteLine("Recording... press any key to stop.");
        Console.ReadKey();

        waveIn.StopRecording();
        waveIn.Dispose();
    }

    static void OnDataAvailable(object? sender, WaveInEventArgs e)
    {
        // This runs every ~50ms with live audio data
        Console.WriteLine($"Received {e.BytesRecorded} bytes of audio.");
    }
}
