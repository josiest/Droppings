using Board;
using Game;
using Pi.Subsystems;
using UnityEngine;

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

        private void Awake()
        {
            var resetSystem = GameBoardSystem.FindOrRegister<ResetSystem>();
            if (resetSystem) { resetSystem.OnGameOver += OnGameOver; }
            else
            {
                Debug.LogError("[Droppings.DivineAbacus] Couldn't find ResetSystem.");
            }

            var instance = GameBoardSystem.Find<DivineAbacus>();
            if (instance != this)
            {
                Debug.Log("[Droppings.DivineAbacus] Encountered duplicate Divine Abacus.");
                Destroy(this);
            }
        }

        private void OnGameOver()
        {
            var legacy = GameSubsystems.FindOrRegister<AncestralLegacy>();
            if (legacy) { legacy.PushScore(Score); }
            else
            {
                Debug.LogError("[Droppings.DivineAbacus] Couldn't find Ancestral Legacy on Game Over. " +
                               $"called from {gameObject.GetInstanceID()}", gameObject);
            }
        }

        private int score;
    }
}
