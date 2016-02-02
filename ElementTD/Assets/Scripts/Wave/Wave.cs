using System;
using System.Collections.Generic;
using Assets.Shared.Scripts;
using Creep;

namespace Game
{
    public class Wave
    {
        private List<CreepEntity> creeps = new List<CreepEntity>();
        public event Action OnWaveCompleted;

        public void AddCreep(CreepEntity creep)
        {
            creep.GetComponent<Health>().OnDead += () =>
            {
                creeps.Remove(creep);
                if (creeps.Empty())
                {
                    if (OnWaveCompleted != null)
                    {
                        OnWaveCompleted();
                    }
                }
            };
            creeps.Add(creep);
        }
    }
}