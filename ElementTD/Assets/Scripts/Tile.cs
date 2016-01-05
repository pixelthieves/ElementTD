using UnityEngine;

namespace Assets.Scripts
{
    public class Tile : MonoBehaviour
    {
        public delegate void OnClickedHandler(int x, int y);

        public int X;
        public int Y;

        public event OnClickedHandler OnClicked;

        public void OnTileClicked()
        {
            if (OnClicked != null) OnClicked(X, Y);
        }
    }
}