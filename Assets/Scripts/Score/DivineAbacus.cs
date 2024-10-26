using Subsystems;

namespace Score
{
    public class DivineAbacus : SceneSubsystem
    {
        public int Score
        {
            get => score;
            set { score = value; OnScoreChanged(value); }
        }

        public void Reset()
        {
            Score = 0;
        }

        public delegate void ScoreChangedEvent(int newScore);
        public ScoreChangedEvent OnScoreChanged;

        private int score;
    }
}
