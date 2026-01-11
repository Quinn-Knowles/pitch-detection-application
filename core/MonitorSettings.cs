namespace pitch_detection_application.core
{
    public class MonitorSettings
    {
        public int MinPitch { get; init; }
        public int MaxPitch { get; init; }

        public bool SentenceMonitor { get; init; }
        public bool OutOfRange { get; init; }
        public bool SelfMute { get; init; }
        public bool Resonance { get; init; }
        public bool Score { get; init; }
    }
}