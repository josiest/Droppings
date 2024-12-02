using System.Collections.Generic;
using System.Linq;
using Board;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Snake
{
    [RequireComponent(typeof(SnakeBody))]
    public class SnakeDigestion : MonoBehaviour, ITickable
    {
        /** The max number of frames before a snake lays a dropping after consuming food */
        [SerializeField] public int numDigestionFrames = 3;
        private ActionDefinition actionMappings;

        /** The current number of frames until a dropping is layed */
        private readonly List<int> droppingTimers = new();

        /** A reference to the snake body */
        private SnakeBody snakeBody;

        private void Awake()
        {
            snakeBody = GetComponent<SnakeBody>();
            actionMappings = new ActionDefinition();
            actionMappings.playerActions.layDropping.performed += OnLayDroppingPressed;
        }
        
        private void Start()
        {
            GameBoardSystem.Find<TickSystem>()?.AddTickable(this);
        }

        public void Reset()
        {
            droppingTimers.Clear();
        }

        public void Tick()
        {
            for (int i = 0; i < droppingTimers.Count; i++)
            {
                droppingTimers[i] -= 1;
            }
            if (droppingTimers.Any(timer => timer <= 0))
            {
                snakeBody.ShouldLayDropping = true;
            }
            droppingTimers.RemoveAll(timer => timer <= 0);
        }

        private void OnLayDroppingPressed(InputAction.CallbackContext ctx)
        {
            if (!ctx.ReadValueAsButton() || droppingTimers.Count == 0
                                         || droppingTimers[0] <= 0)
            {
                return;
            }
            droppingTimers.RemoveAt(0);
            snakeBody.ShouldLayDropping = true;
        }

        public void OnEnable()
        {
            actionMappings?.Enable();
        }
        public void OnDisable()
        {
            actionMappings?.Disable();
        }
        public void Digest()
        {
            droppingTimers.Add(numDigestionFrames);
        }
    }
}