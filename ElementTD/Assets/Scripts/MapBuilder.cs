using System;
using Assets.Scripts;
using Assets.Shared.Scripts;
using Pathfinding;
using UnityEngine;

[RequireComponent(typeof (TowerMap), typeof(AstarPath), typeof(BoxCollider))]
public class MapBuilder : MonoBehaviour
{
    public Texture2D Map;

    public int ForrestOffset;
    public GameObject Tile;
    public GameObject ForrestTile;

    public void Build()
    {
        transform.RemoveAllChildren();
        BuildMap();

        GetComponent<BoxCollider>().size = new Vector3(Map.width, 0 , Map.height);


        var path = new GameObject("Path");
        path.transform.SetParent(transform);

        var map = GetComponent<TowerMap>();
        Fill(
            Map.width/2,
            Map.height/2,
            (x, y) =>
            {
                if (map.PathMap[x*2, y*2])
                {
                    var node = new GameObject("PathNode");
                    node.transform.localPosition = new Vector3(x - Map.width / 4, 0, y - Map.height / 4) * 2;
                    node.transform.SetParent(path.transform);
                }
            });

        var astar = gameObject.GetComponent<AstarPath>();
        var graph = astar.graphs[0] as PointGraph;
        graph.root = path.transform;

        astar.Scan();
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

                map.PathMap[x, y] = pixel.linear == Color.black;

                if (pixel.linear != Color.black)
                {
                    PlaceTile(x, y, Map.width, Map.height, Tile, tiles);
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
                    PlaceTile(x, y, Map.width, Map.height, ForrestTile, forrestTiles);
                }
            });
    }

    private void PlaceTile(int x, int y, int width, int height, GameObject tile, GameObject parent)
    {
        var groundTile = Instantiate(tile);
        groundTile.transform.localPosition = new Vector3(x - width/2 + 0.5f, 0, y - height/2 + 0.5f);
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
}