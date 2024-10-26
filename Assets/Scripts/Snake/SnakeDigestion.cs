using System.Collections.Generic;
using System.Linq;
using Board;
using Subsystems;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Snake
{
    [RequireComponent(typeof(SnakeBody))]
    public class SnakeDigestion : MonoBehaviour, ITickable
    {
        /** The max number of frames before a snake lays a dropping after consuming food */
        [SerializeField] public int numDigestionFrames = 3;
        private ActionDefinition _actionMappings;

        /** The current number of frames until a dropping is layed */
        private readonly List<int> _droppingTimers = new();

        /** A reference to the snake body */
        private SnakeBody _snakeBody;

        public void Awake()
        {
            _snakeBody = GetComponent<SnakeBody>();
            SceneSubsystemLocator.Find<TickSystem>()?.AddTickable(this);

            _actionMappings = new ActionDefinition();
            _actionMappings.playerActions.layDropping.performed += OnLayDroppingPressed;
        }

        public void Reset()
        {
            _droppingTimers.Clear();
        }

        public void Tick()
        {
            for (int i = 0; i < _droppingTimers.Count; i++)
            {
                _droppingTimers[i] -= 1;
            }
            if (_droppingTimers.Any(timer => timer <= 0))
            {
                _snakeBody.ShouldLayDropping = true;
            }
            _droppingTimers.RemoveAll(timer => timer <= 0);
        }

        private void OnLayDroppingPressed(InputAction.CallbackContext ctx)
        {
            if (!ctx.ReadValueAsButton() || _droppingTimers.Count == 0
                                         || _droppingTimers[0] <= 0)
            {
                return;
            }
            _droppingTimers.RemoveAt(0);
            _snakeBody.ShouldLayDropping = true;
        }

        public void OnEnable()
        {
            _actionMappings.Enable();
        }
        public void OnDisable()
        {
            _actionMappings.Disable();
        }
        public void Digest()
        {
            _droppingTimers.Add(numDigestionFrames);
        }
    }
}