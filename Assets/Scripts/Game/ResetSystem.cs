using Board;
using Food;
using Score;
using Snake;
using UnityEngine;

namespace Game
{
    public class ResetSystem : GameBoardSubsystem
    {
        public delegate void ResetEvent();
        public ResetEvent OnGameOver;
        public ResetEvent OnReset;

        public void GameOver()
        {
            OnGameOver?.Invoke();
        }

        public void Reset()
        {
            divineAbacus?.Reset();
            board?.RemoveImmediateByTag(Dropping.DroppingTag);
            snakeNest?.Reset();
            fruitTree?.DropFruit(board? board.RandomOpenSpace() : Vector2Int.zero);
            OnReset?.Invoke();
        }   
        private void Start()
        {
            board = GameBoardSystem.CurrentBoard;
            fruitTree = GameBoardSystem.Find<DivineFruitTree>();
            snakeNest = GameBoardSystem.Find<SnakeNest>();
            divineAbacus = GameBoardSystem.Find<DivineAbacus>();
        }

        private GameBoard board;
        private DivineFruitTree fruitTree;
        private DivineAbacus divineAbacus;
        private SnakeNest snakeNest;
    }
}