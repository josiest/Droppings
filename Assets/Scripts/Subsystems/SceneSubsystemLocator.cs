using UnityEngine;

namespace Subsystems
{
    /** A static interface for querying scene subsystems. */
    public class SceneSubsystemLocator : MonoBehaviour
    {
        /** Get the singleton instance of a scene subsystem if it exists.
         *
         * <example>
         * <code>
         * var mySubsystem = SceneSubsystemLocator.Find&lt;MySubsystem&gt;();
         * // use mySubsystem ...
         * </code>
         * </example>
         */
        public static T Find<T>() where T : SceneSubsystem
        {
            return Instance?.GetComponent<T>();
        }

        /** The singleton instance */
        private static SceneSubsystemLocator Instance
        {
            get
            {
                if (!instance) { Initialize(); }
                return instance;
            }
        }
        private static SceneSubsystemLocator instance;

        /** Create a new singleton instance if it hasn't already been set */
        private static void Initialize()
        {
            if (instance) { return; }
            var sceneSystemManagers = FindObjectsOfType<SceneSubsystemLocator>(true);
            if (sceneSystemManagers.Length < 1)
            {
                var subsystemManager = new GameObject("Scene Subsystem Locator",
                                                      typeof(SceneSubsystemLocator));
                instance = subsystemManager.GetComponent<SceneSubsystemLocator>();           
            }
            else
            {
                if (sceneSystemManagers.Length > 1)
                {
                    Debug.LogWarning("Multiple scene subsystem managers exist in the active scene. " +
                                     "Using the first one found.");
                }
                instance = sceneSystemManagers[0];
                // TODO: ensure only one instance of scene subsystems exist
            }
        }
        // Other subsystems may depend on the subsystem manager's singleton instance
        // existing on Awake or Enable. In order for that to always be true, we'll
        // need to set the instance before any scene loads
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void OnBeforeSceneLoad()
        {
            Initialize();
        }
        public void OnDestroy()
        {
            if (instance == this) { instance = null; }
        }

    }
}