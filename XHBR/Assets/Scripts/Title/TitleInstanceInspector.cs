using Assets.Scripts.Title;
using UnityEditor;

#if UNITY_EDITOR

[CustomEditor(typeof(TitleInstance))]
public class TitleInstanceInspector : Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField("Compositions", EditorStyles.boldLabel);
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty(TitleInstance.SplashTimelineObjName));
        }

        serializedObject.ApplyModifiedProperties();
    }
}

#endif