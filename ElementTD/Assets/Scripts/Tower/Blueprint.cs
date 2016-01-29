using UnityEngine;

namespace Tower
{
    public class Blueprint : MonoBehaviour
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

        public bool InBounds(float x, float y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }

        public bool IsPath(int x, int y)
        {
            return InBounds(x, y) && PathMap[x, y];
        }

        public bool IsAvailable(int x, int y)
        {
            return PathMap[x, y] && towerMap[x, y] == null;
        }
    }
}