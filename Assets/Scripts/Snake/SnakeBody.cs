using System.Collections.Generic;
using System.Linq;
using Board;
using Game;
using UnityEngine;

namespace Snake
{
    /** Represents each of the segments of a snake and handles segment movement */
    public class SnakeBody : MonoBehaviour
    {
        /** The settings used to define this snake */
        [SerializeField] public GameObject bodySegmentPrefab;

        public bool ShouldLayDropping { get; set;  }

        public delegate void OnLayDropping(Vector2Int pos);
        public OnLayDropping LayDropping;

        /** The amount of body segments the snake has */
        private const int NumSegments = 4;

        /** Keeps track of each of the body segments */
        private readonly LinkedList<GameObject> _segments = new();
        
        public GameObject Head => _segments.First.Value;
        public GameObject Tail => _segments.Last.Value;

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
        }

        public bool CollidesWith(Vector2Int pos)
        {
            return _segments.Any(segment => segment.GetComponent<BoardPiece>().Position == pos);
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
            var bodyPosition = Vector2Int.zero;
            var delta = Directions.AsVector2Int(direction);
            _segments.Clear();
            for (int i = 0; i < NumSegments; i++)
            {
                var segment = Instantiate(bodySegmentPrefab, transform);
                segment.GetComponent<BoardPiece>().Position = bodyPosition;
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
                segment.GetComponent<BoardPiece>().Position = bodyPosition;
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
    }
}
