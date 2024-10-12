using Board;
using Scene;
using Snake;
using UnityEngine;

namespace Food
{
    public class FoodPickup : Pickup
    {
        private GameBoard _board;
        public void Start()
        {
            _board = SceneSubsystems.Find<GameBoard>();
        }

        protected override void ConsumeBy(SnakeBody snake)
        {
            Position = _board ? _board.RandomOpenSpace() : Vector2Int.zero;
            snake.Digestion.Digest();
        }
    }
}