using Assets.Scripts;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor
{
    [CustomEditor(typeof(MapBuilder))]
    public class MapBuilderEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Build")) (target as MapBuilder).Build();
        }
    }
}