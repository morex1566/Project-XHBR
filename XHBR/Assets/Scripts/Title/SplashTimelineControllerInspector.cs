using UnityEditor;

#if UNITY_EDITOR

namespace Assets.Scripts.Title
{
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
                EditorGUILayout.PropertyField(serializedObject.FindProperty(SplashTimelineController.RecommendationMsgObjName));
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}

#endif