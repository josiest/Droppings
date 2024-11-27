using Board;
using Snake;
using Subsystems;
using UnityEngine;

namespace Food
{
    [RequireComponent(typeof(GameBoard_DEPRECATED), typeof(SnakeNest))]
    public class DivineFruitTree : SceneSubsystem
    {
        [SerializeField] private GameObject FruitPrefab;
        private FoodPickup fruit;
        private GameBoard_DEPRECATED boardDeprecated;

        public void Awake()
        {
            if (!FruitPrefab.GetComponent<FoodPickup>())
            {
                Debug.LogWarning("Fruit prefab has no FoodPickup component. Adding one now");
                FruitPrefab.AddComponent<FoodPickup>();
            }

            boardDeprecated = GetComponent<GameBoard_DEPRECATED>();
            GetComponent<SnakeNest>().OnSnakeSpawned += _ => SpawnFruit();
        }

        private void SpawnFruit()
        {
            fruit = boardDeprecated.CreatePiece<FoodPickup>(FruitPrefab, boardDeprecated.RandomOpenSpace());
        }
        public void DropFruit(Vector2Int position)
        {
            if (fruit) { fruit.Position = position; }
        }
    }
}