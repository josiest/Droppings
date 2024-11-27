using System.Collections.Generic;
using Board;
using Food;
using Game;
using UnityEngine;

namespace Snake
{
    /** Represents each of the segments of a snake and handles segment movement */
    [RequireComponent(typeof(SnakeDigestion), typeof(MovementComponent))]
    public class SnakeBody : MonoBehaviour
    {
        /** The object used for each snake body segment */
        [SerializeField] public GameObject bodySegmentPrefab;

        /** The object used for each dropping */
        [SerializeField] public GameObject droppingPrefab;

        /** The amount of body segments the snake has */
        [SerializeField] private int numSegments = 4;
        
        public SnakeDigestion Digestion { get; private set; }
        public MovementComponent Movement { get; private set; }

        public bool ShouldLayDropping { get; set;  }
        
        public BoardPiece Head => segments.First.Value;
        public BoardPiece Tail => segments.Last.Value;


        /** Keeps track of each of the body segments */
        private readonly LinkedList<BoardPiece> segments = new();

        private GameBoard_DEPRECATED boardDeprecated;
        
        public static SnakeBody SpawnAt(GameObject snakePrefab, GameBoard_DEPRECATED boardDeprecated,
                                        Vector2Int headPosition, CardinalDirection facingDirection)
        {
            var snake = Instantiate(snakePrefab).GetComponent<SnakeBody>();
            snake.boardDeprecated = boardDeprecated;
            snake.PopulateBody();
            snake.ResetTo(headPosition, facingDirection);
            snake.GetComponent<MovementComponent>().Direction = facingDirection;
            return snake;
        }

        public void Awake()
        {
            if (bodySegmentPrefab is null)
            {
                Debug.LogWarning("Snake Body Segment hasn't been set, using default instead.\n" +
                                 "You can change this in the snake body component settings");

                var settings = GameSettings.GetInstance();
                bodySegmentPrefab = settings.snakeWorldSettings.defaultSnakeSegmentPrefab;
            }
            if (!bodySegmentPrefab.GetComponent<BoardPiece>())
            {
                Debug.LogWarning("Snake Body Segment has no board piece component. Adding one now.");
                bodySegmentPrefab.AddComponent<BoardPiece>();
            }
            if (!droppingPrefab.GetComponent<Dropping>())
            {
                Debug.LogWarning("Snake Body Dropping has no Dropping component. Adding one now.");
                droppingPrefab.AddComponent<Dropping>();
            }

            Digestion = GetComponent<SnakeDigestion>();
            Movement = GetComponent<MovementComponent>();
        }

        public void PopulateBody()
        {
            for (int i = 0; i < numSegments; i++)
            {
                segments.AddLast(boardDeprecated.CreatePiece<BoardPiece>(bodySegmentPrefab, transform));
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

            if (nextPosition.x >= boardDeprecated.Dimensions.xMax)
            {
                nextPosition.x -= boardDeprecated.Dimensions.width;
            }
            else if (nextPosition.x < boardDeprecated.Dimensions.xMin)
            {
                nextPosition.x += boardDeprecated.Dimensions.width;
            }

            if (nextPosition.y >= boardDeprecated.Dimensions.yMax)
            {
                nextPosition.y -= boardDeprecated.Dimensions.height;
            }
            else if (nextPosition.y < boardDeprecated.Dimensions.yMin)
            {
                nextPosition.y += boardDeprecated.Dimensions.height;
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
            boardDeprecated?.CreatePiece<Dropping>(droppingPrefab, position);
        }
    }
}
