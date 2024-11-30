using Board;

namespace Score
{
    public class DivineAbacus : GameBoardSubsystem
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
