using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    /** Various settings and defaults that configure the game */
    [CreateAssetMenu]
    public class GameSettings : ScriptableObject
    {
        public static GameSettings GetInstance()
        {
            return _instance ??= CreateDefault();
        }

        public static GameSettings CreateDefault()
        {
            _instance = CreateInstance<GameSettings>();
            _instance.difficultySetting = CreateInstance<DifficultySettings>();
            _instance.snakeWorldSettings = CreateInstance<SnakeWorldSettings>();
            return _instance;
        }
        public static void SetInstance(GameSettings Instance) { _instance = Instance; }
    
        private static GameSettings _instance;

        /** The default difficulty to start the game as */
        [FormerlySerializedAs("defaultDifficulty")] public DifficultySettings difficultySetting;
    
        /** The default snake world settings */
        public SnakeWorldSettings snakeWorldSettings;

    }
}
