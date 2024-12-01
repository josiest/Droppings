using Board;
using Game;
using UnityEngine;

namespace UI
{
    public class GameOverHandler : MonoBehaviour
    {
        public void Resume()
        {
            tickSystem?.Resume();
            gameObject.SetActive(false);
        }

        private void OnGameOver()
        {
            gameObject.SetActive(true);
        }

        private void Start()
        {
            var resetSystem = GameBoardSystem.Find<ResetSystem>();
            if (resetSystem)
            {
                resetSystem.OnGameOver += OnGameOver;
            }
            tickSystem = GameBoardSystem.Find<TickSystem>();
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            tickSystem?.Pause();
        }

        private TickSystem tickSystem;
    }
}