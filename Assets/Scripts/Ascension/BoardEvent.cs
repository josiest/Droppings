using Board;
using UnityEngine;

namespace Ascension
{
    public abstract class BoardEvent : MonoBehaviour
    {
        protected virtual void Start()
        {
            var ascensionSystem = GameBoardSystem.FindOrRegister<AscensionSystem>();
            ascensionSystem.OnGlyphCompleted += OnGlyphCompleted;
        }

        public virtual void OnGlyphCompleted()
        {
        }
    }
}