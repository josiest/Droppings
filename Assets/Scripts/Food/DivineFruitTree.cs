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

        public void Awake()
        {
            board = GameBoardSystem.CurrentBoard;
            var settings = Resources.Load<TreeSettings>(TreeSettings.ResourcePath);
            if (!settings)
            {
                Debug.LogWarning("[Droppings.DivineFruitTree] Unable to load tree settings at " +
                                 $"{TreeSettings.ResourcePath}, using default settings instead");
                settings = ScriptableObject.CreateInstance<TreeSettings>();
            }
            fruitPrefab = settings.FruitPrefab;

            var snakeNest = GameBoardSystem.FindOrRegister<SnakeNest>();
            if (snakeNest)
            {
                snakeNest.OnSnakeSpawned += _ => SpawnFruit();
            }
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