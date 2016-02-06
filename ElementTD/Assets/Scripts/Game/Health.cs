using System;
using UnityEngine;
using Vexe.Runtime.Types;

namespace Game
{
    public class Health : BetterBehaviour
    {
        private float currentHealth;
        public float MaxHealth { get; set; }

        [Show]
        public float CurrentHealth
        {
            get { return currentHealth; }
            set
            {
                currentHealth = Mathf.Clamp(value, 0, MaxHealth);

                if (currentHealth == 0)
                {
                    if (OnDead != null)
                    {
                        OnDead();
                    }
                }
            }
        }

        private void Start()
        {
            CurrentHealth = MaxHealth;
        }

        public event Action OnDead;
    }
}