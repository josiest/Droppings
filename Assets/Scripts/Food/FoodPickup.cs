using Board;
using Snake;
using UnityEngine;

namespace Food
{
    public class FoodPickup : Pickup
    {
        public const string DefaultPath = "Defaults/Food";

        // Unity Events
        protected override void Start()
        {
            base.Start();
            board = GameBoardSystem.CurrentBoard;
        }

        // Pickup Interface
        protected override void ConsumeBy(SnakeBody snake)
        {
            Position = board ? board.RandomOpenSpace() : Vector2Int.zero;
            snake.Digestion.Digest();
        }
        
        // Internal Interface
        private GameBoard board;
    }
}