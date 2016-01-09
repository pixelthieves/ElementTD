using UnityEngine;

namespace Assets.Scripts
{
    public class TowerMap : MonoBehaviour
    {
        public Map Map;

        private Tower[,] towerMap;

        public int Width
        {
            get { return Map.Width; }
        }

        public int Height
        {
            get { return Map.Height; }
        }

        private void Awake()
        {
            towerMap = new Tower[Map.Width, Map.Height];
        }

        public bool IsAvailable(int x, int y)
        {
            return Map[x, y] && towerMap[x, y] == null;
        }
    }
}