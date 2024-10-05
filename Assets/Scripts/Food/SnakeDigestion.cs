using Snake;
using UnityEngine;

namespace Food
{
    public class SnakeDigestion : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D Other)
        {
            var Snake = GetComponent<MovementComponent>();
            if (!Snake) { return; }

            var Food = Other.gameObject.GetComponent<FoodPickup>();
            if (Food) { Food.Reset(); return; }

            var Dropping = Other.gameObject.GetComponent<DroppingPickup>();
            if (Dropping) { Snake.Reset(); }
        }
    }
}