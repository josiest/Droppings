using Board;
using Food;
using UnityEngine;

namespace Snake
{
    [RequireComponent(typeof(GameBoard), typeof(TickManager))]
    public class SnakeManager : MonoBehaviour
    {
        /** The snake player object that will be spawned at the start of the game */
        [SerializeField] public GameObject snakePrefab;

        /** The food object prefab that the player should consume */
        [SerializeField] public GameObject foodPrefab;

        /** Manage frame-rate-limited objects */
        private TickManager _tickManager;
        
        /** The board which keeps track of object position and collisions */
        private GameBoard _board;

        /** A reference to the spawned snake */
        private GameObject _snake;

        public void Awake()
        {
            _tickManager = GetComponent<TickManager>();
            if (!_tickManager)
            {
                Debug.LogWarning("Snake Manager has no Tick Manager component. " +
                                 "Adding one now");
                _tickManager = gameObject.AddComponent<TickManager>();
            }

            _board = GetComponent<GameBoard>();
            if (!_board)
            {
                Debug.LogWarning("Snake Manager has no Game Board component. " +
                                 "Adding one now");
                _board = gameObject.AddComponent<GameBoard>();
            }
            _tickManager.AddTickable(_board);

            _snake = Instantiate(snakePrefab);
            var snakeBody = _snake.GetComponent<SnakeBody>();
            if (!snakeBody)
            {
                Debug.LogWarning("Snake Prefab doesn't have Snake Body component. " +
                                 "Adding default component now");
                snakeBody = _snake.AddComponent<SnakeBody>();
            }
            var snakeMovement = _snake.GetComponent<MovementComponent>();
            _tickManager.AddTickable(snakeMovement);

            snakeBody.PopulateInDirection(Directions.Opposite(snakeMovement.startingDirection));
            snakeBody.AddToBoard(_board);

            var snakeDigestion = _snake.GetComponent<SnakeDigestion>();
            if (!snakeDigestion)
            {
                Debug.LogWarning("Snake Prefab doesn't have Snake Digestion component. " +
                                 "Adding default now.");
                snakeDigestion = _snake.AddComponent<SnakeDigestion>();
            }
            _tickManager.AddTickable(snakeDigestion);

            if (!foodPrefab.GetComponent<BoardPiece>())
            {
                Debug.LogWarning("Food Prefab doesn't have a Board Piece component. " +
                                 "Adding one now");
                foodPrefab.AddComponent<BoardPiece>();
            }
            if (!foodPrefab.GetComponent<FoodPickup>())
            {
                Debug.LogWarning("Food Prefab doesn't have a Food component. " +
                                 "Adding one now");
                foodPrefab.AddComponent<FoodPickup>();
            }
            _board.CreatePiece(foodPrefab, _board.RandomOpenSpace());
        }
    }
}