using UnityEditor;

#if UNITY_EDITOR

[CustomEditor(typeof(SplashTimelineController))]
public class SplashTimelineControllerInspector : Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField("Dependencies", EditorStyles.boldLabel);
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty(SplashTimelineController.CanvasName));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(SplashTimelineController.DirectorName));
        }

        EditorGUILayout.Space(10);

        EditorGUILayout.LabelField("Compositions", EditorStyles.boldLabel);
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty(SplashTimelineController.RcmdMsgPrfbName));
        }

        serializedObject.ApplyModifiedProperties();
    }
}

#endif