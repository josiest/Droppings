using System.Collections.Generic;
using Ascension;
using Board;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Score
{
    public class GlyphWidget : MonoBehaviour
    {
        [SerializeField] public List<Image> GlyphPieces = new();

        private void Start()
        {
            ascensionSystem = GameBoardSystem.FindOrRegister<AscensionSystem>();
            if (!ascensionSystem) { return; }

            if (ascensionSystem.PointsForTier(0) != GlyphPieces.Count)
            {
                Debug.LogError("Points for tier 1 do not match the amount of glyph pieces in the UI. " +
                               "The ascension glyph UI won't work as expected.");
            }
            ascensionSystem.OnPointAdded += OnPointAdded;
            ascensionSystem.OnGlyphCompleted += OnGlyphCompleted;

            foreach (var piece in GlyphPieces)
            {
                piece.gameObject.SetActive(false);
            }
        }

        private void OnDestroy()
        {
            if (!ascensionSystem) { return; }
            ascensionSystem.OnPointAdded -= OnPointAdded;
            ascensionSystem.OnGlyphCompleted -= OnGlyphCompleted;
        }

        private void OnPointAdded()
        {
            if (currentIndex >= GlyphPieces.Count) { return; }
            GlyphPieces[currentIndex]?.gameObject.SetActive(true);
            currentIndex += 1;
        }

        private void OnGlyphCompleted()
        {
            if (!ascensionSystem) { return; }
            ascensionSystem.OnPointAdded -= OnPointAdded;
            ascensionSystem.OnGlyphCompleted -= OnGlyphCompleted;
        }

        private int currentIndex = 0;
        private AscensionSystem ascensionSystem;
    }
}