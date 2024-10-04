using UnityEngine;
using UnityEngine.InputSystem;

namespace Snake
{
    public class MovementComponent : MonoBehaviour
    {
        /** The direction this movement component starts in.
         * Defaults to East. */
        [SerializeField] public CardinalDirection startingDirection = CardinalDirection.East;

        /** A reference to the action mappings where movement is defined */
        private ActionDefinition _actionMappings;

        /** The current direction this movement component is facing.
         * Defaults to East */
        private CardinalDirection _currentDirection;

        /** A reference to the snake body */
        private SnakeBodyComponent _snakeBody;

        private void Awake()
        {
            _actionMappings = new ActionDefinition();
            _currentDirection = startingDirection;
            _snakeBody = GetComponent<SnakeBodyComponent>();
        }

        private void Start()
        {
            //int numSegments = gameManager.gameSettings.difficultySetting.numSnakeBodySegments;
            _snakeBody.PopulateInDirection(Directions.Opposite(startingDirection));

            if (_actionMappings is null) { return; }
            _actionMappings.playerMovement.up.performed +=
                Ctx => OnDirectionChanged(Ctx, CardinalDirection.North);
            _actionMappings.playerMovement.down.performed +=
                Ctx => OnDirectionChanged(Ctx, CardinalDirection.South);
            _actionMappings.playerMovement.left.performed +=
                Ctx => OnDirectionChanged(Ctx, CardinalDirection.West);
            _actionMappings.playerMovement.right.performed +=
                Ctx => OnDirectionChanged(Ctx, CardinalDirection.East);
        }

        private void Update()
        {
            _snakeBody.MoveInDirection(_currentDirection);
        }

        private void OnDirectionChanged(InputAction.CallbackContext Context, CardinalDirection Direction)
        {
            if (Context.ReadValue<float>() > 0f) { _currentDirection = Direction; }
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
