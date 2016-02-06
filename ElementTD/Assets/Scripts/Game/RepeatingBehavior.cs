﻿using UnitySharedLibrary.Scripts;
using Vexe.Runtime.Extensions;
using Vexe.Runtime.Types;

namespace Game
{
    public abstract class RepeatingBehavior : BetterBehaviour
    {
        public int Delay;
        public int Interval;

        private void Awake()
        {
            var timer = transform.GetOrAddComponent<SuperTimer>();
            timer.Interval = Delay;
            timer.AutoReset = true;
            timer.OnElapsed += () =>
            {
                Action();
                timer.Interval = Interval;
            };
            timer.StartCouroutine();

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

        protected abstract void Action();
    }
}