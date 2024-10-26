using UnityEngine;

namespace Scene
{
    public class SceneSubsystems : MonoBehaviour
    {
        private static SceneSubsystems instance;
        public static SceneSubsystems Instance
        {
            get
            {
                if (instance) { return instance; }
                var sceneSubsystemManagers = FindObjectsOfType<SceneSubsystems>();
                if (sceneSubsystemManagers.Length < 1)
                {
                    Debug.LogError("No scene subsystem managers currently exist");
                    return null;
                }
                if (sceneSubsystemManagers.Length > 1)
                {
                    Debug.LogWarning("Multiple scene subsystem managers exist. Using the first one");
                }
                instance = sceneSubsystemManagers[0];
                return instance;
            }
        }

        public static T Find<T>() where T : SceneSubsystem
        {
            return Instance?.GetComponent<T>();
        }

        public void OnEnable()
        {
            if (instance == null) { instance = this; }
        }
        public void OnDisable()
        {
            if (instance == this) { instance = null; }
        }
    }
}