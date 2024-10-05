using UnityEngine;

namespace Snake
{
    public class SnakeManager : MonoBehaviour
    {
        /** The snake player object that will be spawned at the start of the game */
        [SerializeField] public GameObject snakePrefab;
        
        /** The position to spawn the snake. TODO: use some sort of game object in-level */
        private readonly Vector3 _startPosition = Vector3.zero;

        /** A reference to the spawned snake */
        private GameObject _snake;
        
        /** A reference to the snake's body component */
        private SnakeBody _snakeBody;

        public void Awake()
        {
            _snake = Instantiate(snakePrefab, _startPosition, Quaternion.identity);
            _snakeBody = _snake.GetComponent<SnakeBody>();
            if (!_snakeBody)
            {
                Debug.LogWarning("Snake Prefab doesn't have Snake Body component. " +
                                 "Adding default component now");
                _snakeBody = _snake.AddComponent<SnakeBody>();
            }
        }
    }
}