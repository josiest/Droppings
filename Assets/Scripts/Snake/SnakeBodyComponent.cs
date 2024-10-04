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
        private int numSegments = 4;
        
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

            var settings = GameSettings.GetInstance();
            bodySegmentPrefab = settings.snakeWorldSettings.defaultSnakeSegmentPrefab;
        }

        public void PopulateInDirection(CardinalDirection direction)
        {
            _headTransform = GetComponent<Transform>();
            var bodyPosition = _headTransform.position;
            var delta = Directions.AsVector(direction);
            _segments.Clear();
            for (int i = 0; i < numSegments; i++)
            {
                _segments.AddLast(Instantiate(bodySegmentPrefab, bodyPosition, Quaternion.identity));
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
