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

        private GameBoard _board;
        private DivineFruitTree _fruitTree;
        private DivineAbacus _divineAbacus;
        private SnakeNest _snakeNest;

        protected override void Start()
        {
            base.Start();
            _fruitTree = SceneSubsystemLocator.Find<DivineFruitTree>();
            _snakeNest = SceneSubsystemLocator.Find<SnakeNest>();
            _board = SceneSubsystemLocator.Find<GameBoard>();
            _divineAbacus = SceneSubsystemLocator.Find<DivineAbacus>();
        }
        protected override void ConsumeBy(SnakeBody snake)
        {
            snake.Digestion.Reset();
            _divineAbacus?.Reset();
            _board?.ClearByTag(DroppingTag);
            _snakeNest?.Reset();
            _fruitTree?.DropFruit(_board? _board.RandomOpenSpace() : Vector2Int.zero);
        }
    }
}