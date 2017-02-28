using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Player[] _Players = new Player[2];
    private int _LapCount = 2;

    [SerializeField]
    private GameObject _CurrentMenu;

    /// <summary>
    /// Game Settings
    /// </summary>
    /// 
    [SerializeField]
    private Button _GameSettingsButton;
    [SerializeField]
    private Slider _PlayerCountSlider;
    [SerializeField]
    private Text _PlayerValueText;

    [SerializeField]
    private InputField _PlayerOneNameInput;
    [SerializeField]
    private Slider[] _PlayerOneRGBSliders = new Slider[3];
    [SerializeField]
    private Image _PlayerOneColorImage;

    [SerializeField]
    private InputField _PlayerTwoNameInput;
    [SerializeField]
    private Slider[] _PlayerTwoRGBSliders = new Slider[3];
    [SerializeField]
    private Image _PlayerTwoColorImage;

    [SerializeField]
    private Slider _LapCountSlider;
    [SerializeField]
    private Text _LapValueText;
    [SerializeField]
    private Button _StartGameButton;
    [SerializeField]
    private Button _GameSettingsBackButton;

    /// <summary>
    /// Options
    /// </summary>
    /// 
    [SerializeField]
    private Button _OptionsButton;
    [SerializeField]
    private Slider _VolumeSlider;
    [SerializeField]
    private Text _VolumeValueText;
    [SerializeField]
    private Slider _GameQualitySlider;
    [SerializeField]
    private Text _GameQualityText;
    [SerializeField]
    private Button _OptionsBackButton;

    /// <summary>
    /// Panels
    /// </summary>
    [SerializeField]
    private GameObject _MainMenuPanel;
    [SerializeField]
    private GameObject _GameSettingsPanel;
    [SerializeField]
    private GameObject _OptionsPanel;
    [SerializeField]
    private GameObject _InfoPanel;
    [SerializeField]
    private GameObject _RaceResultPanel;
    [SerializeField]
    private GameObject _PauseGamePanel;

    /// <summary>
    /// Info
    /// </summary>

    [SerializeField]
    private Button _InfoButton;
    [SerializeField]
    private Button _InfoBackButton;

    /// <summary>
    /// Race Results
    /// </summary>
    [SerializeField]
    private Transform _VerticalRaceResultPanel;
    [SerializeField]
    private Button _ResultsBackButton;

    [SerializeField]
    private Button _QuitGameButton;

    /// <summary>
    /// Pause Game
    /// </summary>
    [SerializeField]
    private Button _ResumeGameButton;
    [SerializeField]
    private Button _PauseBackToMainButton;
    [SerializeField]
    private Button _PauseQuitGameButton;


    private static MainMenu _MainMenu;
    public static MainMenu Instance
    {
        get
        {
            if (!_MainMenu) _MainMenu = FindObjectOfType<MainMenu>();
            return _MainMenu;
        }
    }

    public void PauseGame()
    {
        Invoke("SetTimeScale", .8f);
        OnMenuPanelEnter(_PauseGamePanel);
    }

    private void SetTimeScale()
    {
        Time.timeScale = 0f;
    }

    public void OpenRaceMenu(List<RaceData> raceData)
    {
        foreach(VehicleController vehicle in FindObjectsOfType<VehicleController>())
        {
            Destroy(vehicle.gameObject);
        }

        OpenMenu(_RaceResultPanel);
        foreach(RaceData data in raceData)
        {
            ResultPanel resultWindow = Instantiate(Resources.Load<GameObject>("Prefabs/ResultPanel"), _VerticalRaceResultPanel, false).GetComponent<ResultPanel>();
            resultWindow.SetData(data);
        }
    }

    #region PRIVATE_FUNCTIONS
    private void Awake()
    {
        _CurrentMenu.GetComponent<Animator>().SetTrigger("OnEnter");

        _InfoButton.onClick.AddListener(() => OpenMenu(_InfoPanel));
        _GameSettingsButton.onClick.AddListener(() => OpenMenu(_GameSettingsPanel));
        _OptionsButton.onClick.AddListener(() => OpenMenu(_OptionsPanel));     
          
        _PlayerOneNameInput.onEndEdit.AddListener(delegate { SetName(_PlayerOneNameInput.text, _Players[0]); });

        foreach (Slider slider in _PlayerOneRGBSliders)
        {
            slider.onValueChanged.AddListener(delegate { SetVehicleColor(0, slider.value, (int)slider.minValue); });            
        }
        foreach (Slider slider in _PlayerTwoRGBSliders)
        {
            slider.onValueChanged.AddListener(delegate { SetVehicleColor(1, slider.value, (int)slider.minValue); });
        }

        _PlayerTwoNameInput.onEndEdit.AddListener(delegate { SetName(_PlayerTwoNameInput.text, _Players[1]); });
        _LapCountSlider.onValueChanged.AddListener(delegate { SetLapCount((int)_LapCountSlider.value); });        
        _StartGameButton.onClick.AddListener(() => StartGame());

        //Options menu
        _VolumeSlider.onValueChanged.AddListener(delegate { SetVolume((int)_VolumeSlider.value); });
        _GameQualitySlider.onValueChanged.AddListener(delegate { SetGameQuality((int)_GameQualitySlider.value); });
        _OptionsBackButton.onClick.AddListener(() => OpenMenu(_MainMenuPanel));

        _GameSettingsBackButton.onClick.AddListener(() => OpenMenu(_MainMenuPanel));
        _InfoBackButton.onClick.AddListener(() => OpenMenu(_MainMenuPanel));
        _ResultsBackButton.onClick.AddListener(() => PauseToMain());

        _QuitGameButton.onClick.AddListener(() => QuitGame());

        _ResumeGameButton.onClick.AddListener(() => ResumeGame());
        _PauseBackToMainButton.onClick.AddListener(() => PauseToMain());
        _PauseQuitGameButton.onClick.AddListener(() => QuitGame());
    }

    private void PauseToMain()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene("RoadScene");
    }

    private void OpenMenu(GameObject menu)
    {
        OnMenuPanelExit(_CurrentMenu);
        OnMenuPanelEnter(menu);
    }

    private void ResumeGame()
    {
        OnMenuPanelExit(_CurrentMenu);
        Time.timeScale = 1;
    }

    private void StartGame()
    {
        List<Player> playersToSend = new List<Player>();

        for (int i = 0; i < _Players.Length; i++)
        {
            if (_Players[i] == null) continue;            
            if (_Players[i].Name == null || _Players[i].Name == "") continue;

            playersToSend.Add(_Players[i]);
        }
        if (playersToSend.Count == 0) return;

        RaceManager.Instance.CreatePlayers(playersToSend);
        RaceManager.Instance.SetLapCount(_LapCount);
        FindObjectOfType<WaypointCamera>().gameObject.SetActive(false);
        OnMenuPanelExit(_CurrentMenu);
    }

    private void SetLapCount(int count)
    {
        _LapValueText.text = count.ToString();
        _LapCount = count;
    }

    private void SetPlayerCount(int count)
    {
        _PlayerValueText.text = count.ToString();
        if (count == 1)
        {
            _PlayerTwoNameInput.gameObject.SetActive(false);
            if (_Players[1] != null)
            _Players[1].Name = null;
        }
        else
        {
            _PlayerTwoNameInput.gameObject.SetActive(true);
        }
    }

    private void SetVolume(int value)
    {
        AudioMixer mixer = Resources.Load<AudioMixer>("Sound/Music");
        mixer.SetFloat("MasterVolume", value);
        
        _VolumeValueText.text = value.ToString() + "dB";
    }

    private void SetGameQuality(int value)
    {
        _GameQualityText.text = value.ToString();
        QualitySettings.SetQualityLevel(value, true);
    }

    private void SetName(string name, Player player)
    {
        if (player == null)
            player = new Player();
        player.Name = name;
    }

    private void SetVehicleColor(int player, float value, int RGB)
    {
        if (_Players[player] == null)
            _Players[player] = new Player();
        switch (RGB)
        {
            case 0:
                _Players[player].VehicleColor.r = (byte)value;
                break;
            case 1:
                _Players[player].VehicleColor.g = (byte)value;
                break;
            case 2:
                _Players[player].VehicleColor.b = (byte)value;
                break;
        }
        if(player == 0)
        {
            _PlayerOneColorImage.color = _Players[player].VehicleColor;
        }
        else
        {
            _PlayerTwoColorImage.color = _Players[player].VehicleColor;
        }
    }

    private void OnMenuPanelExit(GameObject menu)
    {
        menu.GetComponent<Animator>().SetTrigger("OnExit");
    }

    private void OnMenuPanelEnter(GameObject menu)
    {
        menu.GetComponent<Animator>().SetTrigger("OnEnter");
        _CurrentMenu = menu;
    }

    private void QuitGame()
    {
        Application.Quit();
    }
    #endregion
}