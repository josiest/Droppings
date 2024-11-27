using System.Linq;
using Subsystems;

namespace Board
{
    public class CollisionSystem : GameBoardSubsystem, ITickable
    {
        private void Awake()
        {
            boardDeprecated = SceneSubsystemLocator.Find<GameBoard_DEPRECATED>();
        }

        private void Start()
        {
            SceneSubsystemLocator.Find<TickSystem>().AddTickable(this);
        } 
        public void Tick()
        {
            foreach (var playerPiece in boardDeprecated.Pieces
                         .Where(piece => piece.CompareTag(GameBoard_DEPRECATED.PlayerTag)))
            {
                foreach (var other in boardDeprecated.Pieces
                             .Where(other => !other.CompareTag(GameBoard_DEPRECATED.PlayerTag))
                             .Where(other => playerPiece.Position == other.Position))
                {
                    other.CollideWith(playerPiece);
                }
            }
        }

        private GameBoard_DEPRECATED boardDeprecated;
    }
}