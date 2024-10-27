using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Button))]
    public class ApplicationQuitterWidget : MonoBehaviour
    {
        private static void OnClicked()
        {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
        }
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OnClicked);
        }
    }
}