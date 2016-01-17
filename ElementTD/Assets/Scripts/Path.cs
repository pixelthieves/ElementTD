using System.Collections.Generic;
using System.Linq;
using Assets.Shared.Scripts;
using UnityEngine;

namespace Assets.Scripts
{
    public class Path : MonoBehaviour
    {
        private List<List<Vector3>> worldPath;

        public void Init(TowerMap map, int x, int y,  int nodeSize)
        {
            var commands = CreatePath(map, x, y);
            //TODO pass direction as argument
            var path = new PathBuilder(10, 15, nodeSize, 1).Build(commands, Vector2.down);

            var initialPostion = Vector2.zero;// nodes[0].position - Vector3.forward;

            worldPath = PathToWorld(initialPostion, path);
        }

        private List<PathBuilder.Dir> CreatePath(TowerMap map, int x, int y)
        {
            var directions = new List<PathBuilder.Dir>();

            CreatePath(directions, Vector2.down, map, new Vector2( x, y));

            return directions;

        }

        private void CreatePath(List<PathBuilder.Dir> directions, Vector2 currentDir, TowerMap map, Vector2 position)
        {
            var right = new Vector2( 1, 0);
            var up = new Vector2(0,  1);
            var left = new Vector2(-1, 0);
            var down = new Vector2(0,  -1);

            ValidateDir(directions, currentDir, position, map, right);
            ValidateDir(directions, currentDir, position,map, up);
            ValidateDir(directions, currentDir, position,map, left);
            ValidateDir(directions, currentDir, position,map, down);
            
        }

        private void ValidateDir(List<PathBuilder.Dir> directions, Vector2 currentDir, Vector2 position, TowerMap map, Vector2 newDirection)
        {
            if (map.IsPath((int)(position.x+newDirection.x), (int) (position.y+newDirection.y)))
            {
                var angle = Mathf.DeltaAngle(Mathf.Atan2(currentDir.y, currentDir.x)*Mathf.Rad2Deg,
                    Mathf.Atan2(newDirection.y, newDirection.x)*Mathf.Rad2Deg);

                if (angle == -90) directions.Add(PathBuilder.Dir.Right);
                else if (angle == 90) directions.Add(PathBuilder.Dir.Left);
                else if (angle == 0) directions.Add(PathBuilder.Dir.Straigth);
                else return;

                currentDir = newDirection;
                CreatePath(directions, currentDir, map, newDirection+position);
            }
        }
        
        private List<List<Vector3>> PathToWorld(Vector3 initialPostion, List<List<Vector2>> path)
        {
            return path.Select(o => o.Select(v => initialPostion + v.FromXZ()).ToList()).ToList();
        }

        public void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            foreach (var path in worldPath)
            {
                for (var i = 0; i < path.Count - 1; i++)
                {
                    Gizmos.DrawLine(transform.position+path[i], transform.position + path[i + 1]);
                }
            }
        }
    }
}