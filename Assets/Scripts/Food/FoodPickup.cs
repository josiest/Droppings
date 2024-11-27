using Board;
using Snake;
using Subsystems;
using UnityEngine;

namespace Food
{
    public class FoodPickup : Pickup
    {
        private GameBoard_DEPRECATED boardDeprecated;
        protected override void Start()
        {
            base.Start();
            boardDeprecated = SceneSubsystemLocator.Find<GameBoard_DEPRECATED>();
        }

        protected override void ConsumeBy(SnakeBody snake)
        {
            Position = boardDeprecated ? boardDeprecated.RandomOpenSpace() : Vector2Int.zero;
            snake.Digestion.Digest();
        }
    }
}