using System.Collections.Generic;
using System.Linq;
using Subsystems;
using UnityEngine;

namespace Board
{
    public class TickSystem : SceneSubsystem
    {
        /** How many frames per second to tick with */
        [SerializeField] private float ticksPerSecond = 4f;
        public float SecondsPerTick => 1f/ticksPerSecond;
        private float currentTickTimer;
        private const float MinTickFrequency = 0.0001f;

        private readonly HashSet<ITickable> tickables = new();

        public void Awake()
        {
            tickables.UnionWith(FindObjectsOfType<MonoBehaviour>().OfType<ITickable>());
        }

        public void Start()
        {
            ticksPerSecond = Mathf.Max(MinTickFrequency, ticksPerSecond);
            currentTickTimer = SecondsPerTick;
        }

        public void Update()
        {
            currentTickTimer -= Time.deltaTime;
            while (currentTickTimer <= 0f)
            {
                foreach (var tickable in tickables) { tickable.Tick(); }
                currentTickTimer += SecondsPerTick;
            }
        }

        public void AddTickable(ITickable tickable)
        {
            tickables.Add(tickable);
        }
    }
}