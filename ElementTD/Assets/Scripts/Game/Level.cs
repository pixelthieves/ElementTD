using System.Linq;
using Tower;
using UnityEngine;

namespace Game
{
    public class Level : MonoBehaviour
    {
        public GameObject WaveSpawner;

        public void Build()
        {
            var worldBuilder = GetComponentInChildren<WorldBuilder>();
            worldBuilder.Build();

            var map = GetComponentInChildren<Map>();

            // FIXME hardcoded inital position
            var pathData = PathFromMap.Build(map, 1, 8);
            pathData = pathData.Select(p => p.Select(v => v + worldBuilder.Start + Vector3.forward).ToList()).ToList();

            var waveSpawnerGo = Instantiate(WaveSpawner);
            waveSpawnerGo.name = "Wave Spawner";
            waveSpawnerGo.transform.SetParent(transform);
            var path = waveSpawnerGo.GetComponent<Path>();
            path.Init(pathData);
        }
    }
}