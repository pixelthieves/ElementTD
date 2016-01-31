using System;
using UnityEngine;

namespace Game
{
    public class Health : MonoBehaviour
    {
        public float MaxHealth { get; set; }

        public event Action OnDead;
    }
}