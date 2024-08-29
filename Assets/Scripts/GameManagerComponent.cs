using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /** The settings for the game. */
    [SerializeField] public GameSettings gameSettings;
    
    /** The scene to load once the core scene is ready */
    [SerializeField] public SceneField sceneToLoad;

    private void Start()
    {
        if (gameSettings is null)
        {
            Debug.LogWarning("Game settings hasn't been set, please set it in the inspector. " +
                             "Using default settings instead.");
            gameSettings = GameSettings.CreateDefault();
        }
        else
        {
            GameSettings.SetInstance(gameSettings);
        }
        Application.targetFrameRate = gameSettings.defaultDifficulty.gameSpeed;
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Additive);
    }
}
