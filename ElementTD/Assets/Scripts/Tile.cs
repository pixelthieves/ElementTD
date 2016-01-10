using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof (CanvasGroup))]
    public class Tile : MonoBehaviour
    {
        public delegate void OnClickedHandler(Tile tile);

        public int X;
        public int Y;

        public bool Visible
        {
            get { return GetComponent<CanvasGroup>().alpha != 0; }
            set { GetComponent<CanvasGroup>().alpha = value ? 1 : 0; }
        }

        public event OnClickedHandler OnClicked;

        public void OnTileClicked()
        {
            if (OnClicked != null) OnClicked(this);
        }
    }
}