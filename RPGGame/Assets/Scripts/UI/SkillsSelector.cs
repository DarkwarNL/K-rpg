using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsSelector : MonoBehaviour {
    List<SkillButton> _Buttons = new List<SkillButton>();

    [SerializeField]
    private Transform _SkillChangePanel;
    [SerializeField]
    private Transform _SkillChangeButtonPanel;

    private GameObject _SkillSelectButton;
    private SkillDatabase _SkillDatabase;
    private static SkillsSelector _SkillsSelector;

    private List<SkillSelectButton> _SelectButtons = new List<SkillSelectButton>();

    public static SkillsSelector Instance
    {
        get
        {
            if (!_SkillsSelector) _SkillsSelector = FindObjectOfType<SkillsSelector>();
            return _SkillsSelector;
        }
    }

	void Awake ()
    {
        _SkillSelectButton = Resources.Load<GameObject>("UI/Skills/SkillSelectButton");
        _SkillDatabase = SkillDatabase.Instance;

        UpdateSkills();
    }

    internal void ChangingSkill(int num, SkillButton button)
    {
        _SkillChangePanel.gameObject.SetActive(true);
        Skill[] skills = _SkillDatabase.GetArcherSkills();

        Debug.Log(skills.Length);
        foreach(Skill skill in skills)
        {
            SkillSelectButton newButton = Instantiate(_SkillSelectButton, _SkillChangeButtonPanel, false).GetComponent<SkillSelectButton>();
            newButton.SetData(skill, num, this);
            _SelectButtons.Add(newButton);
        }
    }

    internal void SetNewSkill(Skill skill, int number)
    {
        _SkillDatabase.SetSelectedSkill(number, skill);
        UpdateSkills();
    }
    
    private void UpdateSkills()
    {
        Skill[] selectedSkills = _SkillDatabase.GetSelectedSkills();

        for (int i = 0; i < SkillDatabase.SkillAmount; i++)
        {
            SkillButton button = transform.GetChild(i).GetComponent<SkillButton>();
            button.SetData(selectedSkills[i]);
            _Buttons.Add(button);
        }

        _SkillChangePanel.gameObject.SetActive(false);
        foreach (SkillSelectButton item in _SelectButtons)
        {
            Destroy(item.gameObject);
        }
        _SelectButtons = new List<SkillSelectButton>();
    }
}
