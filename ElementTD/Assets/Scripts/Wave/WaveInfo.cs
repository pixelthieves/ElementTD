using System;
using System.Collections.Generic;
using Creep;

namespace Game
{
    public class WaveInfo
    {
        public int WaveCount { get; private set; }
        public int Health { get; private set; }
        public float Speed { get; private set; }
        public float Size { get; private set; }
        public float Spread { get; private set; }
        public float CreepDistance { get; private set; }
        public Treasure Tresure { get; private set; }

        public WaveInfo(int waveCount, int health, float speed, float size, float spread, float creepDistance, Treasure treasure)
        {
            WaveCount = waveCount;
            Health = health;
            Speed = speed;
            Size = size;
            Spread = spread;
            CreepDistance = creepDistance;
            Tresure = treasure;
        }

        [Serializable]
        public struct Settings
        {
            public int WaveCount { get; private set; }
            public int SwarmMultiplayer { get; private set; }
            public float Speed { get; private set; }
            public float CreepDistance { get; private set; }

            // TODO Get the difficulty elsewhere.
            public Difficulty Difficulty { get; private set; }

            public Settings(int waveCount, int swarmMultiplayer, float speed, float creepDistance, Difficulty difficulty)
                : this()
            {
                WaveCount = waveCount;
                SwarmMultiplayer = swarmMultiplayer;
                Speed = speed;
                CreepDistance = creepDistance;
                Difficulty = difficulty;
            }
        }

        public static List<WaveInfo> Build(List<WaveDraft> drafts, Settings settings)
        {
            List<WaveInfo> list = new List<WaveInfo>();
            foreach (var draft in drafts)
            {
                var waveCount = settings.WaveCount;
                var swarm = settings.SwarmMultiplayer;
                var speed = settings.Speed;
                var creepDistance = settings.CreepDistance;

                // Size scale
                var size = 1f;
                var health = (float) draft.Health;
                var treasure = draft.Treasure;

                foreach (var creepType in draft.Traits)
                {
                    switch (creepType)
                    {
                        case CreepTraits.Fast:
                            speed *= 2;
                            health /= 2;
                            break;
                        case CreepTraits.Resurrect:
                            health *= 0.75f;
                            break;
                        case CreepTraits.Invisible:
                            health *= 0.75f;
                            break;
                        case CreepTraits.Swarm:
                            health /= swarm/2;
                            size /= 1.5f;
                            waveCount *= swarm;
                            creepDistance = size/3;
                            break;
                        case CreepTraits.Healing:
                            // TODO apply health modifier
                            break;
                        case CreepTraits.Boss:
                            size *= 3f;
                            waveCount /= settings.WaveCount;
                            break;
                    }
                }

                var spread = (float)waveCount / settings.WaveCount;

                list.Add(new WaveInfo(waveCount, (int) health, speed, size, spread, creepDistance, treasure));
            }
            return list;
        }
    }
}