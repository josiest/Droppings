using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Board
{
    public class TickSystem : GameBoardSubsystem
    {
        //
        // Configuration
        //

        /** How many frames per second to tick with */
        private float ticksPerSecond = 4f;

        //
        // Public Interface
        //

        public float SecondsPerTick => 1f/ticksPerSecond;

        public void Pause()
        {
            isPaused = true;
            enabled = false;
        }

        public void Resume()
        {
            isPaused = false;
            enabled = true;
        }
                
        public void AddTickable(ITickable tickable)
        {
            tickables.Add(tickable);
        }
        
        //
        // Internal Interface
        //

        private float currentTickTimer;
        private const float MinTickFrequency = 0.0001f;
        private readonly HashSet<ITickable> tickables = new();

        //
        // Unity Events
        //

        public void Awake()
        {
            var settings = Resources.Load<TickSettings>(TickSettings.ResourcePath);
            if (!settings)
            {
                settings = ScriptableObject.CreateInstance<TickSettings>();
                Debug.LogWarning($"Unable to load tick settings from {TickSettings.ResourcePath}, " +
                                  "using default instead.");
            }
            ticksPerSecond = settings.ticksPerSecond;
            tickables.UnionWith(FindObjectsOfType<MonoBehaviour>().OfType<ITickable>());
        }

        public void Start()
        {
            ticksPerSecond = Mathf.Max(MinTickFrequency, ticksPerSecond);
            currentTickTimer = SecondsPerTick;
        }

        public void Update()
        {
            if (isPaused) { return; }
            currentTickTimer -= Time.deltaTime;
            while (currentTickTimer <= 0f)
            {
                foreach (var tickable in tickables)
                {
                    tickable.Tick();
                    if (isPaused) { return; }
                }
                currentTickTimer += SecondsPerTick;
            }
        }

        private bool isPaused;
    }
}