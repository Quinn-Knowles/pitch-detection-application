//receive input from settings or from call.
using pitch_detection_application.audio;

//start taking mic input

// pass mic input to logic in files in audio folder based on settings

//score and feedback system live here

namespace pitch_detection_application.core
{
    
   public class MonitorService
    {
        private readonly MonitorSettings settings;
        private readonly MicCapture mic;

        // Feature modules (optional / conditional)
        private readonly PitchAnalyzer pitchAnalyzer;
        private readonly ScoreTracker? scoreTracker;

        public MonitorService(MonitorSettings settings)
        {
            this.settings = settings ?? 
                throw new ArgumentNullException(nameof(settings));
            mic = new MicCapture();
            mic.ChunkAvailable += OnChunk;

            pitchAnalyzer = new PitchAnalyzer(
                settings.MinPitch,
                settings.MaxPitch);
        }

        public void Start()
        {
            mic.Start();
        }

        public void Stop()
        {
            mic.Stop();
        }

        private void OnChunk(float[] samples)
        {
            pitchAnalyzer.Process(samples);
        }
    }
}