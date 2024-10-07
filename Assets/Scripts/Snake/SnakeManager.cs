﻿using System.Collections.Generic;
using Food;
using UnityEngine;

namespace Snake
{
    public class SnakeManager : MonoBehaviour
    {
        /** The snake player object that will be spawned at the start of the game */
        [SerializeField] public GameObject snakePrefab;

        /** The food object prefab that the player should consume */
        [SerializeField] public GameObject foodPrefab;
        
        /** The dropping object prefab the player will create after consuming food */
        [SerializeField] public GameObject droppingPrefab;

        /** The position to spawn the snake. TODO: use some sort of game object in-level */
        private readonly Vector3 _startPosition = Vector3.zero;

        /** The dimensions of the game board. TODO: read from level settings */
        private readonly RectInt _boardDimensions = new(-5, -5, 10, 10);

        /** A reference to the spawned snake */
        private GameObject _snake;
        
        /** A reference to the snake's body component */
        private SnakeBody _snakeBody;

        /** A reference to the snake's digestion system */
        private SnakeDigestion _snakeDigestion;

        /** A reference to the food object on the board */
        private GameObject _food;

        /** A list of references to all droppings on the board */
        private readonly List<GameObject> _droppings = new();

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

            _snakeDigestion = _snake.GetComponent<SnakeDigestion>();
            if (!_snakeDigestion)
            {
                Debug.LogWarning("Snake Prefab doesn't have Snake Digestion component. " +
                                 "Adding default now.");
                _snakeDigestion = _snake.AddComponent<SnakeDigestion>();
            }
            _snakeDigestion.LayDropping = LayDropping;

            _food = Instantiate(foodPrefab, RandomOpenSpace(), Quaternion.identity);
            var pickup = _food.GetComponent<Pickup>();
            if (!pickup)
            {
                Debug.LogWarning("Food Prefab doesn't have a Pickup component. " +
                                 "Adding one now");
                pickup = _food.AddComponent<Pickup>();
            }
            pickup.Consume = OnConsumeFood;
        }

        public void LayDropping(Vector3 pos)
        {
            var dropping = Instantiate(droppingPrefab, pos, Quaternion.identity, transform);
            _droppings.Add(dropping);
            var pickup = dropping.GetComponent<Pickup>();
            if (!pickup)
            {
                Debug.LogWarning("Dropping Prefab doesn't have a Pickup component. " +
                                 "Adding one now");
                pickup = dropping.AddComponent<Pickup>();
            }
            pickup.Consume = OnConsumeDropping;
        }

        private Vector3 RandomOpenSpace()
        {
            var pos = RandomSpaceOnBoard();
            while (_snakeBody.CollidesWith(pos))
            {
                pos = RandomSpaceOnBoard();
            }
            return pos;
        }

        private Vector3 RandomSpaceOnBoard()
        {
            var x = Random.Range(_boardDimensions.x,
                                 _boardDimensions.x + _boardDimensions.width);

            var y = Random.Range(_boardDimensions.y,
                                 _boardDimensions.y + _boardDimensions.height);
            return new Vector3(x, y, 0f);
        }

        private void OnConsumeFood(GameObject item)
        {
            item.transform.position = RandomOpenSpace();
            _snakeDigestion.Digest();
        }

        private void OnConsumeDropping(GameObject item)
        {
            _droppings.ForEach(Destroy);
            _droppings.Clear();
            _snakeBody.ResetTo(_startPosition, CardinalDirection.East);
            _food.transform.position = RandomOpenSpace();
        }
    }
}