using System.Collections.Generic;
using Creep;
using UnityEngine;
using UnitySharedLibrary.Scripts;
using Vexe.Runtime.Extensions;
using Vexe.Runtime.Types;
using Random = UnityEngine.Random;

namespace Game
{
    [RequireComponent(typeof (Path))]
    public class WaveSpawner : BetterBehaviour
    {
        public Vector3 Direction;
        public float Interval;
        public float Delay;
        public CreepEntity Creep;
        public WaveInfo.Settings settings;

        private List<WaveInfo> waveInfo;
        private List<Wave> waves;
        private int currentWave;
        private Path path;

        private GameObject player;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");

            waveInfo = WaveInfo.Build(WaveDraft.GetNormal(), settings);
            var timer = transform.GetOrAddComponent<SuperTimer>();
            timer.Interval = Delay;
            timer.AutoReset = true;
            timer.OnElapsed += () =>
            {
                SpawnWave(waveInfo[currentWave++]);
                timer.Interval = Interval;
            };
            timer.StartCouroutine();
            path = GetComponent<Path>();

            //            var timer = new Timer();
            //            timer.Interval = Delay;
            //            timer.AutoReset = true;
            //            timer.Elapsed += (a,b) =>
            //            {
            //                SpawnWave(WaveInfo[currentWave++]);
            //                timer.Interval = Interval;
            //            };
            //            timer.Start();
        }

        private void SpawnWave(WaveInfo waveDraft)
        {
            var wave = new Wave();
            var couroutine = RepeatCoroutine.Start(0, waveDraft.CreepDistance, waveDraft.WaveCount, () =>
            {
                var creep = Instantiate(Creep);

                creep.GetComponent<PathView>().Path = path;
                creep.GetComponent<Health>().MaxHealth = waveDraft.Health;
                creep.GetComponent<Health>().OnDead += () =>
                {
                    player.GetComponent<Wallet>().Claim(creep.GetComponent<Wallet>().Treasure);
                };

                creep.GetComponent<MoveOnPath>().OnPathEndReached += () =>
                {
                    creep.gameObject.GetComponent<PathView>().Path = path;
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
            var widthDiff = spread/path.Width;
            var offset = (1 - widthDiff)/2f;
            var pos = Vector3.Lerp(path.FirstLeftPoint, path.FirstRightPoint, offset + Random.value*widthDiff);
            return pos;
        }
    }
}