using System.Collections.Generic;
using System.Linq;
using Board;
using Scene;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Snake
{
    [RequireComponent(typeof(SnakeBody))]
    public class MovementComponent : MonoBehaviour, ITickable
    {
        /** A reference to the action mappings where movement is defined */
        private ActionDefinition _actionMappings;

        /** The current direction this movement component is facing.
         * Defaults to East */
        public CardinalDirection Direction { get; set; }

        /** A reference to the snake body */
        private SnakeBody _snakeBody;

        /** A queue of directions to change in */
        private readonly Queue<CardinalDirection> _directionQueue = new();

        private void Awake()
        {
            _actionMappings = new ActionDefinition();
            _snakeBody = GetComponent<SnakeBody>();
            SceneSubsystems.Find<TickSystem>()?.AddTickable(this);
        }

        private void Start()
        {
            if (_actionMappings is null) { return; }
            _actionMappings.playerMovement.up.performed +=
                ctx => OnDirectionChanged(ctx, CardinalDirection.North);
            _actionMappings.playerMovement.down.performed +=
                ctx => OnDirectionChanged(ctx, CardinalDirection.South);
            _actionMappings.playerMovement.left.performed +=
                ctx => OnDirectionChanged(ctx, CardinalDirection.West);
            _actionMappings.playerMovement.right.performed +=
                ctx => OnDirectionChanged(ctx, CardinalDirection.East);
        }

        public void Tick()
        {
            if (_directionQueue.Count > 0)
            {
                Direction = _directionQueue.Dequeue();
            }
            _snakeBody.MoveInDirection(Direction);
        }

        private void OnDirectionChanged(InputAction.CallbackContext context,
                                        CardinalDirection newDirection)
        {
            var hasChangedDirection = context.ReadValue<float>() > 0f;
            var lastDirection = _directionQueue.Count > 0 ? _directionQueue.Last() : Direction;

            if (hasChangedDirection && newDirection != Directions.Opposite(lastDirection)
                                    && _directionQueue.Count < 2)
            {
                _directionQueue.Enqueue(newDirection);
            }
        }

        private void OnEnable()
        {
            _actionMappings.playerMovement.Enable();
        }
        private void OnDisable()
        {
            _actionMappings.playerMovement.Disable();
        }

        public void Reset()
        {
            _directionQueue.Clear();
        }
    }
}
