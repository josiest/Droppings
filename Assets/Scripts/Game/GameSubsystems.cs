using UnityEngine;

namespace Game
{
    public class GameSubsystems : MonoBehaviour
    {
        private static GameSubsystems instance;
        public static GameSubsystems Instance
        {
            get
            {
                if (instance) { return instance; }
                var gameSubsystemManagers = FindObjectsOfType<GameSubsystems>();
                if (gameSubsystemManagers.Length < 1)
                {
                    Debug.LogError("No game subsystem managers currently exist");
                    return null;
                }

                if (gameSubsystemManagers.Length > 1)
                {
                    Debug.LogWarning("Multiple game subsystem managers exist. Using the first one");
                }
                Instance = gameSubsystemManagers[0];
                return instance;
            }
            private set
            {
                instance = value;
                DontDestroyOnLoad(value.gameObject);
            }
        }
        public static T Find<T>() where T : GameSubsystem
        {
            return Instance?.GetComponent<T>();
        }
        public void OnEnable()
        {
            if (!instance) { Instance = this; }
        }
        public void OnDisable()
        {
            if (instance == this) { instance = null; }
        }
    }
}