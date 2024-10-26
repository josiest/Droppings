using UnityEngine;

namespace Subsystems
{
    /**
     * <summary>
     * <para>A game subsystem is a singleton class that persists for the lifetime of a game.</para>
     *
     * <para>
     * A game subsystem can be queried using
     * <see cref="GameSubsystemLocator.Find{T}">GameSubsystemLocator.Find</see>,
     * but the subsystem must be attached to an object with a GameSubsystemLocator
     * </para>
     * </summary>
     *
     * <example>
     * <code>
     * public class MyGameSubsystem : GameSubsystem
     * {
     *     [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
     *     private static void OnBeforeSceneLoad()
     *     {
     *         GameSubsystems.Register&lt;MyGameSubsystem&gt;();
     *     }
     *     // ...
     * }
     * </code>
     * <code>
     * public class MyBehavior : MonoBehavior
     * {
     *     private void Start()
     *     {
     *         var myGameSubsystem = GameSubsystems.Find&lt;MyGameSubsystem&gt;();
     *         // ... use myGameSubsystem ...
     *     }
     * }
     * </code>
     * </example>
     */
    [RequireComponent(typeof(GameSubsystemLocator))]
    public abstract class GameSubsystem : MonoBehaviour
    {
    }
}