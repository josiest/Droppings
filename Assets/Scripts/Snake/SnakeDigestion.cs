using System;
using Board;
using Scene;
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
        private int _droppingTimer = -1;

        /** A reference to the snake body */
        private SnakeBody _snakeBody;

        public void Awake()
        {
            _snakeBody = GetComponent<SnakeBody>();
            SceneSubsystems.Find<TickSystem>()?.AddTickable(this);

            _actionMappings = new ActionDefinition();
            _actionMappings.playerActions.layDropping.performed += OnLayDroppingPressed;
        }

        public void Reset()
        {
            _droppingTimer = -1;
        }

        public void Tick()
        {
            if (_droppingTimer >= 0)
            {
                _droppingTimer -= 1;
            }
            if (_droppingTimer == 0)
            {
                _snakeBody.ShouldLayDropping = true;
            }
        }

        private void OnLayDroppingPressed(InputAction.CallbackContext ctx)
        {
            if (!ctx.ReadValueAsButton() || _droppingTimer <= 0) { return; }
            _droppingTimer = -1;
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
            _droppingTimer = numDigestionFrames;
        }
    }
}