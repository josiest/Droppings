using Board;
using Subsystems;
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
            tickSystem = SceneSubsystemLocator.Find<TickSystem>();

            actionMappings.playerActions.pause.performed += OnPaused;
            actionMappings.Enable();
            gameObject.SetActive(false);
        }
        private void OnEnable()
        {
            tickSystem.Pause();
        }
        private void OnDisable()
        {
            if (tickSystem) { tickSystem.Resume(); }
        }
        private void OnDestroy()
        {
            actionMappings.Disable();
            actionMappings.playerActions.pause.performed -= OnPaused;
        }
    }
}