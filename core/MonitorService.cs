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
        private readonly PitchRangeMonitor? rangeMonitor;
        private readonly ScoreTracker? scoreTracker;

        public MonitorService(MonitorSettings settings)
        {
            this.settings = settings;

            mic = new MicCapture();
            mic.ChunkAvailable += OnChunk;

            if (settings.OutOfRange)
                rangeMonitor = new PitchRangeMonitor(settings.MinPitch, settings.MaxPitch);

            if (settings.Score)
                scoreTracker = new ScoreTracker();
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
            // Route audio to enabled features

            if (rangeMonitor != null)
                rangeMonitor.Process(samples);

            if (scoreTracker != null)
                scoreTracker.Process(samples);
        }
    }
}