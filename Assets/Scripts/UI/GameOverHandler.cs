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
            tickSystem?.Pause();
            gameObject.SetActive(true);
        }

        private void Start()
        {
            var resetSystem = GameBoardSystem.Find<ResetSystem>();
            if (resetSystem)
            {
                resetSystem.OnGameOver += OnGameOver;
                resetSystem.OnReset += Resume;
            }
            tickSystem = GameBoardSystem.Find<TickSystem>();
            gameObject.SetActive(false);
        }

        private TickSystem tickSystem;
    }
}