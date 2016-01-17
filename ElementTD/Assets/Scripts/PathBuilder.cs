using System;
using System.Collections.Generic;
using Assets.Shared.Scripts;
using UnityEngine;

public class PathBuilder
{
    public enum Dir
    {
        Straigth,
        Left,
        Right
    }

    private static float QUADRANT = (float) (Math.PI/2.0);
    private Vector2 position;
    private Vector2 currentDirection;

    private readonly int segmentCount;
    private readonly float tileSize;
    private readonly float pathWidth;
    private readonly List<List<Vector2>> paths;
    private readonly float pathsSpacing;

    public PathBuilder(int pathsCount, int segmentCount, float tileSize, float pathWidth)
    {
        this.segmentCount = segmentCount;
        this.tileSize = tileSize;
        this.pathWidth = pathWidth;

        paths = createPaths(pathsCount);
        pathsSpacing = pathWidth / (pathsCount - 1);
    }

    private static List<List<Vector2>> createPaths(int pathsCount)
    {
        var paths = new List<List<Vector2>>();
        for (var i = 0; i < pathsCount; i++)
        {
            paths.Add(new List<Vector2>());
        }

        return paths;
    }

    public List<List<Vector2>> Build(List<Dir> commands, Vector2 direction)
    {
        // Initial path node
        CreatePoint(direction, position);

        foreach (var command in
            commands)
        {
            switch (command)
            {
                case Dir.Straigth:
                    CreateStraight();
                    break;
                case Dir.Left:
                    CreateTurn(QUADRANT, segmentCount);
                    break;
                case Dir.Right:
                    CreateTurn(-QUADRANT, segmentCount);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        return paths;
    }

    private void CreateStraight()
    {
        CreatePoint(currentDirection, position + currentDirection*tileSize);
    }

    private void CreateTurn(float angle, int segments)
    {
        Vector2 direction = Quaternion.AngleAxis(angle*Mathf.Rad2Deg, Vector3.forward)*currentDirection;
        var normalOffset = direction*tileSize/2f;
        var corner = position + normalOffset;
        var startAngle = Mathf.Atan2(currentDirection.y, currentDirection.x) - Math.Sign(angle)*QUADRANT;

        for (var i = 1; i <= segments; i++)
        {
            var d = startAngle + angle/ segments * i;
            var x = (float) (corner.x + Math.Cos(d)*tileSize/2f);
            var y = (float) (corner.y + Math.Sin(d)*tileSize/2f);
            var point = new Vector2(x, y);

            var cornerNormal = -(point - corner).normalized.Normal();
            CreatePoint(cornerNormal*-Math.Sign(angle), point);
        }
    }

    private void CreatePoint(Vector2 direction, Vector2 origin)
    {
        currentDirection = direction.normalized;
        position = origin;
        if (paths.Count == 1)
        {
            paths[0].Add(position);
        }
        else
        {
            var normal = currentDirection.Normal();

            var normalOffset = -normal*pathWidth/2f;
            for (var i = 0; i < paths.Count; i++)
            {
                paths[i].Add(position + normalOffset + normal*(pathsSpacing*i));
            }
        }
    }
}