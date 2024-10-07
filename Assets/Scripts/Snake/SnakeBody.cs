using System.Collections.Generic;
using System.Linq;
using Game;
using UnityEngine;

namespace Snake
{
    /** Represents each of the segments of a snake and handles segment movement */
    public class SnakeBody : MonoBehaviour
    {
        /** The settings used to define this snake */
        [SerializeField] public GameObject bodySegmentPrefab;

        /** The amount of body segments the snake has */
        private const int NumSegments = 4;

        /** Keeps track of each of the body segments */
        private readonly LinkedList<GameObject> _segments = new();

        /** Keeps track of the position of the head */
        private Transform _headTransform;
        
        public GameObject Head => _segments.First.Value;
        public GameObject Tail => _segments.Last.Value;

        public bool CollidesWith(Vector3 pos)
        {
            return _segments.Any(segment =>
            {
                var diff = segment.transform.position - pos;
                return diff.sqrMagnitude < 0.01f;
            });
        }

        public void Awake()
        {
            _headTransform = GetComponent<Transform>();
        }

        public void Start()
        {
            if (bodySegmentPrefab is not null) { return; }
            Debug.LogWarning("Snake Body Segment hasn't been set, using default instead.\n" +
                             "You can change this in the snake body component settings");

            var settings = GameSettings.GetInstance();
            bodySegmentPrefab = settings.snakeWorldSettings.defaultSnakeSegmentPrefab;
        }

        public void PopulateInDirection(CardinalDirection direction)
        {
            _headTransform = GetComponent<Transform>();
            var bodyPosition = _headTransform.position;
            var delta = Directions.AsVector(direction);
            _segments.Clear();
            for (int i = 0; i < NumSegments; i++)
            {
                _segments.AddLast(Instantiate(bodySegmentPrefab,
                                              bodyPosition,
                                              Quaternion.identity,
                                              _headTransform));
                bodyPosition += delta;
            }
        }

        public void ResetTo(Vector3 position, CardinalDirection direction)
        {
            var delta = Directions.AsVector(direction);
            var bodyPosition = position;
            foreach (var segment in _segments)
            {
                segment.transform.position = bodyPosition;
                bodyPosition += delta;
            }
        }

        public void MoveInDirection(CardinalDirection direction)
        {
            if (_segments.Count == 0) { return; }
            var tail = _segments.Last.Value;
            _segments.RemoveLast();
            var head = _segments.First.Value;
            tail.transform.position = head.transform.position + Directions.AsVector(direction);
            _segments.AddFirst(tail);
        }
    }
}
