using System.Linq;

namespace Board
{
    public class CollisionSystem : GameBoardSubsystem, ITickable
    {
        private void OnRegisterGameBoard(GameBoard newBoard)
        {
            board = newBoard;
        }

        private void Start()
        {
            GameBoardSystem.Find<TickSystem>()?.AddTickable(this);
        } 
        public void Tick()
        {
            foreach (var playerPiece in board.Pieces
                         .Where(piece => piece.CompareTag(GameBoard.PlayerTag)))
            {
                foreach (var other in board.Pieces
                             .Where(other => !other.CompareTag(GameBoard.PlayerTag))
                             .Where(other => playerPiece.Position == other.Position))
                {
                    other.CollideWith(playerPiece);
                }
            }
        }

        private GameBoard board;
    }
}