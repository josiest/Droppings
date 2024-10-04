using System.Collections.Generic;
using Game;
using UnityEngine;

namespace Snake
{
    /** Represents each of the segments of a snake and handles segment movement */
    public class SnakeBodyComponent : MonoBehaviour
    {
        /** The settings used to define this snake */
        public GameObject bodySegmentPrefab;

        /** The amount of body segments the snake has */
        private const int NumSegments = 4;

        /** Keeps track of each of the body segments */
        private readonly LinkedList<GameObject> _segments = new();

        /** Keeps track of the position of the head */
        private Transform _headTransform;

        private void Awake()
        {
            _headTransform = GetComponent<Transform>();
            
            if (bodySegmentPrefab is not null) { return; }
            Debug.LogWarning("Snake Body Segment hasn't been set, using default instead.\n" +
                             "You can change this in the snake body component settings");

            var Settings = GameSettings.GetInstance();
            bodySegmentPrefab = Settings.snakeWorldSettings.defaultSnakeSegmentPrefab;
        }

        public void PopulateInDirection(CardinalDirection Direction)
        {
            _headTransform = GetComponent<Transform>();
            var BodyPosition = _headTransform.position;
            var Delta = Directions.AsVector(Direction);
            _segments.Clear();
            for (int I = 0; I < NumSegments; I++)
            {
                _segments.AddLast(Instantiate(bodySegmentPrefab, BodyPosition, Quaternion.identity));
                BodyPosition += Delta;
            }
        }
        public void MoveInDirection(CardinalDirection Direction)
        {
            if (_segments.Count == 0) { return; }
            var Tail = _segments.Last.Value;
            _segments.RemoveLast();
            var Head = _segments.First.Value;
            Tail.transform.position = Head.transform.position + Directions.AsVector(Direction);
            _segments.AddFirst(Tail);
        }
    }
}
