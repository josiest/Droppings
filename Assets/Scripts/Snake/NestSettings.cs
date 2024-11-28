using UnityEngine;

namespace Snake
{
    [CreateAssetMenu]
    public class NestSettings : ScriptableObject
    {
        public const string ResourcePath = "Settings/NestSettings";
        public SnakeBody snakePrefab;

        private void OnValidate()
        {
            if (!snakePrefab)
            {
                Debug.LogError($"SnakePrefab in {ResourcePath} is not assigned");
            }
        }
    }
}