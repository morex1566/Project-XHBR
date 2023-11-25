using UnityEditor;

#if UNITY_EDITOR

[CustomEditor(typeof(SplashRcmdMsgController))]
public class SplashRcmdMsgControllerInspector : Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField("Dependencies", EditorStyles.boldLabel);
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty(SplashRcmdMsgController.BackgroundImgName));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(SplashRcmdMsgController.IconImgName));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(SplashRcmdMsgController.MsgTMPName));
        }

        EditorGUILayout.Space(10);

        EditorGUILayout.LabelField("Compositions", EditorStyles.boldLabel);
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty(SplashRcmdMsgController.IconAssetRefName));
        }

        serializedObject.ApplyModifiedProperties();
    }
}

#endif