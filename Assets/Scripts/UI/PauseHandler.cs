using Board;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI
{
    public class PauseHandler : MonoBehaviour
    {
        // Public Interface
        public void Pause()
        {
            gameObject.SetActive(true);
        }
        public void Resume()
        {
            gameObject.SetActive(false);
        }
        
        // Internal Interface
        private void OnPaused(InputAction.CallbackContext context)
        {
            if (context.ReadValueAsButton()) { gameObject.SetActive(!gameObject.activeSelf); }
        }

        private ActionDefinition actionMappings;
        private TickSystem tickSystem;
        
        // Unity Events
        private void Awake()
        {
            actionMappings = new ActionDefinition();
            actionMappings.playerActions.pause.performed += OnPaused;
            actionMappings.Enable();
            gameObject.SetActive(false);
        }
        private void OnEnable()
        {
            if (!tickSystem) { tickSystem = GameBoardSystem.Find<TickSystem>(); }
            tickSystem.Pause();
        }
        private void OnDisable()
        {
            if (tickSystem) { tickSystem.Resume(); }
        }
        private void OnDestroy()
        {
            if (actionMappings == null) { return; }
            actionMappings.Disable();
            actionMappings.playerActions.pause.performed -= OnPaused;
        }
    }
}