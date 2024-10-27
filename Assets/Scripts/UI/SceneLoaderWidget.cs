using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.MainMenu
{
    [RequireComponent(typeof(Button))]
    public class SceneLoaderWidget : MonoBehaviour
    {
        /** The scene that will be loaded when the button is pressed */
        public SceneField SceneToLoad;

        private void OnClicked()
        {
            SceneManager.LoadScene(SceneToLoad);
        }

        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(OnClicked);
        }
    }
}