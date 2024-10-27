using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Button))]
    public class ResumeButton : MonoBehaviour
    {
        private void OnClicked()
        {
            pauseHandler.Resume();
        }
        private void Awake()
        {
            pauseHandler = GetComponentInParent<PauseHandler>();
            button = GetComponent<Button>();
            button.onClick.AddListener(OnClicked);
        }
        private void OnDestroy()
        {
            button.onClick.RemoveListener(OnClicked);
        }

        private PauseHandler pauseHandler;
        private Button button;
    }
}