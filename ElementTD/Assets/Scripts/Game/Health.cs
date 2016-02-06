using System;
using UnityEngine;

namespace Game
{
    public class Health : MonoBehaviour
    {
        private float currentHealth;
        public float MaxHealth { get; set; }

        [SerializeField]
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

        public event Action OnDead;
    }
}