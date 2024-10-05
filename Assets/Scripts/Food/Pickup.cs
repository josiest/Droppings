using Snake;
using UnityEngine;

namespace Food
{
    public class Pickup : MonoBehaviour
    {
        public delegate void OnConsume(GameObject item);
        public OnConsume Consume;

        public void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.GetComponentInParent<SnakeBody>())
            {
                Consume(gameObject);
            }
        }
    }
}