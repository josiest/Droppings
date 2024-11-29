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
                Debug.LogError($"Unable to load nest settings at {NestSettings.ResourcePath}. " +
                                "The snake will not be able to load without it.");
                Application.Quit();
                return;
            }
            snakePrefab = settings.snakePrefab;
            if (!snakePrefab)
            {
                Debug.LogError($"Nest settings at {NestSettings.ResourcePath} have no snake prefab. " +
                                "The snake will not be able to load without it.");
                Application.Quit();
                return;
            }
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