using System.Collections.Generic;
using System.Linq;
using Board;
using Subsystems;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Snake
{
    [RequireComponent(typeof(SnakeBody))]
    public class MovementComponent : MonoBehaviour, ITickable
    {
        /** The current direction this movement component is facing. Defaults to East */
        public CardinalDirection Direction { get; set; }

        /** A reference to the action mappings where movement is defined */
        private ActionDefinition actionMappings;

        /** A reference to the snake body */
        private SnakeBody snakeBody;

        /** A queue of directions to change in */
        private readonly Queue<CardinalDirection> directionQueue = new();

        private void Awake()
        {
            actionMappings = new ActionDefinition();
            snakeBody = GetComponent<SnakeBody>();
        }

        private void Start()
        {
            SceneSubsystemLocator.Find<TickSystem>()?.AddTickable(this);
            if (actionMappings is null) { return; }
            actionMappings.playerMovement.up.performed +=
                ctx => OnDirectionChanged(ctx, CardinalDirection.North);
            actionMappings.playerMovement.down.performed +=
                ctx => OnDirectionChanged(ctx, CardinalDirection.South);
            actionMappings.playerMovement.left.performed +=
                ctx => OnDirectionChanged(ctx, CardinalDirection.West);
            actionMappings.playerMovement.right.performed +=
                ctx => OnDirectionChanged(ctx, CardinalDirection.East);
        }

        public void Tick()
        {
            if (directionQueue.Count > 0)
            {
                Direction = directionQueue.Dequeue();
            }
            snakeBody.MoveInDirection(Direction);
        }

        private void OnDirectionChanged(InputAction.CallbackContext context,
                                        CardinalDirection newDirection)
        {
            var hasChangedDirection = context.ReadValue<float>() > 0f;
            var lastDirection = directionQueue.Count > 0 ? directionQueue.Last() : Direction;

            if (hasChangedDirection && newDirection != Directions.Opposite(lastDirection)
                                    && directionQueue.Count < 2)
            {
                directionQueue.Enqueue(newDirection);
            }
        }

        private void OnEnable()
        {
            actionMappings.playerMovement.Enable();
        }
        private void OnDisable()
        {
            actionMappings.playerMovement.Disable();
        }

        public void Reset()
        {
            directionQueue.Clear();
        }
    }
}
