using Board;
using Snake;

namespace Food
{
    public class FoodPickup : Pickup
    {
        protected override void ConsumeBy(SnakeBody snake)
        {
            var board = GameBoard.Instance;
            if (board) { Position = board.RandomOpenSpace(); }

            var snakeDigestion = snake.GetComponent<SnakeDigestion>();
            snakeDigestion.Digest();
        }
    }
}