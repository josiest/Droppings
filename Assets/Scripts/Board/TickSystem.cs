using System.Collections.Generic;
using Scene;
using UnityEngine;

namespace Board
{
    public class TickSystem : SceneSubsystem
    {
        /** How many frames per second to tick with */
        [SerializeField] private float ticksPerSecond = 4f;
        public float SecondsPerTick => 1f/ticksPerSecond;
        private float _currentTickTimer;
        private const float MinTickFrequency = 0.0001f;

        private readonly List<ITickable> _tickables = new();
        
        public void Start()
        {
            ticksPerSecond = Mathf.Max(MinTickFrequency, ticksPerSecond);
            _currentTickTimer = SecondsPerTick;
        }

        public void Update()
        {
            _currentTickTimer -= Time.deltaTime;
            while (_currentTickTimer <= 0f)
            {
                _tickables.ForEach(tickable => tickable.Tick());
                _currentTickTimer += SecondsPerTick;
            }
        }

        public void AddTickable(ITickable tickable)
        {
            _tickables.Add(tickable);
        }
    }
}