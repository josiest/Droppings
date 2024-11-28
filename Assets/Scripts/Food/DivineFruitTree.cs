using Board;
using Snake;
using UnityEngine;

namespace Food
{
    public class DivineFruitTree : GameBoardSubsystem
    {
        private FoodPickup fruitPrefab;
        private FoodPickup fruit;
        private GameBoard board;

        private void OnRegisterGameBoard(GameBoard newBoard)
        {
            board = newBoard;
        }

        public void Awake()
        {
            var settings = Resources.Load<TreeSettings>(TreeSettings.ResourcePath);
            if (!settings) { settings = ScriptableObject.CreateInstance<TreeSettings>(); }
            fruitPrefab = settings.fruitPrefab;
        }

        private void Start()
        {
            var snakeNest = GameBoardSystem.Find<SnakeNest>();
            if (snakeNest?.Snake) { SpawnFruit(); }
            else if (snakeNest) { snakeNest.OnSnakeSpawned += _ => SpawnFruit(); }
        }

        private void SpawnFruit()
        {
            fruit = board.CreatePiece<FoodPickup>(fruitPrefab.gameObject, board.RandomOpenSpace());
        }
        public void DropFruit(Vector2Int position)
        {
            if (fruit) { fruit.Position = position; }
        }
    }
}