using System;
using Assets.Scripts;
using Assets.Shared.Scripts;
using Pathfinding;
using UnityEngine;

[RequireComponent(typeof (TowerMap), typeof (AstarPath), typeof (BoxCollider))]
public class MapBuilder : MonoBehaviour
{
    public Texture2D Map;

    public int tileSize;
    public int ForrestOffset;

    public GameObject Start;
    public GameObject End;

    public GameObject Node;
    public GameObject Tile;
    public GameObject ForrestTile;

    public void Build()
    {
        transform.RemoveAllChildren();
        BuildMap();

        GetComponent<BoxCollider>().size = new Vector3(Map.width, 0, Map.height);

        var path = new GameObject("Path");
        path.transform.SetParent(transform);


        var pathMapWidth = Map.width/2;
        var pathMapHeight = Map.height/2;
        var map = GetComponent<TowerMap>();
        Fill(
            pathMapWidth,
            pathMapHeight,
            (x, y) =>
            {
                if (map.PathMap[x*2, y*2])
                {
                    PlaceTile(x, y, pathMapWidth, pathMapHeight, 2, Node, path);
                }
            });

        //TODO I have no idea
        path.transform.Translate(-1, 0, -1);

        var astar = gameObject.GetComponent<AstarPath>();
        var graph = astar.graphs[0] as PointGraph;
        graph.root = path.transform;

        astar.Scan();

        PlacePathEnd(Start, Color.red);
        PlacePathEnd(End, Color.blue);
    }

    private void PlacePathEnd(GameObject end, Color color)
    {
        var go = Instantiate(end);
        var posXy = GetRect(Map, color).center;
        go.transform.localPosition = new Vector3(posXy.x - Map.width/2, 0, posXy.y - Map.height / 2);
        go.transform.SetParent(transform);
    }

    private void BuildMap()
    {
        var map = GetComponent<TowerMap>();
        map.PathMap = new bool[Map.width, Map.height];

        var tiles = new GameObject("Tiles");
        tiles.transform.SetParent(transform);

        Fill(
            Map.width,
            Map.height,
            (x, y) =>
            {
                var pixel = Map.GetPixel(x, y);

                map.PathMap[x, y] = pixel.linear != Color.white;

                if (!map.PathMap[x, y])
                {
                    PlaceTile(x, y, Map.width, Map.height, 1, Tile, tiles);
                }
            });

        var forrestTiles = new GameObject("ForrestTiles");
        forrestTiles.transform.SetParent(transform);

        Fill(
            -ForrestOffset,
            -ForrestOffset,
            Map.width + ForrestOffset,
            Map.height + ForrestOffset,
            (x, y) =>
            {
                if (!map.InBounds(x, y))
                {
                    PlaceTile(x, y, Map.width, Map.height, 1, ForrestTile, forrestTiles);
                }
            });
    }

    private void PlaceTile(int x, int y, int width, int height, int size, GameObject tile, GameObject parent)
    {
        var groundTile = Instantiate(tile);
        groundTile.transform.localPosition = new Vector3(x - width/2 + 0.5f, 0, y - height/2 + 0.5f)*size;
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
                    rect.xMax = Math.Max(rect.xMax, x+1);
                    rect.yMax = Math.Max(rect.yMax, y+1);
                }
            }
        }
        return rect;
    }
}