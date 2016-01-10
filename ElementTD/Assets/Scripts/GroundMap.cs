using UnityEngine;

namespace Assets.Scripts
{
    public class GroundMap : MonoBehaviour
    {
        public TowerMap Map;
        public GameObject Tile;
        public GameObject ForrestTile;
        public int offset;

        private void Start()
        {
            var map = Map.Map;
            for (var x = -offset; x < map.Width + offset; x++)
            {
                for (var y = -offset; y < map.Height+ offset; y++)
                {
                    if (!map.InBounds(x, y) || map[x, y])
                    {
                        var groundTile = Instantiate(map.InBounds(x, y) ? Tile : ForrestTile);
                        groundTile.transform.localPosition = new Vector3(x - map.Width/2+0.5f, 0, y - map.Height/2 + 0.5f);
                        groundTile.transform.SetParent(gameObject.transform);
                    }
                }
            }
        }
    }
}