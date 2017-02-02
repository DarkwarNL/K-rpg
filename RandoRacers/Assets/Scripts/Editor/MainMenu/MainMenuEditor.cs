using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MainMenu))]
public class MainMenuEditor : Editor {
    private SerializedProperty GameSettingsButtonProperty;
    private SerializedProperty OptionsButtonProperty;

    /// <summary>
    /// Game Settings
    /// </summary>
    private const int RGBCount = 3;
    private bool ShowGameSettings;
    private bool ShowPlayerOne;
    private bool ShowPlayerOneRGB;
    private bool ShowPlayerTwo;
    private bool ShowPlayerTwoRGB;

    private SerializedProperty PlayerCountSliderProperty;
    private SerializedProperty PlayerValueTextProperty;    
    private SerializedProperty PlayerOneNameInputProperty;
    private SerializedProperty PlayerOneRGBSlidersProperty;
    private SerializedProperty PlayerOneColorImageProperty;    
    private SerializedProperty PlayerTwoNameInputProperty;
    private SerializedProperty PlayerTwoRGBSlidersProperty;
    private SerializedProperty PlayerTwoColorImageProperty;
    private SerializedProperty LapCountSliderProperty;
    private SerializedProperty LapValueTextProperty;
    private SerializedProperty StartGameButtonProperty;
    private SerializedProperty GameSettingsBackButtonProperty;

    /// <summary>
    /// Options
    /// </summary>
    private bool ShowOptions;
    private SerializedProperty VolumeSliderProperty;
    private SerializedProperty VolumeValueTextProperty;
    private SerializedProperty GameQualitySliderProperty;
    private SerializedProperty GameQualityTextProperty;
    private SerializedProperty OptionsBackButtonProperty;

    private bool ShowPanels;
    private SerializedProperty CurrentMenuProperty;
    private SerializedProperty MainMenuPanelProperty;
    private SerializedProperty GameSettingsPanelProperty;
    private SerializedProperty OptionsPanelProperty;

    /// <summary>
    /// MainMenu Button names
    /// </summary>
    private const string MainMenuGameSettingsButtonName = "_GameSettingsButton";
    private const string MainMenuOptionsButtonName = "_OptionsButton";

    /// <summary>
    /// Game Settings names
    /// </summary>
    private const string MainMenuPlayerCountSliderName = "_PlayerCountSlider";
    private const string MainMenuPlayerValueTextName = "_PlayerValueText";
    private const string MainMenuPlayerOneNameInputName = "_PlayerOneNameInput";
    private const string MainMenuPlayerOneRGBSlidersName = "_PlayerOneRGBSliders";
    private const string MainMenuPlayerOneColorImageName = "_PlayerOneColorImage";
    private const string MainMenuPlayerTwoNameInputName = "_PlayerTwoNameInput";
    private const string MainMenuPlayerTwoRGBSlidersName = "_PlayerTwoRGBSliders";
    private const string MainMenuPlayerTwoColorImageName = "_PlayerTwoColorImage";
    private const string MainMenuLapCountSliderName = "_LapCountSlider";
    private const string MainMenuLapValueTextName = "_LapValueText";
    private const string MainMenuStartGameButtonName = "_StartGameButton";
    private const string MainMenuGameSettingsBackButtonName = "_GameSettingsBackButton";

    /// <summary>
    /// Option names
    /// </summary>
    private const string MainMenuVolumeSliderName = "_VolumeSlider";
    private const string MainMenuVolumeValueTextName = "_VolumeValueText";
    private const string MainMenuGameQualitySliderName = "_GameQualitySlider";
    private const string MainMenuGameQualityTextName = "_GameQualityText";
    private const string MainMenuOptionsBackButtonName = "_OptionsBackButton";

    /// <summary>
    /// Panel Names
    /// </summary>
    private const string MainMenuCurrentMenuName = "_CurrentMenu";
    private const string MainMenuPanelName = "_MainMenuPanel";
    private const string MainMenuGameSettingsPanelName = "_GameSettingsPanel";
    private const string MainMenuOptionsPanelName = "_OptionsPanel";

    private SerializedProperty _Players;

    private void OnEnable()
    {
        _Players = serializedObject.FindProperty("_Players");
        //Game Settings
        PlayerCountSliderProperty = serializedObject.FindProperty(MainMenuPlayerCountSliderName);
        PlayerValueTextProperty = serializedObject.FindProperty(MainMenuPlayerValueTextName);
        PlayerOneNameInputProperty = serializedObject.FindProperty(MainMenuPlayerOneNameInputName);
        PlayerOneRGBSlidersProperty = serializedObject.FindProperty(MainMenuPlayerOneRGBSlidersName);
        PlayerOneColorImageProperty = serializedObject.FindProperty(MainMenuPlayerOneColorImageName);
        PlayerTwoNameInputProperty = serializedObject.FindProperty(MainMenuPlayerTwoNameInputName);
        PlayerTwoRGBSlidersProperty = serializedObject.FindProperty(MainMenuPlayerTwoRGBSlidersName);
        PlayerTwoColorImageProperty = serializedObject.FindProperty(MainMenuPlayerTwoColorImageName);
        LapCountSliderProperty = serializedObject.FindProperty(MainMenuLapCountSliderName);
        LapValueTextProperty = serializedObject.FindProperty(MainMenuLapValueTextName);
        StartGameButtonProperty = serializedObject.FindProperty(MainMenuStartGameButtonName);
        GameSettingsBackButtonProperty = serializedObject.FindProperty(MainMenuGameSettingsBackButtonName);

        //Options 
        VolumeSliderProperty = serializedObject.FindProperty(MainMenuVolumeSliderName);
        VolumeValueTextProperty = serializedObject.FindProperty(MainMenuVolumeValueTextName);
        GameQualitySliderProperty = serializedObject.FindProperty(MainMenuGameQualitySliderName);
        GameQualityTextProperty = serializedObject.FindProperty(MainMenuGameQualityTextName);
        OptionsBackButtonProperty = serializedObject.FindProperty(MainMenuOptionsBackButtonName);

        //Panels
        CurrentMenuProperty = serializedObject.FindProperty(MainMenuCurrentMenuName);
        MainMenuPanelProperty = serializedObject.FindProperty(MainMenuPanelName);
        GameSettingsPanelProperty = serializedObject.FindProperty(MainMenuGameSettingsPanelName);
        OptionsPanelProperty = serializedObject.FindProperty(MainMenuOptionsPanelName);
    }


    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        GameSettings();
        Options();
        Panels();

        serializedObject.ApplyModifiedProperties();
    }

    private void GameSettings()
    {
        EditorGUILayout.BeginVertical(GUI.skin.box);

        EditorGUI.indentLevel++;
        for(int i = 0; i < 2; i++)
        {
            EditorGUILayout.PropertyField(_Players.GetArrayElementAtIndex(i));
        }

        ShowGameSettings = EditorGUILayout.Foldout(ShowGameSettings ,"GameSettings");
        if (ShowGameSettings)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(PlayerCountSliderProperty);

            ShowPlayerOne = EditorGUILayout.Foldout(ShowPlayerOne, "PlayerOne");
            if (ShowPlayerOne)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(PlayerValueTextProperty);
                EditorGUILayout.PropertyField(PlayerOneNameInputProperty);
                EditorGUILayout.PropertyField(PlayerOneColorImageProperty);
                
                ShowPlayerOneRGB = EditorGUILayout.Foldout(ShowPlayerOneRGB, "PlayerOneRGB");
                if (ShowPlayerOneRGB)
                {
                    EditorGUI.indentLevel++;

                    for (int i = 0; i < RGBCount; i++)
                    {
                        EditorGUILayout.PropertyField(PlayerOneRGBSlidersProperty.GetArrayElementAtIndex(i));
                    }

                    EditorGUI.indentLevel--;
                }

                EditorGUI.indentLevel--;
            }

            ShowPlayerTwo = EditorGUILayout.Foldout(ShowPlayerTwo, "PlayerTwo");

            if (ShowPlayerTwo)
            {
                EditorGUI.indentLevel++;

                EditorGUILayout.PropertyField(PlayerOneColorImageProperty);
                EditorGUILayout.PropertyField(PlayerTwoNameInputProperty);
                EditorGUILayout.PropertyField(PlayerTwoColorImageProperty);

                ShowPlayerTwoRGB = EditorGUILayout.Foldout(ShowPlayerTwoRGB, "PlayerTwoRGB");
                if (ShowPlayerTwoRGB)
                {
                    EditorGUI.indentLevel++;
                    for (int i = 0; i < RGBCount; i++)
                    {
                        EditorGUILayout.PropertyField(PlayerTwoRGBSlidersProperty.GetArrayElementAtIndex(i));
                    }

                    EditorGUI.indentLevel--;
                }

                EditorGUI.indentLevel--;
            }

            EditorGUILayout.PropertyField(LapCountSliderProperty);
            EditorGUILayout.PropertyField(LapValueTextProperty);
            EditorGUILayout.PropertyField(StartGameButtonProperty);
            EditorGUILayout.PropertyField(GameSettingsBackButtonProperty);
            EditorGUI.indentLevel--;
        }

        EditorGUI.indentLevel--;
        EditorGUILayout.EndVertical();
    }

    private void Options()
    {
        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUI.indentLevel++;

        ShowOptions = EditorGUILayout.Foldout(ShowOptions, "Options");
        if (ShowOptions)
        {
            EditorGUI.indentLevel++;

            EditorGUILayout.PropertyField(VolumeSliderProperty);
            EditorGUILayout.PropertyField(VolumeValueTextProperty);
            EditorGUILayout.PropertyField(GameQualitySliderProperty);
            EditorGUILayout.PropertyField(GameQualityTextProperty);
            EditorGUILayout.PropertyField(OptionsBackButtonProperty);
            EditorGUI.indentLevel--;
        }
        EditorGUI.indentLevel--;
        EditorGUILayout.EndVertical();
    }

    private void Panels()
    {
        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUI.indentLevel++;

        ShowPanels = EditorGUILayout.Foldout(ShowPanels, "Panels");
        if (ShowPanels)
        {
            EditorGUI.indentLevel++;

            EditorGUILayout.PropertyField(CurrentMenuProperty);
            EditorGUILayout.PropertyField(MainMenuPanelProperty);
            EditorGUILayout.PropertyField(GameSettingsPanelProperty);
            EditorGUILayout.PropertyField(OptionsPanelProperty);
            EditorGUI.indentLevel--;
        }
        EditorGUI.indentLevel--;
        EditorGUILayout.EndVertical();
    }
}
