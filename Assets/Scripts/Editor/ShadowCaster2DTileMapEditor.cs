
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(ShadowMap))]
    public class ShadowCaster2DTileMapEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Create"))
            {
                var creator = (ShadowMap)target;
                creator.Create();
            }

            if (GUILayout.Button("Remove Shadows"))
            {
                var creator = (ShadowMap)target;
                creator.DestroyOldShadowCasters();
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}