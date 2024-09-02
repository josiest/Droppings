using Game;
using UnityEngine;

namespace Snake
{
    /** Settings asset that describes a snake body */
    [CreateAssetMenu]
    public class SnakeBodySettings : ScriptableObject
    {
         /** The prefab class used to represent a body segment */
         public GameObject segmentPrefab;
         
         /** The number of body segments the snake should have */
         public int segmentCount = 4;
 
         /** The direction to generate the body segments in at the start of the game */
         public CardinalDirection startingBodyDirection = CardinalDirection.South;

         public static SnakeBodySettings CreateDefault()
         {
             var settings = CreateInstance<SnakeBodySettings>();
             settings.segmentPrefab = GameSettings.GetInstance().snakeWorldSettings.defaultSnakeSegmentPrefab;
             return settings;
         }
    }
}