using System;
using System.Linq;
using Assets.Shared.Scripts;
using Tower;
using UnityEngine;
using Vexe.Runtime.Extensions;

namespace Game
{
    public class Level : MonoBehaviour
    {
        public Texture2D Map;

        public int TileSize;
        public int ForrestOffset;

        public GameObject Node;
        public GameObject Tile;
        public GameObject ForrestTile;
        public GameObject WaveSpawner;
        private int mapWidth;
        private int mapHeight;
        private Blueprint blueprint;
        private GameObject structures;

        public int PathCount;
        public int PathSegmentCount;
        public float PathWidth;

        public Vector3 End { get; set; }

        public Vector3 Start { get; set; }

        public void Build()
        {
            transform.RemoveAllChildren();

            structures = new GameObject("Structures");
            structures.transform.SetParent(transform);

            BuildBlueprint();
            BuildStructures();
            BuildPath();
        }

        private void BuildPath()
        {
            var scaledTexture = ScaleTexture(Map, TileSize);
            Start = GetObjectPostion(scaledTexture, Color.red);
            End = GetObjectPostion(scaledTexture, Color.blue);

            var pathSettings = new PathBuilder.Settings(PathCount, PathSegmentCount, TileSize, TileSize*PathWidth);
            // FIXME hardcoded inital position
            var pathData = PathFromMap.Build(blueprint, 1, 8, pathSettings);
            pathData = pathData.Select(p => p.Select(v => v + Start + Vector3.forward).ToList()).ToList();

            var waveSpawnerGo = Instantiate(WaveSpawner);
            waveSpawnerGo.name = "Creeps";
            waveSpawnerGo.transform.SetParent(transform);
            var path = waveSpawnerGo.GetComponent<Path>();
            path.Init(pathData);

            var towers = new GameObject("Towers");
            towers.transform.SetParent(transform);
        }

        private void BuildStructures()
        {
            mapWidth = blueprint.Width*TileSize;
            mapHeight = blueprint.Height*TileSize;

            var tiles = BuildTiles();
            tiles.transform.SetParent(structures.transform);

            var forrest = BuildForrest();
            forrest.transform.SetParent(structures.transform);
        }

        private void BuildBlueprint()
        {
            blueprint = gameObject.GetOrAddComponent<Blueprint>();

            var grid = new bool[Map.width, Map.height];

            for (var x = 0; x < Map.width; x++)
            {
                for (var y = 0; y < Map.height; y++)
                {
                    grid[x, y] = Map.GetPixel(x, y).linear != Color.white;
                }
            }
            blueprint.PathMap = grid;
        }

        private Texture2D ScaleTexture(Texture2D originalTexture, int scale)
        {
            var scaledTexture = new Texture2D(originalTexture.width*scale, originalTexture.height*scale);
            for (var x = 0; x < scaledTexture.width; x++)
            {
                for (var y = 0; y < scaledTexture.height; y++)
                {
                    var pixel = originalTexture.GetPixel(x/scale, y/scale);
                    scaledTexture.SetPixel(x, y, pixel);
                }
            }
            return scaledTexture;
        }

        private GameObject BuildForrest()
        {
            var forrestTiles = new GameObject("ForrestTiles");
            for (var x = -ForrestOffset; x < mapWidth + ForrestOffset; x++)
            {
                for (var y = -ForrestOffset; y < mapHeight + ForrestOffset; y++)
                {
                    if (!blueprint.InBounds((float) x/TileSize, (float) y/TileSize))
                    {
                        PlaceTile(x, y, mapWidth, mapHeight, 1, ForrestTile, forrestTiles);
                    }
                }
            }
            return forrestTiles;
        }

        private GameObject BuildTiles()
        {
            var tiles = new GameObject("Tiles");
            for (var x = 0; x < mapWidth; x++)
            {
                for (var y = 0; y < mapHeight; y++)
                {
                    if (!blueprint.PathMap[x/TileSize, y/TileSize])
                    {
                        PlaceTile(x, y, mapWidth, mapHeight, 1, Tile, tiles);
                    }
                }
            }
            return tiles;
        }

        private static void PlaceTile(int x, int y, int width, int height, int size, GameObject tile, GameObject parent)
        {
            var groundTile = Instantiate(tile);
            groundTile.name += " [" + x + ", " + y + "]";
            groundTile.transform.localPosition = new Vector3(x - width + 0.5f, 0, y - height + 0.5f)*size;
            groundTile.transform.SetParent(parent.transform);
        }

        private static Vector3 GetObjectPostion(Texture2D m, Color color)
        {
            var posXy = GetRect(m, color).center;
            return new Vector3(posXy.x - m.width, 0, posXy.y - m.height);
        }

        private static Rect GetRect(Texture2D tex, Color color)
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