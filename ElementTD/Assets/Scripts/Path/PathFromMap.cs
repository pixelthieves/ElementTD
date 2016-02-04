using System.Collections.Generic;
using System.Linq;
using Assets.Shared.Scripts;
using Tower;
using UnityEngine;

namespace Game
{
    public class PathFromMap : MonoBehaviour
    {
        public static List<List<Vector3>> Build(Blueprint map, int x, int y, PathBuilder.Settings pathSettings)
        {
            var commands = CreatePath(map, x, y);
            //TODO pass direction as argument
            var path = new PathBuilder(pathSettings).Build(commands, Vector2.down);
            var initialPostion = Vector2.zero;
            return PathToWorld(initialPostion, path);
        }

        private static List<PathBuilder.Dir> CreatePath(Blueprint map, int x, int y)
        {
            var directions = new List<PathBuilder.Dir>();
            CreatePath(directions, Vector2.down, map, new Vector2(x, y));
            return directions;
        }

        private static void CreatePath(List<PathBuilder.Dir> directions, Vector2 currentDir, Blueprint map, Vector2 position)
        {
            var right = new Vector2(1, 0);
            var up = new Vector2(0, 1);
            var left = new Vector2(-1, 0);
            var down = new Vector2(0, -1);

            ValidateDir(directions, currentDir, position, map, right);
            ValidateDir(directions, currentDir, position, map, up);
            ValidateDir(directions, currentDir, position, map, left);
            ValidateDir(directions, currentDir, position, map, down);
        }

        private static void ValidateDir(List<PathBuilder.Dir> directions, Vector2 currentDir, Vector2 position, Blueprint map,
            Vector2 newDirection)
        {
            if (map.IsPath((int) (position.x + newDirection.x), (int) (position.y + newDirection.y)))
            {
                var angle = Mathf.DeltaAngle(Mathf.Atan2(currentDir.y, currentDir.x)*Mathf.Rad2Deg,
                    Mathf.Atan2(newDirection.y, newDirection.x)*Mathf.Rad2Deg);

                if (angle == -90) directions.Add(PathBuilder.Dir.Right);
                else if (angle == 90) directions.Add(PathBuilder.Dir.Left);
                else if (angle == 0) directions.Add(PathBuilder.Dir.Straigth);
                else return;

                currentDir = newDirection;
                CreatePath(directions, currentDir, map, newDirection + position);
            }
        }

        private static List<List<Vector3>> PathToWorld(Vector3 initialPostion, List<List<Vector2>> path)
        {
            return path.Select(o => o.Select(v => initialPostion + v.ToXZ()).ToList()).ToList();
        }
    }
}