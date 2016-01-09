using Assets.Scripts;
using UnityEditor;
using UnityEngine;

namespace Assets.Integration.Editor
{
    [CustomEditor(typeof (TowerMap))]
    public class TowerMapEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var towerMap = (TowerMap) target;
            var tileSize = new Vector2(50, 50);
            GUILayout.Space(8);
            var rect = EditorGUILayout.GetControlRect();
            for (var i = 0; i < towerMap.Map.Width; i++)
            {
                for (var j = 0; j < towerMap.Map.Height; j++)
                {
                    var position = new Rect(rect.x + i*tileSize.x, rect.y + j*tileSize.y, tileSize.x, tileSize.y);
                    towerMap.Map[i, j] = !GUI.Toggle(position, !towerMap.Map[i, j], "", "Button");
                }
            }
            GUILayout.Space(towerMap.Map.Height*tileSize.y);

            if (GUI.changed)
            {
                EditorUtility.SetDirty(towerMap);
            }
        }
    }
}