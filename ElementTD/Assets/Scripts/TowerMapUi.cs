using Assets.Shared.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    [RequireComponent(typeof (GridLayoutGroup))]
    public class TowerMapUi : MonoBehaviour
    {
        public TowerMap TowerMap;
        public Tile Tile;

        private void Start()
        {
            var rectTransform = GetComponent<RectTransform>();
            rectTransform.SetWidth(TowerMap.Width);
            rectTransform.SetHeight(TowerMap.Height);

            var grid = GetComponent<GridLayoutGroup>();
            grid.constraint = GridLayoutGroup.Constraint.FixedRowCount;
            grid.constraintCount = TowerMap.Width;

            var cellSizeX = rectTransform.GetWidth()/TowerMap.Width;
            var cellSizeY = rectTransform.GetHeight()/TowerMap.Height;
            grid.cellSize = new Vector2(cellSizeX, cellSizeY);

            for (var y = 0; y < TowerMap.Height; y++)
            {
                for (var x = 0; x < TowerMap.Width; x++)
                {
                    var tile = Instantiate(Tile);
                    tile.Visible = TowerMap.IsAvailable(x, y);
                    tile.X = x;
                    tile.Y = y;
                    tile.OnClicked += OnTileClicked;
                    tile.transform.SetParent(transform);
                }
            }
        }

        private void OnTileClicked(int x, int y)
        {
            Debug.Log("Tile[" + x + ", " + y + "] has been clicked.");
        }
    }
}