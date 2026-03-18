
namespace pitch_detection_application.audio
{
    public class PitchAnalyzer(int minPitch, int maxPitch)
    {
    private readonly int minPitch = minPitch;
    private readonly int maxPitch = maxPitch;

        public void Process(float[] samples)
    {
        // analyze samples here
    }
}

}