using System;
using Assets.Shared.Scripts;
using Tower;
using UnityEngine;

namespace Game
{
    public class WorldBuilder : MonoBehaviour
    {
        public Texture2D Map;

        public int TileSize;
        public int ForrestOffset;

        public GameObject Node;
        public GameObject Tile;
        public GameObject ForrestTile;

        public Vector3 Start { get; private set; }
        public Vector3 End { get; private set; }

        public void Build()
        {
            transform.RemoveAllChildren();

            var scaledTexture = new Texture2D(Map.width*2, Map.height*2);
            for (var x = 0; x < scaledTexture.width; x++)
            {
                for (var y = 0; y < scaledTexture.height; y++)
                {
                    var pixel = Map.GetPixel(x/TileSize, y/TileSize);
                    scaledTexture.SetPixel(x, y, pixel);
                }
            }

            var mapGO = new GameObject("Map");
            var map = mapGO.AddComponent<Map>();
            map.PathMap = BuildMap(Map);
            map.TileSize = TileSize;
            BuildWorld(map, TileSize);
            map.transform.SetParent(transform);

            Start = GetObjectPostion(scaledTexture, Color.red);
            End = GetObjectPostion(scaledTexture, Color.blue);
        }

        private Vector3 GetObjectPostion(Texture2D m, Color color)
        {
            var posXy = GetRect(m, color).center;
            return new Vector3(posXy.x - m.width, 0, posXy.y - m.height);
        }

        private bool[,] BuildMap(Texture2D source)
        {
            var grid = new bool[source.width, source.height];

            Fill(
                source.width,
                source.height,
                (x, y) => { grid[x, y] = source.GetPixel(x, y).linear != Color.white; });
            return grid;
        }

        private void BuildWorld(Map m, int tileSize)
        {
            var tiles = new GameObject("Tiles");
            tiles.transform.SetParent(transform);

            var sWidth = m.Width*tileSize;
            var sHeight = m.Height*tileSize;
            Fill(
                sWidth,
                sHeight,
                (x, y) =>
                {
                    if (!m.PathMap[x/tileSize, y/tileSize])
                    {
                        PlaceTile(x, y, sWidth, sHeight, 1, Tile, tiles);
                    }
                });

            var forrestTiles = new GameObject("ForrestTiles");
            forrestTiles.transform.SetParent(transform);

            Fill(
                -ForrestOffset,
                -ForrestOffset,
                sWidth + ForrestOffset,
                sHeight + ForrestOffset,
                (x, y) =>
                {
                    if (!m.InBounds(x/tileSize, y/tileSize))
                    {
                        PlaceTile(x, y, sWidth, sHeight, 1, ForrestTile, forrestTiles);
                    }
                });
        }

        private void PlaceTile(int x, int y, int width, int height, int size, GameObject tile, GameObject parent)
        {
            var groundTile = Instantiate(tile);
            groundTile.name += " [" + x + ", " + y + "]";
            groundTile.transform.localPosition = new Vector3(x - width + 0.5f, 0, y - height + 0.5f)*size;
            groundTile.transform.SetParent(parent.transform);
        }

        private void Fill(int width, int height, Action<int, int> action)
        {
            Fill(0, 0, width, height, action);
        }

        private void Fill(int fromX, int fromY, int toX, int toY, Action<int, int> action)
        {
            for (var x = fromX; x < toX; x++)
            {
                for (var y = fromY; y < toY; y++)
                {
                    action(x, y);
                }
            }
        }

        private Rect GetRect(Texture2D tex, Color color)
        {
            var rect = new Rect(float.MaxValue, float.MaxValue, float.MinValue, float.MinValue);
            for (var x = 0; x < tex.width; x++)
            {
                for (var y = 0; y < tex.height; y++)
                {
                    if (tex.GetPixel(x, y) == color)
                    {
                        rect.xMin = Math.Min(rect.xMin, x);
                        rect.yMin = Math.Min(rect.yMin, y);
                        rect.xMax = Math.Max(rect.xMax, x + 1);
                        rect.yMax = Math.Max(rect.yMax, y + 1);
                    }
                }
            }
            return rect;
        }
    }
}