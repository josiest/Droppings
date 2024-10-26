using Board;
using Snake;
using Subsystems;
using UnityEngine;

namespace Food
{
    [RequireComponent(typeof(GameBoard), typeof(SnakeNest))]
    public class DivineFruitTree : SceneSubsystem
    {
        [SerializeField] private GameObject FruitPrefab;
        private FoodPickup fruit;
        private GameBoard board;

        public void Awake()
        {
            if (!FruitPrefab.GetComponent<FoodPickup>())
            {
                Debug.LogWarning("Fruit prefab has no FoodPickup component. Adding one now");
                FruitPrefab.AddComponent<FoodPickup>();
            }

            board = GetComponent<GameBoard>();
            GetComponent<SnakeNest>().OnSnakeSpawned += _ => SpawnFruit();
        }

        private void SpawnFruit()
        {
            fruit = board.CreatePiece<FoodPickup>(FruitPrefab, board.RandomOpenSpace());
        }
        public void DropFruit(Vector2Int position)
        {
            if (fruit) { fruit.Position = position; }
        }
    }
}