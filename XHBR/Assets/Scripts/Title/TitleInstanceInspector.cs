using UnityEditor;

#if UNITY_EDITOR

[CustomEditor(typeof(TitleInstance))]
public class TitleInstanceInspector : Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField("Compositions", EditorStyles.boldLabel);
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty(TitleInstance.SplashTimelinePrpbName));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(TitleInstance.AudioClipAssetRefName));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(TitleInstance.TitleBckgPrpbName));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(TitleInstance.TitleAudioSpectrumPrpbName));
        }

        serializedObject.ApplyModifiedProperties();
    }
}

#endif