using Board;

namespace Ascension
{
    public class BoardEventSystem : GameBoardSubsystem
    {
        public void Start()
        {
            var settings = AscensionSettings.LoadOrCreateDefault();
            foreach (var eventPrefab in settings.ActiveBoardEvents)
            {
                Instantiate(eventPrefab.gameObject, transform);
            }
        }
    }
}