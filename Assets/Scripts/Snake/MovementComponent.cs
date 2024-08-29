using UnityEngine;
using UnityEngine.InputSystem;

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

    /** The transform of the game object this component is attached to. */
    private Transform _objectTransform;

    /** The scale of the object to move by */
    private float _unitSize = 1.0f;

    private void Awake()
    {
        _actionMappings = new ActionDefinition();
    }

    private void Start()
    {
        _currentDirection = startingDirection;
        _objectTransform = GetComponent<Transform>();
        _unitSize = GameSettings.GetInstance().snakeWorldSettings.unitSize;
        if (_actionMappings is not null)
        {
            _actionMappings.playerMovement.up.performed +=
                ctx => OnDirectionChanged(ctx, CardinalDirection.North);
            _actionMappings.playerMovement.down.performed +=
                ctx => OnDirectionChanged(ctx, CardinalDirection.South);
            _actionMappings.playerMovement.left.performed +=
                ctx => OnDirectionChanged(ctx, CardinalDirection.West);
            _actionMappings.playerMovement.right.performed +=
                ctx => OnDirectionChanged(ctx, CardinalDirection.East);
        }
    }

    private void Update()
    {
        var dirVec = _currentDirection switch
        {
            CardinalDirection.East => Vector3.right,
            CardinalDirection.West => Vector3.left,
            CardinalDirection.North => Vector3.up,
            CardinalDirection.South => Vector3.down,
            _ => Vector3.right
        };
        _objectTransform.position += dirVec * _unitSize;
    }

    private void OnDirectionChanged(InputAction.CallbackContext context, CardinalDirection direction)
    {
        if (context.ReadValue<float>() > 0f) { _currentDirection = direction; }
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
