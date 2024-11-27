using System.Collections.Generic;
using UnityEngine;

namespace Scenes
{
    [CreateAssetMenu]
    public class SceneNode : ScriptableObject
    {
        public SceneField scene;
        public List<SceneField> sceneDependencies = new();
    }
}
