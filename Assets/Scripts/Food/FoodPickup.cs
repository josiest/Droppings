using Board;
using Snake;
using Subsystems;
using UnityEngine;

namespace Food
{
    public class FoodPickup : Pickup
    {
        private GameBoard board;
        protected override void Start()
        {
            base.Start();
            board = SceneSubsystemLocator.Find<GameBoard>();
        }

        protected override void ConsumeBy(SnakeBody snake)
        {
            Position = board ? board.RandomOpenSpace() : Vector2Int.zero;
            snake.Digestion.Digest();
        }
    }
}