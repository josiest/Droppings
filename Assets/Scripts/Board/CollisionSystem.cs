using System.Linq;
using Subsystems;

namespace Board
{
    public class CollisionSystem : SceneSubsystem, ITickable
    {
        private void Awake()
        {
            board = SceneSubsystemLocator.Find<GameBoard>();
        }

        private void Start()
        {
            SceneSubsystemLocator.Find<TickSystem>().AddTickable(this);
        } 
        public void Tick()
        {
            foreach (var playerPiece in board.Pieces.Where(piece => piece.CompareTag(GameBoard.PlayerTag)))
            {
                foreach (var other in board.Pieces.Where(other => !other.CompareTag(GameBoard.PlayerTag))
                                                  .Where(other => playerPiece.Position == other.Position))
                {
                    other.CollideWith(playerPiece);
                }
            }
        }

        private GameBoard board;
    }
}