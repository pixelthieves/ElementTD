using System.Linq;
using Tower;
using UnityEngine;

namespace Game
{
    public class Level : MonoBehaviour
    {
        public void Build()
        {
            var worldBuilder = GetComponentInChildren<WorldBuilder>();
            worldBuilder.Build();

            var map = GetComponentInChildren<Map>();

            // FIXME hardcoded inital position
            var path = PathFromMap.Build(map, 1, 8);
            path = path.Select(p => p.Select(v => v + worldBuilder.Start + Vector3.forward).ToList()).ToList();

            var pathGO = new GameObject("Path");
            pathGO.AddComponent<Path>().Init(path);
            pathGO.transform.SetParent(transform);
        }
    }
}