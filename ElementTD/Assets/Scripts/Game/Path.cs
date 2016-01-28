using System.Collections.Generic;
using Assets.Shared.Scripts;
using UnityEngine;
using Vexe.Runtime.Types;
// for BetterBehaviour, BetterScriptableObject and uAction

namespace Game
{
    public class Path : BetterBehaviour
    {
        public List<List<Vector3>> WorldPath { get; private set; }
        public int Length { get; private set; }
        public int Width { get; private set; }

        public Vector3 FirstRightPoint
        {
            get { return WorldPath[Width - 1][0]; }
        }

        public Vector3 FirstLeftPoint
        {
            get { return WorldPath[0][0]; }
        }

        public void Init(List<List<Vector3>> path)
        {
            WorldPath = path;
            Length = path[0].Count;
            Width = path.Count;
        }

        public void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;

            foreach (var path in WorldPath)
            {
                for (var i = 0; i < path.Count - 1; i++)
                {
                    Gizmos.DrawLine(transform.position + path[i], transform.position + path[i + 1]);
                }
            }
        }
    }
}