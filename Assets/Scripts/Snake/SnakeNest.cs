using Board;
using Subsystems;
using UnityEngine;

namespace Snake
{
    [RequireComponent(typeof(GameBoard_DEPRECATED))]
    public class SnakeNest : SceneSubsystem
    {
        /** The snake player object that will be spawned at the start of the game */
        [SerializeField] public GameObject snakePrefab;
        [SerializeField] private SnakeSpawnPoint spawnPoint;

        private Vector2Int startPosition = Vector2Int.zero;
        private CardinalDirection startDirection = CardinalDirection.East;

        public delegate void SnakeSpawnedEvent(SnakeBody snake);
        public SnakeSpawnedEvent OnSnakeSpawned;
        
        /** A reference to the spawned snake */
        public SnakeBody Snake { get; private set; }

        private void Awake()
        {
            if (!snakePrefab.GetComponent<SnakeBody>())
            {
                Debug.LogWarning("Snake Prefab has no Snake Body component. Adding default now");
                snakePrefab.AddComponent<SnakeBody>();
            }
            if (spawnPoint)
            {
                startPosition = spawnPoint.Position;
                startDirection = spawnPoint.Direction;
            }
            else
            {
                Debug.LogWarning("Snake nest has no spawn point. Using default instead.");
            }
        }
        private void Start()
        {
            Snake = SnakeBody.SpawnAt(snakePrefab, GetComponent<GameBoard_DEPRECATED>(),
                                      startPosition, startDirection);
            OnSnakeSpawned?.Invoke(Snake);
        }
        public void Reset()
        {
            var direction = Snake ? Snake.Movement.Direction : startDirection;
            Snake?.ResetTo(startPosition, direction);
        }
    }
}