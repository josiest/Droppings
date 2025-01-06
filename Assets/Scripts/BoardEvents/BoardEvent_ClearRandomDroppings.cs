using System.Linq;
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

        public override void OnGlyphCompleted()
        {
            gameBoard = GameBoardSystem.CurrentBoard;
            var piecesToClear = gameBoard.RandomPiecesWithTag(Dropping.DroppingTag, numDroppingsToRemove).ToList();

            for (int i = 0; i < piecesToClear.Count; i++)
            {
                var piece = piecesToClear[i];
                piece.CollisionEnabled = false;
                var currentFadeOut = piece.GetComponent<SequentialFadeOutBehavior>();
                if (currentFadeOut)
                {
                    currentFadeOut.OnCompleted += () => { gameBoard.RemovePiece(piece); };
                }
                else
                {
                    gameBoard.RemovePiece(piece);
                }

                if (i >= piecesToClear.Count - 1) { continue; }
                var nextFadeOut = piecesToClear[i + 1].GetComponent<SequentialFadeOutBehavior>();
                if (nextFadeOut) { currentFadeOut.OnStagger += () => { nextFadeOut.FadeOut(); }; }
            }

            if (piecesToClear.Count >= 1)
            {
                var lastPiece = piecesToClear.Last();
                var lastFadeOut = lastPiece.GetComponent<SequentialFadeOutBehavior>();
                lastFadeOut.OnCompleted += () =>
                {
                    gameBoard.RemovePiece(lastPiece);
                };

                var firstFadeOut = piecesToClear.First().GetComponent<SequentialFadeOutBehavior>();
                firstFadeOut.FadeOut();
            }
        }

        private GameBoard gameBoard;
    }
}