using Pi.Subsystems;
using Score;
using TMPro;
using UnityEngine;

namespace UI.Score
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class HighScoreHandler : MonoBehaviour
    {
        private void Awake()
        {
            legacy = GameSubsystems.FindOrRegister<AncestralLegacy>();
            UpdateScoreText();
            if (legacy) { legacy.OnNewHighScore += OnNewHighScore; } 
        }

        private void OnDestroy()
        {
            if (legacy) { legacy.OnNewHighScore -= OnNewHighScore; }
        }

        private void OnNewHighScore(int score)
        {
            UpdateScoreText();
        }

        private void UpdateScoreText()
        {
            var text = GetComponent<TextMeshProUGUI>();
            if (legacy && text) { text.text = legacy.HighScore.ToString(); }
        }

        private AncestralLegacy legacy;
    }
}