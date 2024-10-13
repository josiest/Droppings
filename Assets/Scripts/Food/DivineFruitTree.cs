using Board;
using Scene;
using Snake;
using UnityEngine;

namespace Food
{
    [RequireComponent(typeof(GameBoard), typeof(SnakeNest))]
    public class DivineFruitTree : SceneSubsystem
    {
        [SerializeField] private GameObject fruitPrefab;
        private FoodPickup _fruit;
        private GameBoard _board;

        public void Awake()
        {
            if (!fruitPrefab.GetComponent<FoodPickup>())
            {
                Debug.LogWarning("Fruit prefab has no FoodPickup component. Adding one now");
                fruitPrefab.AddComponent<FoodPickup>();
            }

            _board = GetComponent<GameBoard>();
            var snakeNest = GetComponent<SnakeNest>();
            if (snakeNest.Snake) { SpawnFruit(); }
            else { snakeNest.OnSnakeSpawned += _ => SpawnFruit(); }
        }

        private void SpawnFruit()
        {
            _fruit = _board.CreatePiece<FoodPickup>(fruitPrefab, _board.RandomOpenSpace());
        }

        public void DropFruit(Vector2Int position)
        {
            if (_fruit) { _fruit.Position = position; }
        }
    }
}