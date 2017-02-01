using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Player[] Players = new Player[2];

    [SerializeField]
    private GameObject _CurrentMenu;
    /// <summary>
    /// OnFirstStart /// OnNameChange
    /// </summary>
    [SerializeField]
    private InputField _SetPlayerOneName;
    [SerializeField]
    private InputField _SetPlayerTwoName;
    [SerializeField]
    private Button _FinishedNameChangeButton;

    [SerializeField]
    private GameObject _MainMenuPanel;
    /// <summary>
    /// singleplayer menu
    /// </summary>
    [SerializeField]
    private Button _SinglePlayerButton;
    /// <summary>
    /// Splitscreen menu
    /// </summary>
    [SerializeField]
    private Button _SplitscreenButton;
    /// <summary>
    /// Options menu
    /// </summary>
    [SerializeField]
    private Button _OptionsButton;

    void Awake()
    {
        _CurrentMenu.GetComponent<Animator>().SetTrigger("OnEnter");

        _SetPlayerOneName.onEndEdit.AddListener(delegate { SetName(_SetPlayerOneName.text, Players[0]); });
        _SetPlayerTwoName.onEndEdit.AddListener(delegate { SetName(_SetPlayerTwoName.text, Players[1]); });
        _FinishedNameChangeButton.onClick.AddListener(() =>  OpenMenu(_MainMenuPanel));
    }

    private void SetName(string name, Player player)
    {       
        player.Name = name;
    }

    private void OpenMenu(GameObject menu)
    {
        OnMenuPanelExit(_CurrentMenu);
        OnMenuPanelEnter(menu);
    }

    private void OnMenuPanelExit(GameObject menu)
    {
        menu.GetComponent<Animator>().SetTrigger("OnExit");
    }

    private void OnMenuPanelEnter(GameObject menu)
    {
        menu.GetComponent<Animator>().SetTrigger("OnEnter");
    }
}