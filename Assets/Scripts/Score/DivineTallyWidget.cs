using Scene;
using TMPro;
using UnityEngine;

namespace Score
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class DivineTallyWidget : MonoBehaviour
    {
        private TextMeshProUGUI _scoreText;

        public void Start()
        {
            _scoreText = GetComponent<TextMeshProUGUI>();
            var divineAbacus = SceneSubsystems.Find<DivineAbacus>();
            divineAbacus.OnScoreChanged += OnScoreChanged;
        }

        private void OnScoreChanged(int score)
        {
            _scoreText.text = score.ToString();
        }
    }
}