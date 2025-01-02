using Ascension;
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
            ascensionSystem = GameBoardSystem.FindOrRegister<AscensionSystem>();
        }

        // Pickup Interface
        protected override void ConsumeBy(SnakeBody snake)
        {
            Position = board ? board.RandomOpenSpace() : Vector2Int.zero;
            ascensionSystem.AscensionPoints += 1;
            snake.Digestion.Digest();
        }
        
        // Internal Interface
        private GameBoard board;
        private AscensionSystem ascensionSystem;
    }
}