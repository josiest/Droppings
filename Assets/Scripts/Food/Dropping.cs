using Board;
using Score;
using Snake;
using Subsystems;
using UnityEngine;

namespace Food
{
    public class Dropping : Pickup
    {
        public const string DroppingTag = "Dropping";

        private GameBoard_DEPRECATED boardDeprecated;
        private DivineFruitTree fruitTree;
        private DivineAbacus divineAbacus;
        private SnakeNest snakeNest;

        protected override void Start()
        {
            base.Start();
            fruitTree = SceneSubsystemLocator.Find<DivineFruitTree>();
            snakeNest = SceneSubsystemLocator.Find<SnakeNest>();
            boardDeprecated = SceneSubsystemLocator.Find<GameBoard_DEPRECATED>();
            divineAbacus = SceneSubsystemLocator.Find<DivineAbacus>();
        }
        protected override void ConsumeBy(SnakeBody snake)
        {
            snake.Digestion.Reset();
            divineAbacus?.Reset();
            boardDeprecated?.RemoveByTag(DroppingTag);
            snakeNest?.Reset();
            fruitTree?.DropFruit(boardDeprecated? boardDeprecated.RandomOpenSpace() : Vector2Int.zero);
        }
    }
}