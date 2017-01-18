using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSelectButton : MonoBehaviour {
    private Image _Icon;
    private Button _Button;
    private Text _Name;
    private Skill _Skill;

    void Awake()
    {
        _Button = GetComponent<Button>();
        _Icon = transform.FindChild("Icon").GetComponent<Image>();        
        _Name = transform.FindChild("Name").GetComponent<Text>();

        _Button.onClick.AddListener(() => GetComponentInParent<SkillsSelector>().SelectedSkill(_Skill));
    }

    public void SetData(Skill skill)
    {        
        _Skill = skill;
        _Icon.sprite = skill.Sprite;
        _Name.text = skill.SkillName;
    }
}
