using System;
using System.Collections.Generic;
using Assets.Shared.Scripts;
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
        public WaveInfo[] WaveInfo;

        private int currentWave;
        private readonly int BaseWaveCount = 10;
        private readonly float SpawnTime = 1;
        private readonly int SwarmMultiplier = 4;
        private Path path;

        private void Start()
        {
            var timer = transform.GetOrAddComponent<SuperTimer>();
            timer.Interval = Delay;
            timer.AutoReset = true;
            timer.OnElapsed += () =>
            {
                SpawnWave(WaveInfo[currentWave++]);
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

        private void SpawnWave(WaveInfo waveInfo)
        {
            var creepWaveCount = getCreepsInWave(waveInfo);
            var healthMultiplier = getHeahtMultiplier(waveInfo);
            var spread = (float) creepWaveCount/BaseWaveCount;
            var spawnTime = SpawnTime/((float) creepWaveCount/BaseWaveCount);

            var couroutine = RepeatCoroutine.Start(0, spawnTime, creepWaveCount, () =>
            {
                var creep = Instantiate(waveInfo.Creep);

                // TODO spread position

                creep.GetComponent<PathView>().path = path;
                creep.GetComponent<Health>().MaxHealth *= healthMultiplier;

                var widthDiff = (spread/path.Width);
                var offset = (1 - widthDiff)/2f;
                var pos = Vector3.Lerp(path.FirstLeftPoint, path.FirstRightPoint, offset + Random.value*widthDiff);

                creep.transform.position = pos;
            });

            StartCoroutine(couroutine);
        }

        private float getHeahtMultiplier(WaveInfo waveInfo)
        {
            float healthMultiplier = 1;
            foreach (var creepType in waveInfo.CreepType)
            {
                switch (creepType)
                {
                    case CreepTraits.Swarm:
                        healthMultiplier /= SwarmMultiplier*0.75f; // Splash damage really pays off
                        break;
                    case CreepTraits.Boss:
                        healthMultiplier *= BaseWaveCount; // Hard, splash damage is less useful
                        break;
                }
            }
            return healthMultiplier;
        }

        private int getCreepsInWave(WaveInfo waveInfo)
        {
            var creepsInWave = BaseWaveCount;
            foreach (var creepType in waveInfo.CreepType)
            {
                switch (creepType)
                {
                    case CreepTraits.Swarm:
                        creepsInWave *= SwarmMultiplier;
                        break;
                    case CreepTraits.Boss:
                        creepsInWave /= BaseWaveCount;
                        break;
                }
            }
            return creepsInWave;
        }
    }

    [Serializable]
    public class WaveInfo
    {
        public CreepEntity Creep;
        public List<CreepTraits> CreepType;
    }
}