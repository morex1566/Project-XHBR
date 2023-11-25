using UnityEditor;

#if UNITY_EDITOR

[CustomEditor(typeof(GameInstance))]
public class GameInstanceInspector : Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField("Compositions", EditorStyles.boldLabel);
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty(GameInstance.FontAssetRef));
        }

        serializedObject.ApplyModifiedProperties();
    }
}

#endif