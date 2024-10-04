using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scene
{
    [CreateAssetMenu]
    public class SceneDependencyTree : ScriptableObject
    {
        public List<SceneNode> dependencyMap;

        public List<string> DependenciesOf(string scene)
        {
            return dependencyMap.FirstOrDefault(node => node.scene == scene)
                                .sceneDependencies.Select(field => field.ToString()).ToList();
        }
        public bool HasDependencies(string scene)
        {
            return dependencyMap.FirstOrDefault(pair => pair.Key == scene).Value.Count > 0;
        }
    }
}
