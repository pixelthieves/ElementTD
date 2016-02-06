using System.Collections.Generic;
using Creep;
using UnityEngine;
using UnitySharedLibrary.Scripts;
using Vexe.Runtime.Extensions;
using Random = UnityEngine.Random;

namespace Game
{
    [RequireComponent(typeof (Path))]
    public class WaveSpawner : RepeatingBehavior
    {
        public Vector3 Direction;
        public CreepEntity Creep;
        public WaveInfo.Settings settings;

        private List<WaveInfo> waveInfo;
        private List<Wave> waves;
        private int currentWave;

        private GameObject player;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            waveInfo = WaveInfo.Build(WaveDraft.GetNormal(), settings);
        }

        private void SpawnWave(WaveInfo waveDraft)
        {
            var wave = new Wave();
            var couroutine = RepeatCoroutine.Start(0, waveDraft.CreepDistance, waveDraft.WaveCount, () =>
            {
                var creep = Instantiate(Creep);

                creep.GetComponent<PathView>().Path = GetComponent<Path>();
                creep.GetComponent<Health>().MaxHealth = waveDraft.Health;
                creep.GetComponent<Health>().OnDead += () =>
                {
                    player.GetComponent<Wallet>().Claim(creep.GetComponent<Wallet>().Treasure);
                };

                creep.GetComponent<MoveOnPath>().OnPathEndReached += () =>
                {
                    creep.gameObject.GetComponent<PathView>().Path = GetComponent<Path>();
                    creep.transform.position = GetInitalPosition(waveDraft.Spread);
                };
                creep.GetOrAddComponent<Wallet>().Treasure = waveDraft.Tresure;
                creep.transform.position = GetInitalPosition(waveDraft.Spread);
                creep.transform.SetParent(transform);
                wave.AddCreep(creep);
            });

            StartCoroutine(couroutine);
        }

        private Vector3 GetInitalPosition(float spread)
        {
            var path = GetComponent<Path>();
            var widthDiff = spread/path.Width;
            var offset = (1 - widthDiff)/2f;
            var pos = Vector3.Lerp(path.FirstLeftPoint, path.FirstRightPoint, offset + Random.value*widthDiff);
            return pos;
        }

        protected override void Action()
        {
            if (currentWave < waveInfo.Count)
                SpawnWave(waveInfo[currentWave++]);
        }
    }
}