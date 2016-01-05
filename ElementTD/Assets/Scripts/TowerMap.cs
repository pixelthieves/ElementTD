using UnityEngine;

namespace Assets.Scripts
{
    public class TowerMap : MonoBehaviour
    {
        public int Width;
        public int Height;

        Tower[,] map;

        private void Start()
        {
             map = new Tower[Width, Height];
        }
    }
}