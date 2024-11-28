using UnityEngine;

namespace Food
{
    [CreateAssetMenu]
    public class TreeSettings : ScriptableObject
    {
        public const string ResourcePath = "Settings/TreeSettings";
        public FoodPickup fruitPrefab;

        private void OnValidate()
        {
            if (!fruitPrefab)
            {
                Debug.LogError($"fruitPrefab in {ResourcePath} is not assigned");
            }
        }
    }
}