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
        
        /** A reference to the spawned snake */
        private SnakeBody _snake;

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
            _snake = SnakeBody.SpawnAt(snakePrefab, board, _startPosition, _startDirection);
            SceneSubsystems.Find<DivineFruitTree>()?
                           .DropFruit(board.RandomOpenSpace());
        }

        public void RespawnSnake()
        {
            _snake?.ResetTo(_startPosition, _startDirection);
        }
    }
}