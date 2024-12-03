using Board;
using Game;
using Snake;
using UnityEngine;

namespace Food
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Dropping : Pickup
    {
        public const string DroppingTag = "Dropping";

        private ResetSystem resetSystem;
        protected override void Start()
        {
            base.Start();
            resetSystem = GameBoardSystem.FindOrRegister<ResetSystem>();
        }
        protected override void ConsumeBy(SnakeBody snake)
        {
            resetSystem.GameOver();
        }
    }
}