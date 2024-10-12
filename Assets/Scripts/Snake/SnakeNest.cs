using Board;
using Food;
using Scene;
using UnityEngine;

namespace Snake
{
    [RequireComponent(typeof(GameBoard), typeof(TickSystem))]
    public class SnakeNest : MonoBehaviour
    {
        /** The snake player object that will be spawned at the start of the game */
        [SerializeField] public GameObject snakePrefab;

        /** A reference to the spawned snake */
        private SnakeBody _snake;

        public void Awake()
        {
            var board = GetComponent<GameBoard>();
            if (!snakePrefab.GetComponent<SnakeBody>()) {
                Debug.LogWarning("Snake Prefab has no Snake Body component. Adding default now");
                snakePrefab.AddComponent<SnakeBody>();
            }
            _snake = Instantiate(snakePrefab).GetComponent<SnakeBody>();
            var snakeMovement = _snake.GetComponent<MovementComponent>();
            _snake.PopulateInDirection(Directions.Opposite(snakeMovement.startingDirection));
            _snake.AddToBoard(board);

            SceneSubsystems.Find<DivineFruitTree>()?
                           .DropFruit(board.RandomOpenSpace());
        }
    }
}