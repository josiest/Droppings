using UnityEngine;

public class GameManager : MonoBehaviour
{
    /** The settings for the game. */
    public GameSettings gameSettings;

    private void Start()
    {
        if (gameSettings is null)
        {
            Debug.LogWarning("Game settings hasn't been set, please set it in the inspector. " +
                             "Using default settings instead.");
            gameSettings = GameSettings.CreateDefault();
        }
        Application.targetFrameRate = gameSettings.defaultDifficulty.gameSpeed;
    }
}
