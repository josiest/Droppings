using Board;
using Game;
using Pi.Subsystems;

namespace Score
{
    public class DivineAbacus : GameBoardSubsystem
    {
        public int Score
        {
            get => score;
            set { score = value; OnScoreChanged?.Invoke(value); }
        }

        public void Reset()
        {
            Score = 0;
        }

        public delegate void ScoreChangedEvent(int newScore);
        public ScoreChangedEvent OnScoreChanged;

        private void Start()
        {
            var resetSystem = GameBoardSystem.Find<ResetSystem>();
            if (resetSystem) { resetSystem.OnGameOver += OnGameOver; }
        }

        private void OnGameOver()
        {
            var legacy = GameSubsystems.Find<AncestralLegacy>();
            if (legacy) { legacy.PushScore(Score); }
        }

        private int score;
    }
}
