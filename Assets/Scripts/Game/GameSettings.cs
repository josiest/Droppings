using UnityEngine;

/** Various settings and defaults that configure the game */
[CreateAssetMenu]
public class GameSettings : ScriptableObject
{
    public static GameSettings GetInstance()
    {
        return _instance ??= CreateDefault();
    }

    public static GameSettings CreateDefault()
    {
        _instance = CreateInstance<GameSettings>();
        _instance.defaultDifficulty = CreateInstance<DifficultySettings>();
        _instance.snakeWorldSettings = CreateInstance<SnakeWorldSettings>();
        return _instance;
    }
    public static void SetInstance(GameSettings instance) { _instance = instance; }
    
    private static GameSettings _instance;

    /** The default difficulty to start the game as */
    public DifficultySettings defaultDifficulty;
    
    /** The default snake world settings */
    public SnakeWorldSettings snakeWorldSettings;

}
