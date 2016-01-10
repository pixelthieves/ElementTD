using System;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    public struct Map
    {
        public int Width;
        public int Height;

        [SerializeField] [HideInInspector] private bool[] Data;

        public bool this[int x, int y]
        {
            get
            {
                Ensure();
                return Data[y*Width + x];
            }
            set
            {
                Ensure();
                Data[y*Width + x] = value;
            }
        }

        public bool InBounds(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }

        public void Ensure()
        {
            if (Data == null) Data = new bool[Width*Height];
            if (Data.Length != Width*Height)
                Data = Enumerable.Repeat(true, Width*Height).ToArray();
        }

        public static void ResizeBidimArrayWithElements<T>(ref T[,] original, int rows, int cols)
        {
            var newArray = new T[rows, cols];
            var minX = Math.Min(original.GetLength(0), newArray.GetLength(0));
            var minY = Math.Min(original.GetLength(1), newArray.GetLength(1));

            for (var i = 0; i < minX; ++i)
                Array.Copy(original, i*original.GetLength(1), newArray, i*newArray.GetLength(1), minY);

            original = newArray;
        }
    }
}