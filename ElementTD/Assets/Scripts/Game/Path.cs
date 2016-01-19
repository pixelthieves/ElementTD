using Vexe.Runtime.Types; // for BetterBehaviour, BetterScriptableObject and uAction
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Path : BetterBehaviour
    {
        public List<List<Vector3>> WorldPath { get; private set; }
        public int Length { get; private set; }
        public int Width { get; private set; }

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