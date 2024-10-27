using System.Linq;
using UnityEngine;

namespace Subsystems
{
    public static class LocatorUtilities
    {
        /**
         * <summary>
         * Search in the active scene for game subsystem locators and cache the first one found.
         * </summary>
         * <remarks>
         * If multiple locators are found, their subsystem components should be unioned into
         * one object. The configuration of the subsystems on the first locator found will be
         * prioritized. This means that if any locators have duplicate subsystems, only the
         * configuration of the subsystem on the "true" instance will be kept.
         * </remarks>
         */
        public static T Initialize<T>() where T : SubsystemLocator
        {
            var locators = Object.FindObjectsOfType<T>(true);
            if (locators.Length < 1)
            {
                var locatorType = typeof(T);
                var locatorObject = new GameObject(locatorType.Name, locatorType);
                return locatorObject.GetComponent<T>();
            }
            if (locators.Length > 1)
            {
                Debug.LogWarning("Multiple game subsystem managers exist in the active scene. " +
                                 "Using the first one found");
            }
            return locators[0];
        }

        public static void CopyNovelSubsystems<T>(T source, T target) where T : SubsystemLocator
        {
            var novelSubsystems = source.GetComponents<SceneSubsystem>()
                .Where(subsystem => !target.GetComponent(subsystem.GetType()))
                .GroupBy(subsystem => subsystem.GetType())
                .Select(group => group.First());
        
            // Copy each of the novel subsystems that exist onto the true instance
            foreach (var copy in novelSubsystems)
            {
                var subsystemType = copy.GetType();
                var trueComponent = target.gameObject.AddComponent(subsystemType);
                foreach (var field in subsystemType.GetFields())
                {
                    field.SetValue(trueComponent, field.GetValue(copy));
                }
            }
        }

        public static void EnsureUniqueLocator<T>(T other, T truth) where T : SubsystemLocator
        {
            // if other is on a different game object than the true locator
            // copy any unique subsystems on the other locator to the true locator
            // before destroying itself
            if (other.gameObject != truth.gameObject)
            {
                CopyNovelSubsystems(other, truth);
                Object.Destroy(other.gameObject);
            }
            // otherwise if they're not the same, other instance doesn't need to exist
            else if (other != truth)
            {
                Object.Destroy(other);
            }
        }
    }
}