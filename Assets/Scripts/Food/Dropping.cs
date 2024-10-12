using Board;
using Scene;
using Snake;
using UnityEngine;

namespace Food
{
    public class Dropping : Pickup
    {
        private DivineFruitTree _fruitTree;
        public override void Awake()
        {
            base.Awake();
            _fruitTree = SceneSubsystems.Find<DivineFruitTree>();
            if (!_fruitTree)
            {
                Debug.LogError("Fruit tree doesn't exist when dropping is awake");
            }
        }
        protected override void ConsumeBy(SnakeBody snake)
        {
            var board = GameBoard.Instance;
            if (!board) { return; }
            board.ClearByTag("Dropping");

            var snakeMovement = snake.GetComponent<MovementComponent>();
            snakeMovement.Reset();

            _fruitTree?.DropFruit(board.RandomOpenSpace());
        }
    }
}