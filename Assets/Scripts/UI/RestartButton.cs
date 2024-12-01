using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Button))]
    public class RestartButton : MonoBehaviour
    {
        private void OnClicked()
        {
            gameOverHandler?.Resume();
        }
        private void Start()
        {
            var button = GetComponent<Button>();
            button?.onClick.AddListener(OnClicked);

            gameOverHandler = GetComponentInParent<GameOverHandler>();
        }
        private GameOverHandler gameOverHandler;
    }
}