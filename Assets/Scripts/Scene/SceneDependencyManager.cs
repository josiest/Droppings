using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scene
{
    public class SceneDependencyManager : MonoBehaviour
    {
        [SerializeField] public SceneField[] scenesToLoad;
        [SerializeField] public SceneDependencyTree sceneDependencies;

        private void Start()
        {
            LoadDependencies();
        }

        private void LoadDependencies()
        {
            // first copy the tree's edges as children and parent edge maps
            var children = new Dictionary<string, List<string>>();
            var parents = new Dictionary<string, List<string>>();

            var queue = new List<string>(scenesToLoad.Select(scene => scene.ToString()));
            var visited = new HashSet<string>();
            var independentNodes = new List<string>();

            while (queue.Count > 0)
            {
                var scene = queue[0];
                queue.RemoveAt(0);
                visited.Add(scene);

                if (!sceneDependencies.HasDependencies(scene)) { independentNodes.Add(scene); }
                parents.Add(scene, sceneDependencies.DependenciesOf(scene));

                foreach (var dep in sceneDependencies.DependenciesOf(scene))
                {
                    var edges = children.GetValueOrDefault(dep, new List<string>());
                    edges.Add(scene);
                    children.TryAdd(dep, edges);
                    if (!visited.Contains(dep)) { queue.Add(dep); }
                }
            }

            // now perform topological sort while loading scenes
            var loadOrder = new List<string>();
            while (independentNodes.Count > 0)
            {
                var scene = independentNodes[0];
                independentNodes.RemoveAt(0);
                loadOrder.Add(scene);

                foreach (var child in children[scene])
                {
                    parents[child].Remove(scene);
                    if (parents[child].Count > 0)
                    {
                        continue;
                    }

                    independentNodes.Add(child);
                    parents.Remove(child);
                }
            }

            if (parents.Count > 0)
            {
                Debug.LogError("Some scenes have circular dependencies. Please fix this in order to load scenes.");
                return;
            }

            foreach (var scene in loadOrder)
            {
                SceneManager.LoadScene(scene, LoadSceneMode.Additive);
            }
        }
    }
}
