using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Palmmedia.ReportGenerator.Core;

[CustomEditor(typeof(VRIKScalerCalibrator))]
public class VRIKScalerCalibratorEditor : Editor
{
    // Serialized Properties
    SerializedProperty mode;
    SerializedProperty modelHeight;
    SerializedProperty userHeight;
    SerializedProperty modelEyeHeight;
    SerializedProperty hmd;


    private void OnEnable()
    {
        mode = serializedObject.FindProperty("mode");
        modelHeight = serializedObject.FindProperty("modelHeight");
        userHeight = serializedObject.FindProperty("userHeight");

        modelEyeHeight = serializedObject.FindProperty("modelEyeHeight");
        hmd = serializedObject.FindProperty("hmd");
    }

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        serializedObject.Update();

        VRIKScalerCalibrator calibrator = (VRIKScalerCalibrator) target;

        // Script Field
        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour((MonoBehaviour)target), typeof(MonoScript), false);
        EditorGUILayout.ObjectField("Editor", MonoScript.FromScriptableObject(this), typeof(MonoScript), false);
        EditorGUI.EndDisabledGroup();

        //EditorGUILayout.Space();
        GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));

        EditorGUILayout.PropertyField(mode);

        GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));

        switch (calibrator.mode)
        {
            case VRIKScalerCalibrator.CalibrateMode.UseHeight:
                EditorGUILayout.PropertyField(modelHeight);
                EditorGUILayout.PropertyField(userHeight);
                break;
            case VRIKScalerCalibrator.CalibrateMode.UseHMD:
                EditorGUILayout.HelpBox("Keep the user standing up straight. This function is only effective during play.", MessageType.Info);
                EditorGUILayout.PropertyField(modelEyeHeight);
                EditorGUILayout.PropertyField(hmd);
                break;
            default:
                EditorGUILayout.PropertyField(modelHeight);
                EditorGUILayout.PropertyField(userHeight);
                EditorGUILayout.PropertyField(modelEyeHeight);
                EditorGUILayout.PropertyField(hmd);
                break;
        }

        using (new EditorGUILayout.HorizontalScope())
        {
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Calibrate", GUILayout.Width(100f)))
            {
                calibrator.Calibrate();
            }
            if (GUILayout.Button("Reset", GUILayout.Width(100f)))
            {
                calibrator.Reset();
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
}
