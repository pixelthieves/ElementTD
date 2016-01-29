using Game;
using UnityEngine;
using UnityEngine.UI;

namespace Tower
{
    [RequireComponent(typeof (GridLayoutGroup))]
    public class TowerMapUi : MonoBehaviour
    {
        public Blueprint TowerMap;
        public Tile Tile;
        public GameObject currentTower;

        private void Start()
        {
            /*var rectTransform = GetComponent<RectTransform>();
            rectTransform.SetWidth(TowerMap.Width);
            rectTransform.SetHeight(TowerMap.Height);

            var grid = GetComponent<GridLayoutGroup>();
            grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
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
                    tile.OnClicked += PlaceTower;
                    tile.transform.SetParent(transform);
                    tile.transform.localPosition = Vector3.zero;
                    // FIXME for whaterer reason the ui is upside down
                    tile.transform.localRotation = Quaternion.Euler(0,180,0);
                }
            }*/
        }

        private void PlaceTower(Tile tile)
        {
            if (tile.Visible)
            {
                var tower = Instantiate(currentTower);
                tower.transform.position = tile.transform.position;
            }
        }

        public void setlectTower(GameObject tower)
        {
            currentTower = tower;
        }
    }
}