using Assets.Shared.Scripts;
using UnityEngine;

namespace Game
{
    public class PathView : MonoBehaviour
    {
        public Path path;

        private int i;

        public Vector3 Goal
        {
            get
            {
                var closest = new Vector3();
                var closestDistance = float.MaxValue;

                for (var j = 0; j < path.Width; j++)
                {
                    var currentDistance = transform.position.Distance(path.WorldPath[j][i]);
                    if (currentDistance < closestDistance)
                    {
                        closestDistance = currentDistance;
                        closest = path.WorldPath[j][i];
                    }
                }
                return closest;
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