using Board;
using Game;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Button))]
    public class RestartButton : MonoBehaviour
    {
        private void OnClicked()
        {
            var resetSystem = GameBoardSystem.Find<ResetSystem>();
            resetSystem?.Reset();
        }
        private void Start()
        {
            var button = GetComponent<Button>();
            button?.onClick.AddListener(OnClicked);
        }
    }
}