using System;
using Scene;

namespace Score
{
    public class DivineAbacus : SceneSubsystem
    {
        public delegate void ScoreChangedEvent(int newScore);
        public ScoreChangedEvent OnScoreChanged;

        private int _score;
        public int Score
        {
            get => _score;
            set
            {
                _score = value;
                OnScoreChanged(value);
            }
        }

        public void Reset()
        {
            Score = 0;
        }
    }
}
