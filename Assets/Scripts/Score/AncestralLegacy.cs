using Pi.Subsystems;

namespace Score
{
    public class AncestralLegacy : GameSubsystem
    {
        public delegate void ScoreEvent(int score);
        public ScoreEvent OnNewHighScore;

        public int HighScore { get; set; }

        public void PushScore(int score)
        {
            if (score <= HighScore) { return; }
            HighScore = score;
            OnNewHighScore?.Invoke(HighScore);
        }
    }
}