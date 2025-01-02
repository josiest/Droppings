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
    }
}