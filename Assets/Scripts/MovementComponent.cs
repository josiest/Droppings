using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    /** The direction this movement component starts in.
     * Defaults to East. */
    public CardinalDirection startingDirection = CardinalDirection.East;

    /** The current direction this movement component is facing.
     * Defaults to East */
    private CardinalDirection currentDirection;

    /** The transform of the game object this component is attached to. */
    private Transform objectTransform;

    /** The scale of the object to move by */
    private float scale = 1.0f;

    void Start()
    {
        currentDirection = startingDirection;
        objectTransform = GetComponent<Transform>();
    }

    void Update()
    {
        Vector3 dirVec = Vector3.zero;
        switch (currentDirection)
        {
            case CardinalDirection.East: {
                dirVec = Vector3.right;
                break;
            }
            case CardinalDirection.West: {
                dirVec = Vector3.left;
                break;
            }
            case CardinalDirection.North: {
                dirVec = Vector3.up;
                break;
            }
            case CardinalDirection.South: {
                dirVec = Vector3.down;
                break;
            }
            default: {
                dirVec = Vector3.right;
                break;
            }
        }
        objectTransform.position += dirVec * scale;
    }

    void OnHorizontalInput(float inValue)
    {
        if (inValue < 0.0f) {
            currentDirection = CardinalDirection.West;
        }
        else if (inValue > 0.0f) {
            currentDirection = CardinalDirection.East;
        }
    }

    void OnVerticalInput(float inValue)
    {
        if (inValue < 0.0f) {
            currentDirection = CardinalDirection.North;
        }
        else if (inValue > 0.0f) {
            currentDirection = CardinalDirection.South;
        }
    }
}
