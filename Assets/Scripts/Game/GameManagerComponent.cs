using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        /** The settings for the game. */
        [SerializeField] public GameSettings gameSettings;
    
        /** The scene to load once the core scene is ready */
        [SerializeField] public SceneField sceneToLoad;

        public static GameManager GetInstance() { return _instance; }
        private static GameManager _instance;

        private void Start()
        {
            _instance = this;
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
            Application.targetFrameRate = gameSettings.difficultySetting.gameSpeed;
            if (sceneToLoad is not null)
            {
                SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Additive);
            }
        }
    }
}
