using static Subsystems.LocatorUtilities;
using UnityEngine;

namespace Subsystems
{
    /** A static interface for querying scene subsystems. */
    public class SceneSubsystemLocator : SubsystemLocator
    {
        //
        // Public Interface
        //

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
 
        // 
        // Unity Events
        // 

        // Other subsystems may depend on the subsystem manager's singleton instance
        // existing on Awake or Enable. In order for that to always be true, we'll
        // need to set the instance before any scene loads
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void BeforeSceneLoad()
        {
            instance = Initialize<SceneSubsystemLocator>();
        }
        // Whenever a locator is loaded, verify that it's the true instance
        // or copy its unique subsystems onto the true instance then destroy itself
        private void Awake()
        {
            if (!instance) { instance = this; }
            EnsureUniqueLocator(this, instance);
        }
        private void OnDestroy()
        {
            if (instance == this) { instance = null; }
        }
       
        //
        // Internal Interface
        // 

        /** The singleton instance */
        private static SceneSubsystemLocator Instance
        {
            get
            {
                if (!instance) { instance = Initialize<SceneSubsystemLocator>(); }
                return instance;
            }
        }
        private static SceneSubsystemLocator instance;
    }
}