using UnityEngine;

[CreateAssetMenu]
public class DifficultySettings : ScriptableObject
{
    /** The speed of the game (in frames per second) */
    [Range(1, 100)]
    public int gameSpeed = 10;
}
