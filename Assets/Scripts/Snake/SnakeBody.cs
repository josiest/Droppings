using System.Collections.Generic;
using Board;
using Food;
using UnityEngine;

namespace Snake
{
    /** Represents each of the segments of a snake and handles segment movement */
    [RequireComponent(typeof(SnakeDigestion), typeof(MovementComponent))]
    public class SnakeBody : MonoBehaviour
    {
        /** The object used for each snake body segment */
        [SerializeField] public SnakeBodySegment bodySegmentPrefab;

        /** The object used for each dropping */
        [SerializeField] public Dropping droppingPrefab;

        /** The amount of body segments the snake has */
        [SerializeField] private int numSegments = 4;
        
        public SnakeDigestion Digestion { get; private set; }
        public MovementComponent Movement { get; private set; }

        public bool ShouldLayDropping { get; set;  }
        
        public BoardPiece Head => segments.First.Value;
        public BoardPiece Tail => segments.Last.Value;


        /** Keeps track of each of the body segments */
        private readonly LinkedList<BoardPiece> segments = new();
        private GameBoard board;
        
        public static SnakeBody SpawnAt(GameObject snakePrefab, GameBoard board,
                                        Vector2Int headPosition, CardinalDirection facingDirection)
        {
            var snake = Instantiate(snakePrefab).GetComponent<SnakeBody>();
            snake.board = board;
            snake.PopulateBody();
            snake.ResetTo(headPosition, facingDirection);
            snake.GetComponent<MovementComponent>().Direction = facingDirection;
            return snake;
        }

        public void Awake()
        {
            Digestion = GetComponent<SnakeDigestion>();
            Movement = GetComponent<MovementComponent>();
        }

        public void PopulateBody()
        {
            for (int i = 0; i < numSegments; i++)
            {
                segments.AddLast(board.CreatePiece<BoardPiece>(bodySegmentPrefab.gameObject, transform));
            }
        }

        public void ResetTo(Vector2Int headPosition, CardinalDirection facingDirection)
        {
            ShouldLayDropping = false;
            Movement.Reset();
            Digestion.Reset();

            var delta = Directions.AsVector2Int(Directions.Opposite(facingDirection));
            var bodyPosition = headPosition;
            foreach (var segment in segments)
            {
                segment.Position = bodyPosition;
                bodyPosition += delta;
            }
            Movement.Direction = facingDirection;
        }

        public void MoveInDirection(CardinalDirection direction)
        {
            if (segments.Count == 0) { return; }

            // Pop the tail
            var tail = Tail;
            var tailPosition = Tail.GetComponent<BoardPiece>().Position;
            segments.RemoveLast();

            // move the tail to the head + next direction
            var nextPosition = Head.GetComponent<BoardPiece>().Position +
                               Directions.AsVector2Int(direction);

            if (nextPosition.x >= board.Dimensions.xMax)
            {
                nextPosition.x -= board.Dimensions.width;
            }
            else if (nextPosition.x < board.Dimensions.xMin)
            {
                nextPosition.x += board.Dimensions.width;
            }

            if (nextPosition.y >= board.Dimensions.yMax)
            {
                nextPosition.y -= board.Dimensions.height;
            }
            else if (nextPosition.y < board.Dimensions.yMin)
            {
                nextPosition.y += board.Dimensions.height;
            }

            tail.GetComponent<BoardPiece>().Position = nextPosition;
            
            // Add the tail back to the front of the segment list as the new head
            segments.AddFirst(tail);
            
            // Finally lay a dropping if needed
            if (ShouldLayDropping)
            {
                LayDropping(tailPosition);
                ShouldLayDropping = false;
            }
        }

        private void LayDropping(Vector2Int position)
        {
            board?.CreatePiece<Dropping>(droppingPrefab.gameObject, position);
        }
    }
}
