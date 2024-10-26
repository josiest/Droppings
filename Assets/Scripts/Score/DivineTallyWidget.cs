﻿using Subsystems;
using TMPro;
using UnityEngine;

namespace Score
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class DivineTallyWidget : MonoBehaviour
    {
        private TextMeshProUGUI scoreText;

        public void Start()
        {
            scoreText = GetComponent<TextMeshProUGUI>();
            var divineAbacus = SceneSubsystemLocator.Find<DivineAbacus>();
            divineAbacus.OnScoreChanged += OnScoreChanged;
        }

        private void OnScoreChanged(int score)
        {
            scoreText.text = score.ToString();
        }
    }
}