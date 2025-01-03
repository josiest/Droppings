using Ascension;
using Board;
using Food;
using UnityEngine;

namespace BoardEvents
{
    public class BoardEvent_ClearRandomDroppings : BoardEvent
    {
        [Tooltip("The amount of random droppings to remove when a glyph is completed")]
        public int numDroppingsToRemove = 4;

        public override void OnGlyphCompleted(int glyphTier)
        {
            if (glyphTier != 1) { return; }

            var gameBoard = GameBoardSystem.CurrentBoard;
            gameBoard.RemoveRange(gameBoard.RandomPiecesWithTag(Dropping.DroppingTag, numDroppingsToRemove));
        }
    }
}