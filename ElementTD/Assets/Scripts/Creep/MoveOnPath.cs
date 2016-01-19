using System.Collections.Generic;
using Assets.Shared.Scripts;
using Game;
using UnityEngine;

namespace Creep
{
    [RequireComponent(typeof(PathView))]
    public class MoveOnPath : MonoBehaviour
    {
        public float speed;

        private List<List<Vector3>> Path;
        private PathView path;
        private Vector3 goal;

        private void Start()
        {
            path = GetComponent<PathView>();
            goal = path.Goal;
        }
        private void Update()
        {
            float travelAbility = speed*Time.deltaTime;

            while (true)
            {
                float distance = goal.Distance(transform.position);
                if (distance >= travelAbility) break;

                move(distance);
                if (!path.next())
                {
                    // TODO End of path
                }
                goal = path.Goal;
                travelAbility -= distance;
            }
            move(travelAbility);
        }

        private void move(float travelAbility)
        {
            transform.position += travelAbility*(goal - transform.position).normalized;
        }
    }
}