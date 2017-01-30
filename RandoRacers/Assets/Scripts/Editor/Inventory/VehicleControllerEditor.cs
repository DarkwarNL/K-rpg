using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(VehicleController))]
public class VehicleControllerEditor : Editor {
    private SerializedProperty WheelMeshProperty;
    private SerializedProperty WheelColProperty;
    private bool[] ShowWheels = new bool[VehicleController.WheelAmount];

    private const string VehicleControllerWheelMeshName = "_WheelMeshes";
    private const string VehicleControllerWheelColName = "_WheelColliders";

    private void OnEnable()
    {
        WheelMeshProperty = serializedObject.FindProperty(VehicleControllerWheelMeshName);
        WheelColProperty = serializedObject.FindProperty(VehicleControllerWheelColName);
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        for (int i = 0; i < VehicleController.WheelAmount; i++)
        {
            WheelSlotGUI(i);
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void WheelSlotGUI(int index)
    {
        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUI.indentLevel++;

        ShowWheels[index] = EditorGUILayout.Foldout(ShowWheels[index], "Wheel "+ index);

        if (ShowWheels[index])
        {
            EditorGUILayout.PropertyField(WheelMeshProperty.GetArrayElementAtIndex(index));
            EditorGUILayout.PropertyField(WheelColProperty.GetArrayElementAtIndex(index));
        }

        EditorGUI.indentLevel--;
        EditorGUILayout.EndVertical();
    }
}
