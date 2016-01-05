using Assets.Shared.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    [RequireComponent(typeof(GridLayoutGroup))]
    public class TowerMapUi : MonoBehaviour
    {
        public TowerMap TowerMap;
        public Tile Tile;

        private void Awake()
        {
            var rectTransform = GetComponent<RectTransform>();
            rectTransform.SetWidth(TowerMap.Width);
            rectTransform.SetHeight(TowerMap.Height);

            var grid = GetComponent<GridLayoutGroup>();
            grid.constraint = GridLayoutGroup.Constraint.FixedRowCount;
            grid.constraintCount = TowerMap.Width;
            grid.cellSize = new Vector2(rectTransform.GetWidth() / TowerMap.Width, rectTransform.GetHeight() / TowerMap.Height);

            for (var x = 0; x < TowerMap.Width; x++)
            {
                for (var y = 0; y < TowerMap.Height; y++)
                {
                    var tile = Instantiate(Tile);
                    tile.X = x;
                    tile.Y = y;
                    tile.OnClicked += OnTileClicked;
                    tile.transform.parent = transform;
                }
            }
        }

        private void OnTileClicked(int x, int y)
        {
            Debug.Log("Tile["+x+", "+y+"] has been clicked.");
        }
    }
}