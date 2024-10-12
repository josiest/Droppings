using System;
using UnityEngine;

namespace Scene
{
    public class SceneSubsystems : MonoBehaviour
    {
        private static SceneSubsystems _instance;
        public static T Find<T>() where T : SceneSubsystem
        {
            return _instance?.GetComponent<T>();
        }

        public void OnEnable()
        {
            if (_instance == null) { _instance = this; }
        }
        public void OnDisable()
        {
            if (_instance == this) { _instance = null; }
        }
    }
}