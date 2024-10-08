using Board;
using Snake;
using UnityEngine;

namespace Food
{
    public class Pickup : BoardPiece
    {
        public delegate void OnConsume(GameObject item);
        public OnConsume Consume;

        public override void CollideWith(GameObject other)
        {
            if (other.GetComponentInParent<SnakeBody>())
            {
                Consume(gameObject);
            }
        }
    }
}