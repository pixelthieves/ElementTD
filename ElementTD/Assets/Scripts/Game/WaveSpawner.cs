using System;
using System.Collections.Generic;
using System.Timers;
using Creep;
using UnityEngine;
using UnitySharedLibrary.Scripts;
using Vexe.Runtime.Extensions;
using Vexe.Runtime.Types;

namespace Game
{
    [RequireComponent(typeof(Path))]
    public class WaveSpawner : BetterBehaviour
    {
        public Vector3 Direction;

        public float Interval;
        public float Delay;

        public WaveInfo[] WaveInfo;
        private int currentWave;
        private int BaseWaveCount = 10;
        private float SpawnTime = 1;
        private int SwarmMultiplier = 4;

        private void Start()
        {
            var timer = transform.GetOrAddComponent< SuperTimer>();
            timer.Interval = Delay;
            timer.AutoReset = true;
            timer.OnElapsed += () =>
            {
                SpawnWave(WaveInfo[currentWave++]);
                timer.Interval = Interval;
            };
            timer.StartCouroutine();
        }

        private void SpawnWave(WaveInfo waveInfo)
        {
            var creepWaveCount = getCreepsInWave(waveInfo);
            var healthMultiplier = getHeahtMultiplier(waveInfo);
            var spread = (float) creepWaveCount/BaseWaveCount - 1;
            var spawnTime = SpawnTime/((float) creepWaveCount/BaseWaveCount);

            var couroutine = RepeatCoroutine.Start(0, spawnTime, creepWaveCount, () =>
            {
                var creep = Instantiate(waveInfo.Creep);

                // TODO spread position
                var component = GetComponent<Path>();
                creep.GetComponent<PathView>().path = component;
                creep.GetComponent<Health>().MaxHealth *= healthMultiplier;
                creep.transform.position = creep.GetComponent<PathView>().FirstCenterPoint;
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