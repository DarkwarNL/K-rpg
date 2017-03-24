using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class Menu : MonoBehaviour {
    [SerializeField]
    private Button _ShopButton;
    [SerializeField]
    private Button[] _MainMenuButton;
    [SerializeField]
    private Button _CardsButton;

    /// <summary>
    /// Shop
    /// </summary>
    [SerializeField]
    private Button _BronzeButton;
    [SerializeField]
    private Button _SilverButton;
    [SerializeField]
    private Button _GoldButton;
    [SerializeField]
    private Button _YesButton;
    [SerializeField]
    private Button _NoButton;
    [SerializeField]
    private Button _ExitButton;

    [SerializeField]
    private Button _OptionsButton;

    private CardType _PurchasingType;
    [SerializeField]
    private Slider _SoundEffectSlider;
    [SerializeField]
    private Slider _SoundVolumeSider;

    private static string _BronzeString = "Are you sure that you will purchase the <b>Bronze pack?</b>";
    private static string _SilverString = "Are you sure that you will purchase the <b>Silver pack</b>?";
    private static string _GoldString = "Are you sure that you will purchase the <b>Gold pack?</b>";

    [SerializeField]
    private Animator _MainMenuAnim;
    [SerializeField]
    private Animator _ShopAnim;
    [SerializeField]
    private Animator _PurchaseAnim;
    [SerializeField]
    private Animator _CardsAnim;
    [SerializeField]
    private Animator _CurrentAnim;
    [SerializeField]
    private Animator _OptionsAnim;

    [SerializeField]
    private Image _CardToShow;
    [SerializeField]
    private Button _CardToShowButton;

    private static Menu _Menu;
    public static Menu Instance
    {
        get
        {
            if (!_Menu) _Menu = FindObjectOfType<Menu>();
            return _Menu;

        }
    }

    void Awake()
    {   
        _ShopButton.onClick.AddListener(() => MenuSwitch(_ShopAnim));
        _CardsButton.onClick.AddListener(() => MenuSwitch(_CardsAnim));

        _BronzeButton.onClick.AddListener(() => OpenPurchasePanel(_BronzeString, CardType.Normal));
        _SilverButton.onClick.AddListener(() => OpenPurchasePanel(_SilverString, CardType.Rare));
        _GoldButton.onClick.AddListener(() => OpenPurchasePanel(_GoldString, CardType.Epic));

        _YesButton.onClick.AddListener(() => Purchase());
        _NoButton.onClick.AddListener(() => MenuSwitch(_ShopAnim));

        _CardToShowButton.onClick.AddListener(() => HideCard());

        _ExitButton.onClick.AddListener(() => Application.Quit());

        _OptionsButton.onClick.AddListener(() => MenuSwitch(_OptionsAnim));

        _SoundEffectSlider.onValueChanged.AddListener(delegate { SetVolume((int)_SoundEffectSlider.value, "SoundFX"); });
        _SoundVolumeSider.onValueChanged.AddListener(delegate { SetVolume((int)_SoundVolumeSider.value, "MusicVolume"); });

        foreach (Button button in _MainMenuButton)
        {
            button.onClick.AddListener(() => MenuSwitch(_MainMenuAnim));
        }
    }

    private void Purchase()
    {
        MenuSwitch(_ShopAnim);

        CardManager.Instance.PurchasedPack(_PurchasingType);
    }

    private void OpenPurchasePanel(string text , CardType type)
    {
        _PurchaseAnim.gameObject.SetActive(true);
        _PurchaseAnim.GetComponentInChildren<Text>().text = text;
        _PurchasingType = type;

        MenuSwitch(_PurchaseAnim);
    }

    private void MenuSwitch(Animator anim)
    {
        if (_CurrentAnim.GetCurrentAnimatorStateInfo(0).IsName("OnEnter") || _CurrentAnim.GetCurrentAnimatorStateInfo(0).IsName("OnLeave"))
        {
            return;
        }

        SoundManager.Instance.PlayButtonClicked();
        _CurrentAnim.SetTrigger("Leave");

        anim.SetTrigger("Enter");
        _CurrentAnim = anim;
        
    }

    public void ShowCard(Card cardToShow)
    {
        _CardToShow.sprite = cardToShow.CardImage;
        _CardToShow.gameObject.SetActive(true);
    }

    private void HideCard()
    {
        _CardToShow.sprite = null;
        _CardToShow.gameObject.SetActive(false);
    }

    private void SetVolume(int value , string volumeName)
    {
        AudioMixer mixer = Resources.Load<AudioMixer>("Sounds/SoundVolume");
        mixer.SetFloat(volumeName, value);
        Debug.Log(value );
        float val;
        mixer.GetFloat(volumeName, out val);
        Debug.Log(val );
    }
}