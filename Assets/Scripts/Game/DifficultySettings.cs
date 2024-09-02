using UnityEngine;

namespace Game
{
    [CreateAssetMenu]
    public class DifficultySettings : ScriptableObject
    {
        /** The speed of the game (in frames per second) */
        [Range(1, 100)] public int gameSpeed = 10;

        /** The number of body segments the snake has */
        [Range(1, 100)] public int numSnakeBodySegments = 4;
    }
}
