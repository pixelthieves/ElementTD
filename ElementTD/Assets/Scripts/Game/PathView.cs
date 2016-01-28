using Assets.Shared.Scripts;
using UnityEngine;

namespace Game
{
    public class PathView : MonoBehaviour
    {
        public Path path;

        private int i;

        private int CurrentPathIndex
        {
            get
            {
                var closest = 0;
                var closestDistance = float.MaxValue;

                for (var j = 0; j < path.Width; j++)
                {
                    var currentDistance = transform.position.Distance(path.WorldPath[j][i]);
                    if (currentDistance < closestDistance)
                    {
                        closestDistance = currentDistance;
                        closest = j;
                    }
                }
                return closest;
            }
        }

        public Vector3 PrevPoint
        {
            get
            {
                return path.WorldPath[CurrentPathIndex][i-1];
            }
        }

        public Vector3 NextPoint
        {
            get
            {
                return path.WorldPath[CurrentPathIndex][i];
            }
        }

        public Vector3 PrevCenterPoint
        {
            get
            {
                var p = path.WorldPath;
                var w = path.Width;
                return p[0][i - 1].HalfWay(p[w - 1][i - 1]);
            }
        }

        public Vector3 NextCenterPoint
        {
            get
            {
                var p = path.WorldPath;
                var w = path.Width;
                return p[0][i].HalfWay(p[w - 1][i]);
            }
        }

        public float SegmentLengthRatio
        {
            get
            {
                if (i == 0) return 1;

                var baseDistance = PrevCenterPoint.Distance(NextCenterPoint);
                var currentDistance = PrevPoint.Distance(NextPoint);
                return currentDistance / baseDistance;
            }
        }

        public bool next()
        {
            if (path.Length <= i - 1) return false;

            i++;
            return true;
        }
    }
}