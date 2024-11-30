using Board;
using Food;
using Score;
using Snake;
using UnityEngine;

namespace Game
{
    public class ResetSystem : GameBoardSubsystem
    {
        public void Reset()
        {
            divineAbacus?.Reset();
            board?.RemoveByTag(Dropping.DroppingTag);
            snakeNest?.Reset();
            fruitTree?.DropFruit(board? board.RandomOpenSpace() : Vector2Int.zero);
        }   
        private void Start()
        {
            fruitTree = GameBoardSystem.Find<DivineFruitTree>();
            snakeNest = GameBoardSystem.Find<SnakeNest>();
            board = GameBoardSystem.CurrentBoard;
            divineAbacus = GameBoardSystem.Find<DivineAbacus>();
        }

        private GameBoard board;
        private DivineFruitTree fruitTree;
        private DivineAbacus divineAbacus;
        private SnakeNest snakeNest;
    }
}