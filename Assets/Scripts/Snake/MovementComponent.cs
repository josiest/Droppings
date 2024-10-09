using Board;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Snake
{
    [RequireComponent(typeof(SnakeBody))]
    public class MovementComponent : Tickable
    {
        /** The direction this movement component starts in.
         * Defaults to East. */
        [SerializeField] public CardinalDirection startingDirection = CardinalDirection.East;

        /** The starting position */
        private Vector2Int _startingPosition;

        /** A reference to the action mappings where movement is defined */
        private ActionDefinition _actionMappings;

        /** The current direction this movement component is facing.
         * Defaults to East */
        private CardinalDirection _currentDirection;

        /** A reference to the snake body */
        private SnakeBody _snakeBody;

        private void Awake()
        {
            _actionMappings = new ActionDefinition();
            _currentDirection = startingDirection;
            _snakeBody = GetComponent<SnakeBody>();
            _startingPosition = new Vector2Int(Mathf.FloorToInt(transform.position.x),
                                               Mathf.FloorToInt(transform.position.y));
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

        public override void Tick()
        {
            _snakeBody.MoveInDirection(_currentDirection);
        }

        public void Reset()
        {
            _snakeBody.ResetTo(_startingPosition, Directions.Opposite(startingDirection));
        }

        private void OnDirectionChanged(InputAction.CallbackContext context,
                                        CardinalDirection direction)
        {
            if (context.ReadValue<float>() > 0f &&
                direction != Directions.Opposite(_currentDirection))
            {
                _currentDirection = direction;
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
