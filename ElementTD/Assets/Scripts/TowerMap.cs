using UnityEngine;

namespace Assets.Scripts
{
    public class TowerMap : MonoBehaviour
    {
        public bool[,] PathMap;

        private Tower[,] towerMap;

        public int Width
        {
            get { return PathMap.GetLength(0); }
        }

        public int Height
        {
            get { return PathMap.GetLength(1); }
        }

        public bool InBounds(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }

        private void Awake()
        {
            //towerMap = new Tower[PathMap.Width, PathMap.Height];
        }

        public bool IsAvailable(int x, int y)
        {
            return PathMap[x, y] && towerMap[x, y] == null;
        }
    }
}