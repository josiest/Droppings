using Board;
using Scene;
using Snake;

namespace Food
{
    public class Dropping : Pickup
    {
        protected override void ConsumeBy(SnakeBody snake)
        {
            var board = GameBoard.Instance;
            if (!board) { return; }
            board.ClearByTag("Dropping");

            var snakeMovement = snake.GetComponent<MovementComponent>();
            snakeMovement.Reset();

            SceneSubsystems.Find<DivineFruitTree>()?
                           .DropFruit(board.RandomOpenSpace());
        }
    }
}