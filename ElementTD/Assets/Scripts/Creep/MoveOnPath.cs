using System;
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
        public event Action OnPathEndReached;

        private List<List<Vector3>> Path;
        private PathView path;
        private Vector3? goal;

        private void Start()
        {
            path = GetComponent<PathView>();
            goal = path.NextPoint;
        }
        private void Update()
        {
            if (goal == null)
            {
                if (path.next())
                {
                    goal = path.NextPoint;

                }
                else
                {
                    return;
                }
            }

            var time = Time.deltaTime;

            while (true)
            {
                float distance = goal.Value.Distance(transform.position);
                if (distance >= time * speed * path.SegmentLengthRatio) break;

                move(distance);
                if (!path.next())
                {
                    if (OnPathEndReached != null)
                    {
                        OnPathEndReached();
                    }
                    goal = null;
                    return;
                }
                goal = path.NextPoint;

                time -= distance / (speed * path.SegmentLengthRatio);
            }
            move(time * speed * path.SegmentLengthRatio);
        }

        private void move(float travelAbility)
        {
            var destination = transform.position + travelAbility * (goal.Value - transform.position).normalized;

            // TODO Smooth the rotation
            transform.LookAt(destination, Vector3.up);
            transform.position = destination;
        }
    }
}