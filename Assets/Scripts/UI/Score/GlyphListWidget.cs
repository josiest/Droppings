using System.Collections.Generic;
using Ascension;
using Board;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Score
{
    [RequireComponent(typeof(HorizontalLayoutGroup))]
    public class GlyphListWidget : MonoBehaviour
    {
        public GlyphWidget GlyphPrefab;

        private void Start()
        {
            AddGlyph();
            ascensionSystem = GameBoardSystem.FindOrRegister<AscensionSystem>();
            ascensionSystem.OnScoreReset += ResetGlyphs;
            ascensionSystem.OnGlyphCompleted += AddGlyph;
        }

        private void AddGlyph()
        {
            glyphs.Add(Instantiate(GlyphPrefab.gameObject, transform).GetComponent<GlyphWidget>());
        }

        private void ResetGlyphs()
        {
            foreach (var glyph in glyphs) { Destroy(glyph.gameObject); }
            glyphs.Clear();
            AddGlyph();
        }

        private readonly List<GlyphWidget> glyphs = new();
        private AscensionSystem ascensionSystem;
    }
}