using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scenes
{
    [CreateAssetMenu]
    public class SceneDependencyTree : ScriptableObject
    {
        public List<SceneNode> dependencyMap;

        public List<string> DependenciesOf(string Scene)
        {
            return dependencyMap.FirstOrDefault(Node => Node.scene == Scene)?
                                .sceneDependencies.Select(Field => Field.ToString())
                                .ToList();
        }
        public bool HasDependencies(string Scene)
        {
            return dependencyMap.FirstOrDefault(Node => Node.scene == Scene)?
                                .sceneDependencies.Count > 0;
        }
    }
}
