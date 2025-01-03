using System.Collections.Generic;
using UnityEngine;

namespace Ascension
{
    [CreateAssetMenu]
    public class AscensionSettings : ScriptableObject
    {
        public const string ResourcePath = "Settings/AscensionSettings";

        [Tooltip("The base or tally mark for each tier. i.e. how many points to add up before " +
                 "incrementing the base. Tier 0 are ascension points.")]
        public List<int> PointsPerTier = new List<int> { 9, 3 };

        [Tooltip("The board events that should be used in gameplay")]
        public List<BoardEvent> ActiveBoardEvents = new();

        public static AscensionSettings LoadOrCreateDefault()
        {
            var settings = Resources.Load<AscensionSettings>(ResourcePath);
            if (!settings)
            {
                 Debug.LogWarning("[Droppings.Ascension.AscensionSettings] Unable to load ascension settings at " +
                                  $"{ResourcePath}, using default settings instead");
                 settings = CreateInstance<AscensionSettings>();
            }
            return settings;
        }
    }
}