using System;
using Pi.Subsystems;
using Score;
using TMPro;
using UnityEngine;

namespace UI.Score
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class NewHighScoreHandler : MonoBehaviour
    {
        private void Awake()
        {
            text = GetComponent<TextMeshProUGUI>();
            legacy = GameSubsystems.Find<AncestralLegacy>();
            if (legacy) { legacy.OnNewHighScore += OnNewHighScore; }
        }

        private void OnDisable()
        {
            if (text) { text.enabled = false; }
        }

        private void OnDestroy()
        {
            if (legacy) { legacy.OnNewHighScore -= OnNewHighScore; }
        }

        private void OnNewHighScore(int newHighScore)
        {
            if (text) { text.enabled = true; }
        }

        private AncestralLegacy legacy;
        private TextMeshProUGUI text;
    }
}