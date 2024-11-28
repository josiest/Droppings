using Board;
using UnityEngine;

namespace Snake
{
    public class SnakeNest : GameBoardSubsystem
    {
        /** The snake player object that will be spawned at the start of the game */
        private SnakeBody snakePrefab;
        private SnakeSpawnPoint spawnPoint;

        private Vector2Int startPosition = Vector2Int.zero;
        private CardinalDirection startDirection = CardinalDirection.East;

        public delegate void SnakeSpawnedEvent(SnakeBody snake);
        public SnakeSpawnedEvent OnSnakeSpawned;

        /** A reference to the spawned snake */
        public SnakeBody Snake { get; private set; }

        private void Awake()
        {
            var settings = Resources.Load<NestSettings>(NestSettings.ResourcePath);
            if (!settings)
            {
                settings = ScriptableObject.CreateInstance<NestSettings>();
                Debug.LogWarning($"Unable to load nest settings at {NestSettings.ResourcePath}, " +
                                  "using default settings instead");
            }
            snakePrefab = settings.snakePrefab;
            spawnPoint = FindObjectOfType<SnakeSpawnPoint>();
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
            Snake = SnakeBody.SpawnAt(snakePrefab.gameObject, GameBoardSystem.CurrentBoard,
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