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
            var Children = new Dictionary<string, List<string>>();
            var Parents = new Dictionary<string, List<string>>();

            var Queue = new List<string>(scenesToLoad.Select(Scene => Scene.ToString()));
            var Visited = new HashSet<string>();
            var IndependentNodes = new List<string>();

            while (Queue.Count > 0)
            {
                var Scene = Queue[0];
                Queue.RemoveAt(0);
                Visited.Add(Scene);

                if (!sceneDependencies.HasDependencies(Scene)) { IndependentNodes.Add(Scene); }
                Parents.Add(Scene, sceneDependencies.DependenciesOf(Scene));

                foreach (var Dep in sceneDependencies.DependenciesOf(Scene))
                {
                    var Edges = Children.GetValueOrDefault(Dep, new List<string>());
                    Edges.Add(Scene);
                    Children.TryAdd(Dep, Edges);
                    if (!Visited.Contains(Dep)) { Queue.Add(Dep); }
                }
            }

            // now perform topological sort while loading scenes
            var LoadOrder = new List<string>();
            while (IndependentNodes.Count > 0)
            {
                var Scene = IndependentNodes[0];
                IndependentNodes.RemoveAt(0);
                LoadOrder.Add(Scene);

                foreach (var Child in Children[Scene])
                {
                    Parents[Child].Remove(Scene);
                    if (Parents[Child].Count > 0)
                    {
                        continue;
                    }

                    IndependentNodes.Add(Child);
                    Parents.Remove(Child);
                }
            }

            if (Parents.Count > 0)
            {
                Debug.LogError("Some scenes have circular dependencies. " +
                               "Please fix this in order to load scenes.");
                return;
            }

            foreach (var Scene in LoadOrder)
            {
                SceneManager.LoadScene(Scene, LoadSceneMode.Additive);
            }
        }
    }
}
