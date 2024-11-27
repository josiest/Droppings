using UnityEngine;

namespace Board
{
    [CreateAssetMenu]
    public class TickSettings : ScriptableObject
    {
        public const string ResourcePath = "Settings/TickSettings";

        [Tooltip("How many frames to tick within a second?"),
         Range(MinTicksPerSecond, MaxTicksPerSecond)]
        public float ticksPerSecond = 4f;
        
        /** How many seconds are in a tick */
        public float SecondsPerTick => 1f/ticksPerSecond;

        private void OnValidate()
        {
            ticksPerSecond = Mathf.Clamp(ticksPerSecond, MinTicksPerSecond, MaxTicksPerSecond);
        }
        
        private const float MinTicksPerSecond = 0.001f;
        private const float MaxTicksPerSecond = 100f;
    }
}