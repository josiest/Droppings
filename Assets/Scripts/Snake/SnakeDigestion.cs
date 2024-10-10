using Board;
using UnityEngine;

namespace Snake
{
    [RequireComponent(typeof(SnakeBody))]
    public class SnakeDigestion : Tickable
    {
        /** The max number of frames before a snake lays a dropping after consuming food */
        [SerializeField] public int numDigestionFrames = 3;

        /** The current number of frames until a dropping is layed */
        private int _droppingTimer = -1;

        /** A reference to the snake body */
        private SnakeBody _snakeBody;

        public void Awake()
        {
            _snakeBody = GetComponent<SnakeBody>();
        }

        public override void Tick()
        {
            if (_droppingTimer >= 0)
            {
                _droppingTimer -= 1;
            }
            if (_droppingTimer == 0)
            {
                _snakeBody.ShouldLayDropping = true;
            }
        }

        public void Digest()
        {
            _droppingTimer = numDigestionFrames;
        }
    }
}