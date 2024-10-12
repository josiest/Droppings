using Board;
using Scene;
using UnityEngine;

namespace Food
{
    public class DivineFruitTree : SceneSubsystem
    {
        [SerializeField] private GameObject fruitPrefab;
        private FoodPickup _fruit;

        public void Awake()
        {
            if (!fruitPrefab.GetComponent<FoodPickup>())
            {
                Debug.LogWarning("Fruit prefab has no FoodPickup component. Adding one now");
                fruitPrefab.AddComponent<FoodPickup>();
            }

            var board = GetComponent<GameBoard>();
            if (board)
            {
                _fruit = board.CreatePiece<FoodPickup>(fruitPrefab);
            }
            else
            {
                Debug.LogError("Tried to spawn fruit but Game Board doesn't exist");
            }
        }

        public void DropFruit(Vector2Int position)
        {
            if (_fruit) { _fruit.Position = position; }
        }
    }
}