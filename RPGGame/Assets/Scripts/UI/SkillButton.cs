using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour {
    [SerializeField]
    private Image _Icon;
    [SerializeField]
    private Button _Button;
    [SerializeField]
    private Text _Name;
    public int KeyNumber;

    void Awake()
    {
        _Button.onClick.AddListener(() => GetComponentInParent<SkillsSelector>().ChangingSkill(KeyNumber-1, this));
    }

    internal void SetData(Skill skill)
    {
        if (skill == null) return;
        _Icon.sprite = skill.Sprite;
        _Name.text = skill.SkillName;
    }
}
