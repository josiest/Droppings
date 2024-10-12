using System.Collections.Generic;
using System.Linq;
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

        public bool ShouldLayDropping { get; set;  }

        /** The amount of body segments the snake has */
        private const int NumSegments = 4;

        /** Keeps track of each of the body segments */
        private readonly LinkedList<BoardPiece> _segments = new();
        
        public BoardPiece Head => _segments.First.Value;
        public BoardPiece Tail => _segments.Last.Value;

        public void Start()
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
        }

        public void AddToBoard(GameBoard board)
        {
            foreach (var piece in _segments.Select(segment => segment.GetComponent<BoardPiece>()))
            {
                board.AddPiece(piece);
            }
        }

        public void PopulateInDirection(CardinalDirection direction)
        {
            var board = GameBoard.Instance;
            if (!board) { return; }

            var bodyPosition = Vector2Int.zero;
            var delta = Directions.AsVector2Int(direction);
            for (int i = 0; i < NumSegments; i++)
            {
                var segment = board.CreatePiece<BoardPiece>(bodySegmentPrefab, bodyPosition, transform);
                _segments.AddLast(segment);
                bodyPosition += delta;
            }
        }

        public void ResetTo(Vector2Int position, CardinalDirection direction)
        {
            var delta = Directions.AsVector2Int(direction);
            var bodyPosition = position;
            foreach (var segment in _segments)
            {
                segment.Position = bodyPosition;
                bodyPosition += delta;
            }
        }

        public void MoveInDirection(CardinalDirection direction)
        {
            if (_segments.Count == 0) { return; }

            // Pop the tail
            var tail = Tail;
            var tailPosition = Tail.GetComponent<BoardPiece>().Position;
            _segments.RemoveLast();

            // move the tail to the head + next direction
            var headPosition = Head.GetComponent<BoardPiece>().Position;
            tail.GetComponent<BoardPiece>().Position = headPosition + Directions.AsVector2Int(direction);
            
            // Add the tail back to the front of the segment list as the new head
            _segments.AddFirst(tail);
            
            // Finally lay a dropping if needed
            if (ShouldLayDropping)
            {
                LayDropping(tailPosition);
                ShouldLayDropping = false;
            }
        }

        private void LayDropping(Vector2Int position)
        {
            var board = GameBoard.Instance;
            if (board) { board.CreatePiece<Dropping>(droppingPrefab, position); }
        }
    }
}
