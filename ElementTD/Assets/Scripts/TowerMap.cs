using UnityEngine;

namespace Assets.Scripts
{
    public class TowerMap : MonoBehaviour
    {
        public int Width;
        public int Height;

        private Tower[,] map;

        private void Awake()
        {
            map = new Tower[Width, Height];
        }
    }
}