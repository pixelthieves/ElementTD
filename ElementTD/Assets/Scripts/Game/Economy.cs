using UnityEngine;
using UnitySharedLibrary.Scripts;
using Vexe.Runtime.Extensions;

namespace Game
{
    [RequireComponent(typeof (Wallet))]
    public class Economy : MonoBehaviour
    {
        public delegate void OnGoldEarnedHandler(int gold);

        public float Interest;
        public int Delay;
        public int Interval;

        public event OnGoldEarnedHandler OnGoldEarned;

        private void Start()
        {
            var timer = transform.GetOrAddComponent<SuperTimer>();
            timer.Interval = Delay;
            timer.AutoReset = true;
            timer.OnElapsed += () =>
            {
                EarnInterest();
                timer.Interval = Interval;
            };
            timer.StartCouroutine();
        }

        private void EarnInterest()
        {
            var wallet = GetComponent<Wallet>();
            var goldToAdd = (int) (wallet.GetGold()*(1 + Interest));
            if (OnGoldEarned != null) OnGoldEarned(goldToAdd);
            wallet.AddGold(goldToAdd);
        }
    }
}