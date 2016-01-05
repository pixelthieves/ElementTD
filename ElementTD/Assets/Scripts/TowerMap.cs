using System;
using Assets.Shared.Scripts;
using TouchScript.Gestures;
using TouchScript.Hit;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof (Collider), typeof (TapGesture))]
    public class TowerMap : MonoBehaviour
    {
        public int Width;
        public int Height;

        private Tower[,] map;
        private Bounds bounds;

        private void Start()
        {
            bounds = GetComponent<Collider>().bounds;
            map = new Tower[Width, Height];
        }

        private void OnEnable()
        {
            GetComponent<TapGesture>().Tapped += OnTapped;
        }

        private void OnDisable()
        {
            GetComponent<TapGesture>().Tapped -= OnTapped;
        }

        private void OnTapped(object sender, EventArgs e)
        {
            var gesture = sender as TapGesture;
            TouchHit hit;
            gesture.GetTargetHitResult(out hit);

            var normalizedPoint = (hit.Point - bounds.min).Divide(bounds.size);
            OnTileTapped((int) (normalizedPoint.x*Width), (int) (normalizedPoint.z*Height));
        }

        private void OnTileTapped(int x, int y)
        {
            Debug.Log("Tile Tapped on [" + x + "," + y + "]");
        }
    }
}