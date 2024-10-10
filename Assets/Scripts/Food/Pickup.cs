using Board;
using Snake;

namespace Food
{
    public class Pickup : BoardPiece
    {
        public override void CollideWith(BoardPiece other)
        {
            var snakeBody = other.GetComponentInParent<SnakeBody>();
            if (snakeBody) { ConsumeBy(snakeBody); }
        }

        protected virtual void ConsumeBy(SnakeBody snake) {}
    }
}