using UnityEngine;

namespace Snake
{
    /** The four cardinal directions */
    public enum CardinalDirection
    {
        North, East, South, West
    };

    public static class Directions
    {
        public static Vector3 AsVector3(CardinalDirection direction)
        {
            return direction switch
            {
                CardinalDirection.East => Vector3.right,
                CardinalDirection.West => Vector3.left,
                CardinalDirection.North => Vector3.up,
                CardinalDirection.South => Vector3.down,
                _ => Vector3.right
            };
        }

        public static Vector2Int AsVector2Int(CardinalDirection direction)
        {
            return direction switch
            {
                CardinalDirection.East => Vector2Int.right,
                CardinalDirection.West => Vector2Int.left,
                CardinalDirection.North => Vector2Int.up,
                CardinalDirection.South => Vector2Int.down,
                _ => Vector2Int.right
            };
        }

        public static CardinalDirection Opposite(CardinalDirection direction)
        {
            return direction switch
            {
                CardinalDirection.East => CardinalDirection.West,
                CardinalDirection.West => CardinalDirection.East,
                CardinalDirection.North => CardinalDirection.South,
                CardinalDirection.South => CardinalDirection.North,
                _ => CardinalDirection.East
            };
        }
    }

}
