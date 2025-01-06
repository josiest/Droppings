using System.Linq;

namespace Board
{
    public class CollisionSystem : GameBoardSubsystem, ITickable
    {
        private void Awake()
        {
            GameBoardSystem.FindOrRegister<TickSystem>()?.AddTickable(this);
            board = GameBoardSystem.CurrentBoard;
        } 
        public void Tick()
        {
            foreach (var playerPiece in board.Pieces
                         .Where(piece => piece.CompareTag(GameBoard.PlayerTag)))
            {
                foreach (var other in board.Pieces
                             .Where(other => playerPiece.Position == other.Position)
                             .Where(other => !other.CompareTag(GameBoard.PlayerTag))
                             .Where(other => other.CollisionEnabled))
                {
                    other.CollideWith(playerPiece);
                }
            }
        }

        private GameBoard board;
    }
}