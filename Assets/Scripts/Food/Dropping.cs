using Board;
using Score;
using Snake;
using UnityEngine;

namespace Food
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Dropping : Pickup
    {
        public const string DroppingTag = "Dropping";

        private GameBoard board;
        private DivineFruitTree fruitTree;
        private DivineAbacus divineAbacus;
        private SnakeNest snakeNest;

        protected override void Start()
        {
            base.Start();
            fruitTree = GameBoardSystem.Find<DivineFruitTree>();
            snakeNest = GameBoardSystem.Find<SnakeNest>();
            board = GameBoardSystem.CurrentBoard;
            divineAbacus = GameBoardSystem.Find<DivineAbacus>();
        }
        protected override void ConsumeBy(SnakeBody snake)
        {
            snake.Digestion.Reset();
            divineAbacus?.Reset();
            board?.RemoveByTag(DroppingTag);
            snakeNest?.Reset();
            fruitTree?.DropFruit(board? board.RandomOpenSpace() : Vector2Int.zero);
        }
    }
}