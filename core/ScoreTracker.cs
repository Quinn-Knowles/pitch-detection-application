namespace pitch_detection_application.core
{
    public class ScoreTracker
    {
        public int Score { get; private set; } = 0;
        public float Multiplier { get; private set; } = 1.0f;

        public ScoreTracker()
        {
        }

        public void AddScore(int basePoints)
        {
            Score += (int)(basePoints * Multiplier);
        }

        public void SubtractScore(int points)
        {
            Score -= points;

            // Optional: prevent negative score
            if (Score < 0)
            {
                Score = 0;
            }
        }

        public void SetMultiplier(float multiplier)
        {
            if (multiplier < 0f)
            {
                throw new ArgumentOutOfRangeException(nameof(multiplier), "Multiplier cannot be negative.");
            }

            Multiplier = multiplier;
        }

        public void IncreaseMultiplier(float amount)
        {
            SetMultiplier(Multiplier + amount);
        }

        public void ResetMultiplier()
        {
            Multiplier = 1.0f;
        }

        public void ResetScore()
        {
            Score = 0;
        }

        public void ResetAll()
        {
            Score = 0;
            Multiplier = 1.0f;
        }
    }
}