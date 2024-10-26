using UnityEngine;

namespace Subsystems
{
    /**
     * <summary>
     * <para>A scene subsystem is a singleton class that persists for the lifetime of a scene.</para>
     *
     * <para>
     * A scene subsystem can be queried using
     * <see cref="SceneSubsystemLocator.Find{T}">SceneSubsystemLocator.Find</see>,
     * but it must be attached to a game object with a scene subsystem locator
     * </para>
     * </summary>
     *
     * <example>
     * <code>
     * public class MyBehavior : MonoBehavior
     * {
     *     private void Start()
     *     {
     *         var mySceneSubsystem = SceneSubsystems.Find&lt;MySceneSubsystem&gt;();
     *         // ... use mySceneSubsystem ...
     *     }
     * }
     * </code>
     * </example>
     */
    [RequireComponent(typeof(SceneSubsystemLocator))]
    public abstract class SceneSubsystem : MonoBehaviour
    {
    }
}