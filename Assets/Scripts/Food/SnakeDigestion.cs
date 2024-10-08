using System;
using Snake;
using UnityEngine;

namespace Food
{
    [RequireComponent(typeof(SnakeBody))]
    public class SnakeDigestion : MonoBehaviour
    {
        /** The max number of frames before a snake lays a dropping after consuming food */
        [SerializeField] public int numDigestionFrames = 3;

        /** The current number of frames until a dropping is layed */
        private int _droppingTimer = -1;

        /** A reference to the snake body */
        private SnakeBody _snakeBody;

        public void Digest()
        {
            _droppingTimer = numDigestionFrames;
        }
        
        public void Awake()
        {
            _snakeBody = GetComponent<SnakeBody>();
        }

        public void Update()
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
    }
}