using UnityEngine;

namespace Snake
{
    public class SnakeSpawnPoint : MonoBehaviour
    {
        [SerializeField] private CardinalDirection direction = CardinalDirection.East;
        public CardinalDirection Direction => direction;
        public Vector2Int Position => new(Mathf.FloorToInt(transform.position.x),
                                          Mathf.FloorToInt(transform.position.y));
    }
}