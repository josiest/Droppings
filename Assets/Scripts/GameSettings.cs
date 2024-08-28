using UnityEngine;

/** Various settings and defaults that configure the game */
[CreateAssetMenu]
public class GameSettings : ScriptableObject
{
    /** The default difficulty to start the game as */
    public DifficultySettings defaultDifficulty;

    public static GameSettings CreateDefault()
    {
        var settings = CreateInstance<GameSettings>();
        settings.defaultDifficulty = CreateInstance<DifficultySettings>();
        return settings;
    }
}
