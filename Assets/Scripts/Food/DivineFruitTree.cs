using Board;
using Scene;
using UnityEngine;

namespace Food
{
    [RequireComponent(typeof(GameBoard))]
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
            _fruit = GetComponent<GameBoard>().CreatePiece<FoodPickup>(fruitPrefab);
        }

        public void DropFruit(Vector2Int position)
        {
            if (_fruit) { _fruit.Position = position; }
        }
    }
}