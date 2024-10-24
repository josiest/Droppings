using Board;
using Snake;

namespace Food
{
    public class Pickup : BoardPiece
    {
        public delegate void ConsumedEvent();
        public ConsumedEvent OnConsumed;

        public override void CollideWith(BoardPiece other)
        {
            var snakeBody = other.GetComponentInParent<SnakeBody>();
            if (snakeBody) { ConsumeInternal(snakeBody); }
        }

        protected virtual void ConsumeBy(SnakeBody snake) {}
        private void ConsumeInternal(SnakeBody snake)
        {
            ConsumeBy(snake);
            OnConsumed?.Invoke();
        }
    }
}