using static Subsystems.LocatorUtilities;
using UnityEngine;

namespace Subsystems
{
    /**
     * <summary>
     * <para>A static interface for querying game subsystems.</para>
     * 
     * <para>
     * In order to query a game subsystem, it must be attached to an object with a Game
     * Subsystem Locator.
     * </para>
     * </summary>
     * 
     * <remarks>
     * <para>
     * A game subsystem locator may be considered a "Service Locator" that finds the
     * singleton instance of a game subsystem.
     * </para>
     * 
     * <para>
     * If there are multiple game subsystem locators in a scene, their subsystem
     * components will be unioned into one object with one locator. If any of the
     * locators have duplicate subsystems, the configuration of the subsystems
     * will be kept in the order their locators are found. Any excess locators will
     * be destroyed along with their game objects and any subsystems attached.
     * </para>
     * </remarks>
     */
    public class GameSubsystemLocator : SubsystemLocator
    {
        /** Get the singleton instance of a game subsystem if it exists.
         * 
         * <example>
         * <code>
         * var mySubsystem = GameSubsystems.Find&lt;MySubsystem&gt;();
         * // use mySubsystem ...
         * </code>
         * </example>
         */
        public static T Find<T>() where T : GameSubsystem
        {
            return Instance?.GetComponent<T>();
        }

        /** The singleton instance of the game subsystem locator */
        private static GameSubsystemLocator Instance
        {
            get
            {
                if (!instance) { instance = Initialize<GameSubsystemLocator>(); }
                return instance;
            }
            set
            {
                instance = value;
                DontDestroyOnLoad(value.gameObject);
            }
        }
        private static GameSubsystemLocator instance;

        // Other subsystems may depend on the subsystem manager's singleton instance
        // existing on Awake or Enable. In order for that to always be true, we'll
        // need to set the instance before any scene loads
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        private static void BeforeSplashScreen()
        {
            Instance = Initialize<GameSubsystemLocator>();
        }
        private void Awake()
        {
            if (!instance) { instance = this; }
            EnsureUniqueLocator(this, instance);
            
        }
        private void OnDestroy()
        {
            instance = null;
        }
    }
}