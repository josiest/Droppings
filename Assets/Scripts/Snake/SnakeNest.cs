using Board;
using Food;
using Scene;
using UnityEngine;

namespace Snake
{
    [RequireComponent(typeof(GameBoard), typeof(TickSystem))]
    public class SnakeNest : SceneSubsystem
    {
        /** The snake player object that will be spawned at the start of the game */
        [SerializeField] public GameObject snakePrefab;
        [SerializeField] private SnakeSpawnPoint spawnPoint;

        private Vector2Int _startPosition = Vector2Int.zero;
        private CardinalDirection _startDirection = CardinalDirection.East;

        public delegate void SnakeSpawnedEvent(SnakeBody snake);
        public SnakeSpawnedEvent OnSnakeSpawned;
        
        /** A reference to the spawned snake */
        public SnakeBody Snake { get; private set; }

        public void Awake()
        {
            var board = GetComponent<GameBoard>();
            if (!snakePrefab.GetComponent<SnakeBody>())
            {
                Debug.LogWarning("Snake Prefab has no Snake Body component. Adding default now");
                snakePrefab.AddComponent<SnakeBody>();
            }
            if (spawnPoint)
            {
                _startPosition = spawnPoint.Position;
                _startDirection = spawnPoint.Direction;
            }
            else
            {
                Debug.LogWarning("Snake nest has no spawn point. Using default instead.");
            }
            Snake = SnakeBody.SpawnAt(snakePrefab, board, _startPosition, _startDirection);
            OnSnakeSpawned(Snake);
        }

        public void RespawnSnake()
        {
            Snake?.ResetTo(_startPosition, _startDirection);
        }
    }
}