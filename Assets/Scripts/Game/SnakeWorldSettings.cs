using UnityEngine;

namespace Game
{
    [CreateAssetMenu]
    public class SnakeWorldSettings : ScriptableObject
    {
        /** The unit size of the snake world (in world units) */
        public float unitSize = 1f;
    
        /** The default snake segment prefab to use if none was set up */
        public GameObject defaultSnakeSegmentPrefab;
    }
}
