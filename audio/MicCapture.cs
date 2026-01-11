using NAudio.Wave;
using NAudio.CoreAudioApi;
using System;
using System.Collections.Generic;
using System.Linq;

namespace pitch_detection_application.audio
{
    public class MicCapture
    {
        private WasapiCapture? capture;
        private readonly List<float> buffer = new();

        private const int SampleRate = 44100;
        private const int ChunkMs = 50;
        private readonly int chunkSamples = SampleRate * ChunkMs / 1000;

        public event Action<float[]>? ChunkAvailable;

        public void Start()
        {
            capture = new WasapiCapture();
            capture.DataAvailable += OnDataAvailable;
            capture.StartRecording();
        }

        public void Stop()
        {
            if (capture == null) return;

            capture.StopRecording();
            capture.Dispose();
            capture = null;
            buffer.Clear();
        }

        private void OnDataAvailable(object? sender, WaveInEventArgs e)
        {
            int bytesPerSample = sizeof(float);
            int samples = e.BytesRecorded / bytesPerSample;

            for (int i = 0; i < samples; i++)
            {
                float sample = BitConverter.ToSingle(e.Buffer, i * bytesPerSample);
                buffer.Add(sample);
            }

            while (buffer.Count >= chunkSamples)
            {
                float[] chunk = buffer.Take(chunkSamples).ToArray();
                buffer.RemoveRange(0, chunkSamples);

                ChunkAvailable?.Invoke(chunk);
            }
        }
    }
}
