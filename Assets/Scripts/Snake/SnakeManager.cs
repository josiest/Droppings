using System.Collections.Generic;
using Board;
using Food;
using UnityEngine;

namespace Snake
{
    [RequireComponent(typeof(GameBoard))]
    public class SnakeManager : MonoBehaviour
    {
        /** The snake player object that will be spawned at the start of the game */
        [SerializeField] public GameObject snakePrefab;

        /** The food object prefab that the player should consume */
        [SerializeField] public GameObject foodPrefab;
        
        /** The dropping object prefab the player will create after consuming food */
        [SerializeField] public GameObject droppingPrefab;

        /** The position to spawn the snake. TODO: use some sort of game object in-level */
        private readonly Vector2Int _startPosition = Vector2Int.zero;

        /** The dimensions of the game board. TODO: read from level settings */
        private GameBoard _board;

        /** A reference to the spawned snake */
        private GameObject _snake;
        
        /** A reference to the snake's body component */
        private SnakeBody _snakeBody;

        /** A reference to the snake's digestion system */
        private SnakeDigestion _snakeDigestion;

        /** A reference to the food object on the board */
        private GameObject _food;

        public void Awake()
        {
            _board = GetComponent<GameBoard>();
            if (!_board)
            {
                Debug.LogWarning("Snake Manager has no Game Board component. " +
                                 "Adding one now");
                _board = gameObject.AddComponent<GameBoard>();
            }

            _snake = Instantiate(snakePrefab);
            _snakeBody = _snake.GetComponent<SnakeBody>();
            if (!_snakeBody)
            {
                Debug.LogWarning("Snake Prefab doesn't have Snake Body component. " +
                                 "Adding default component now");
                _snakeBody = _snake.AddComponent<SnakeBody>();
            }
            var snakeMovement = _snakeBody.GetComponent<MovementComponent>();
            _snakeBody.PopulateInDirection(Directions.Opposite(snakeMovement.startingDirection));
            _snakeBody.AddToBoard(_board);
            _snakeBody.LayDropping = LayDropping;

            _snakeDigestion = _snake.GetComponent<SnakeDigestion>();
            if (!_snakeDigestion)
            {
                Debug.LogWarning("Snake Prefab doesn't have Snake Digestion component. " +
                                 "Adding default now.");
                _snakeDigestion = _snake.AddComponent<SnakeDigestion>();
            }

            if (!droppingPrefab.GetComponent<BoardPiece>())
            {
                Debug.LogWarning("Dropping Prefab doesn't have a Board Piece component. " +
                                 "Adding one now");
                droppingPrefab.AddComponent<BoardPiece>();
            }
            if (!droppingPrefab.GetComponent<Pickup>())
            {
                Debug.LogWarning("Dropping Prefab doesn't have a Pickup component. " +
                                 "Adding one now");
                droppingPrefab.AddComponent<Pickup>();
            }

            _food = _board.CreatePiece(foodPrefab, _board.RandomOpenSpace());
            var foodPickup = _food.GetComponent<Pickup>();
            if (!foodPickup)
            {
                Debug.LogWarning("Food Prefab doesn't have a Pickup component. " +
                                 "Adding one now");
                foodPickup = _food.AddComponent<Pickup>();
            }
            foodPickup.Consume = OnConsumeFood;
        }

        public void LayDropping(Vector2Int pos)
        {
            var dropping = _board.CreatePiece(droppingPrefab, pos);
            dropping.GetComponent<Pickup>().Consume = OnConsumeDropping;
        }

        private void OnConsumeFood(GameObject item)
        {
            item.GetComponent<BoardPiece>().Position = _board.RandomOpenSpace();
            _snakeDigestion.Digest();
        }

        private void OnConsumeDropping(GameObject item)
        {
            _board.ClearByTag("Dropping");
            _snakeBody.ResetTo(_startPosition, CardinalDirection.East);
            _food.GetComponent<BoardPiece>().Position = _board.RandomOpenSpace();
        }
    }
}