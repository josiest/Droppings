using Board;
using Scene;
using Score;
using Snake;
using UnityEngine;

namespace Food
{
    public class Dropping : Pickup
    {
        private const string DroppingTag = "Dropping";

        private GameBoard _board;
        private DivineFruitTree _fruitTree;
        private DivineAbacus _divineAbacus;
        private SnakeNest _snakeNest;
        public override void Awake()
        {
            base.Awake();
            _fruitTree = SceneSubsystems.Find<DivineFruitTree>();
            if (!_fruitTree)
            {
                Debug.LogError("Fruit tree doesn't exist when dropping is awake");
            }
            _snakeNest = SceneSubsystems.Find<SnakeNest>();
            if (!_snakeNest)
            {
                Debug.LogError("Snake nest doesn't exist when dropping is awake");
            }
            _board = SceneSubsystems.Find<GameBoard>();
            if (!_board)
            {
                Debug.LogError("Game board doesn't exist when dropping is awake");
            }
            _divineAbacus = SceneSubsystems.Find<DivineAbacus>();
            if (!_divineAbacus)
            {
                Debug.LogError("Divine abacus doesn't exist when dropping is awake");
            }
        }
        protected override void ConsumeBy(SnakeBody snake)
        {
            snake.Digestion.Reset();
            _divineAbacus?.Reset();
            _board?.ClearByTag(DroppingTag);
            _snakeNest?.RespawnSnake();
            _fruitTree?.DropFruit(_board? _board.RandomOpenSpace() : Vector2Int.zero);
        }
    }
}