using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(VRIKTargetGenerator))]
public class VRIKTargetGeneratorEditor : Editor
{
    // Serialized Properties
    SerializedProperty OVRCameraRig;
    SerializedProperty handTrackingType;
    SerializedProperty vrik;

    // For foldout
    bool isOpen;

    private void OnEnable()
    {
        OVRCameraRig = serializedObject.FindProperty("OVRCameraRig");
        handTrackingType = serializedObject.FindProperty("handTrackingType");
        vrik = serializedObject.FindProperty("vrik");
    }

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        serializedObject.Update();

        VRIKTargetGenerator generator = (VRIKTargetGenerator) target;

        // Script Field
        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour((MonoBehaviour)target), typeof(MonoScript), false);
        EditorGUILayout.ObjectField("Editor", MonoScript.FromScriptableObject(this), typeof(MonoScript), false);
        EditorGUI.EndDisabledGroup();

        //EditorGUILayout.Space();
        GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));

        // Generate Targets
        EditorGUILayout.LabelField("Generate / Update Targets in OVRCameraRig", EditorStyles.boldLabel);

        EditorGUILayout.PropertyField(OVRCameraRig);

        EditorGUILayout.PropertyField(handTrackingType);
        
        using (new EditorGUILayout.HorizontalScope())
        {
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Generate", GUILayout.Width(100f)))
            {
                generator.GenerateTargets();
            }
        }

        //EditorGUILayout.Space();
        GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));

        // Register Targets
        EditorGUILayout.LabelField("Register Targets to VRIK", EditorStyles.boldLabel);

        EditorGUILayout.PropertyField(vrik);
        
        using (new EditorGUILayout.HorizontalScope())
        {
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Register", GUILayout.Width(100f)))
            {
                generator.RegisterTargets();
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
}
