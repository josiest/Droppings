using Board;
using Food;
using Scene;
using UnityEngine;

namespace Snake
{
    [RequireComponent(typeof(GameBoard), typeof(TickSystem))]
    public class SnakeManager : MonoBehaviour
    {
        /** The snake player object that will be spawned at the start of the game */
        [SerializeField] public GameObject snakePrefab;

        /** Manage frame-rate-limited objects */
        private TickSystem _tickSystem;
        
        /** The board which keeps track of object position and collisions */
        private GameBoard _board;

        /** A reference to the spawned snake */
        private GameObject _snake;

        public void Awake()
        {
            _tickSystem = GetComponent<TickSystem>();
            if (!_tickSystem)
            {
                Debug.LogWarning("Snake Manager has no Tick Manager component. " +
                                 "Adding one now");
                _tickSystem = gameObject.AddComponent<TickSystem>();
            }

            _board = GetComponent<GameBoard>();
            if (!_board)
            {
                Debug.LogWarning("Snake Manager has no Game Board component. " +
                                 "Adding one now");
                _board = gameObject.AddComponent<GameBoard>();
            }
            _tickSystem.AddTickable(_board);

            _snake = Instantiate(snakePrefab);
            var snakeBody = _snake.GetComponent<SnakeBody>();
            if (!snakeBody)
            {
                Debug.LogWarning("Snake Prefab doesn't have Snake Body component. " +
                                 "Adding default component now");
                snakeBody = _snake.AddComponent<SnakeBody>();
            }
            var snakeMovement = _snake.GetComponent<MovementComponent>();
            _tickSystem.AddTickable(snakeMovement);

            snakeBody.PopulateInDirection(Directions.Opposite(snakeMovement.startingDirection));
            snakeBody.AddToBoard(_board);

            var snakeDigestion = _snake.GetComponent<SnakeDigestion>();
            if (!snakeDigestion)
            {
                Debug.LogWarning("Snake Prefab doesn't have Snake Digestion component. " +
                                 "Adding default now.");
                snakeDigestion = _snake.AddComponent<SnakeDigestion>();
            }
            _tickSystem.AddTickable(snakeDigestion);

            SceneSubsystems.Find<DivineFruitTree>()?
                           .DropFruit(_board.RandomOpenSpace());
        }
    }
}