using UnityEngine;

namespace Game
{
    [RequireComponent(typeof (Wallet))]
    public class Economy : RepeatingBehavior
    {
        public delegate void OnGoldEarnedHandler(int gold);

        public float Interest;

        public event OnGoldEarnedHandler OnGoldEarned;

        protected override void Action()
        {
            var wallet = GetComponent<Wallet>();
            var goldToAdd = (int)(wallet.GetGold() * (1 + Interest));
            if (OnGoldEarned != null) OnGoldEarned(goldToAdd);
            wallet.AddGold(goldToAdd);
        }
    }
}