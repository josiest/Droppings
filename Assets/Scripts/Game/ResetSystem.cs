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
        private void Awake()
        {
            board = GameBoardSystem.CurrentBoard;
            fruitTree = GameBoardSystem.FindOrRegister<DivineFruitTree>();
            snakeNest = GameBoardSystem.FindOrRegister<SnakeNest>();
            divineAbacus = GameBoardSystem.FindOrRegister<DivineAbacus>();
        }

        private GameBoard board;
        private DivineFruitTree fruitTree;
        private DivineAbacus divineAbacus;
        private SnakeNest snakeNest;
    }
}