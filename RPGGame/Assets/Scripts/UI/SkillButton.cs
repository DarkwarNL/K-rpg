using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour {
    private Image _Icon;
    private Button _Button;
    private Text _Name;
    private Text _Number;
    public int KeyNumber;

    void Awake()
    {
        _Button = GetComponent<Button>();
        _Icon = transform.FindChild("Icon").GetComponent<Image>();        
        _Name = transform.FindChild("Name").GetComponent<Text>();


        _Number = transform.FindChild("KeyNumber").GetComponent<Text>();
        _Button.onClick.AddListener(() => GetComponentInParent<SkillsSelector>().ChangingSkill(KeyNumber-1, this));
    }

    internal void SetData(Skill skill)
    {
        if (skill == null) return;
        _Icon.sprite = skill.GetSprite();
        _Name.text = skill.SkillName;
    }
}
