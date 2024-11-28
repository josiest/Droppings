using UnityEngine;

namespace Food
{
    [CreateAssetMenu]
    public class TreeSettings : ScriptableObject
    {
        public const string ResourcePath = "Settings/TreeSettings";
        
        /** The fruit that will be dropped from the divine tree */
        public FoodPickup FruitPrefab
        {
            get
            {
                if (!fruitPrefab) { LoadDefault();}
                return fruitPrefab;
            }
        }
        
        [Tooltip("The fruit that will be dropped from the divine tree")]
        [SerializeField] private FoodPickup fruitPrefab;

        private void OnValidate()
        {
            if (fruitPrefab) { return; }
            Debug.LogWarning($"fruitPrefab in {ResourcePath} is not assigned, " +
                             $"using default prefab at {FoodPickup.DefaultPath}");
            LoadDefault();
        }
        
        private void LoadDefault()
        {
            fruitPrefab = Resources.Load<FoodPickup>(FoodPickup.DefaultPath);
            if (!fruitPrefab)
            {
                Debug.LogError($"Default fruit prefab at {FoodPickup.DefaultPath} doesn't exist");
            }
        }
    }
}