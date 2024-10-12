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
            _snakeBody.MoveInDirection(Direction);
        }

        private void OnDirectionChanged(InputAction.CallbackContext context,
                                        CardinalDirection direction)
        {
            if (context.ReadValue<float>() > 0f &&
                direction != Directions.Opposite(Direction))
            {
                Direction = direction;
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
    }
}
