using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    /** The direction this movement component starts in.
     * Defaults to East. */
    public CardinalDirection startingDirection = CardinalDirection.East;

    /** The current direction this movement component is facing.
     * Defaults to East */
    private CardinalDirection _currentDirection;

    /** The transform of the game object this component is attached to. */
    private Transform _objectTransform;

    /** The scale of the object to move by */
    private float _unitSize = 1.0f;

    private void Start()
    {
        _currentDirection = startingDirection;
        _objectTransform = GetComponent<Transform>();
        _unitSize = GameSettings.GetInstance().snakeWorldSettings.unitSize;
        Debug.Log($"Using unit size {_unitSize}");
    }

    void Update()
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

    void OnHorizontalInput(float inValue)
    {
        if (inValue < 0.0f) {
            _currentDirection = CardinalDirection.West;
        }
        else if (inValue > 0.0f) {
            _currentDirection = CardinalDirection.East;
        }
    }

    void OnVerticalInput(float inValue)
    {
        if (inValue < 0.0f) {
            _currentDirection = CardinalDirection.North;
        }
        else if (inValue > 0.0f) {
            _currentDirection = CardinalDirection.South;
        }
    }
}
