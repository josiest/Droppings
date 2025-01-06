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

        protected override void Start()
        {
            base.Start();
            gameBoard = GameBoardSystem.CurrentBoard;
        }

        public override void OnGlyphCompleted()
        {
            var gameBoard = GameBoardSystem.CurrentBoard;
            var piecesToClear = gameBoard.RandomPiecesWithTag(Dropping.DroppingTag, numDroppingsToRemove).ToList();
            foreach (var piece in piecesToClear)
            {
                piece.CollisionEnabled = false;
                var fadeOut = piece.GetComponent<SequentialFadeOutBehavior>();
                if (fadeOut)
                {
                    fadeOut.OnCompleted += () => { gameBoard.RemovePiece(piece); };
                    fadeOut.FadeOut();
                }
                else
                {
                    gameBoard.RemovePiece(piece);
                }
            }
        }

        private GameBoard gameBoard;
    }
}