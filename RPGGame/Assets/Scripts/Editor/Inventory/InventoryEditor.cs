using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Inventory))]
public class InventoryEditor : Editor {
    private SerializedProperty ItemImagesProperty;
    private SerializedProperty ItemsProperty;
    private bool[] ShowItemSlots = new bool[Inventory.NumItemSlots];

    private const string InventoryPropItemImagesName = "ItemSlots";
    private const string InventoryPropItemsName = "Items";

    private void OnEnable()
    {
        ItemImagesProperty = serializedObject.FindProperty(InventoryPropItemImagesName);
        ItemsProperty = serializedObject.FindProperty(InventoryPropItemsName);

    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        for (int i = 0; i < Inventory.NumItemSlots; i++)
        {
            ItemSlotGUI(i);
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void ItemSlotGUI(int index)
    {
        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUI.indentLevel++;

        ShowItemSlots[index] = EditorGUILayout.Foldout(ShowItemSlots[index], "Item Slot "+ index);

        if (ShowItemSlots[index])
        {
            EditorGUILayout.PropertyField(ItemImagesProperty.GetArrayElementAtIndex(index));
            EditorGUILayout.PropertyField(ItemsProperty.GetArrayElementAtIndex(index));
        }

        EditorGUI.indentLevel--;
        EditorGUILayout.EndVertical();
    }

}
