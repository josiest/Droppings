using Board;
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

            var food = board.FindPieceByTag("Food");
            if (food) { food.Position = board.RandomOpenSpace(); }
        }
    }
}