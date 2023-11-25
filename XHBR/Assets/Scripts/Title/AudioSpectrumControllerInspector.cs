using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(AudioSpectrumController))]
public class AudioSpectrumControllerInspector : Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField("Dependencies", EditorStyles.boldLabel);
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty(AudioSpectrumController.AudioSourceName));
        }

        EditorGUILayout.Space(10);

        EditorGUILayout.LabelField("Compositions", EditorStyles.boldLabel);
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty(AudioSpectrumController.RingImgAssetRefName));
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif
