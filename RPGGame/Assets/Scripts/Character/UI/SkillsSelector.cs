using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillsSelector : MonoBehaviour {
    GameObject[] _Skills = new GameObject[0];
    List<SkillButton> _Buttons = new List<SkillButton>();

    private GameObject _SkillSelect;
    private GameObject _SkillSelectButton;
    private SkillDatabase _SkillDatabase;

	void Awake ()
    {
        _Skills = Resources.LoadAll<GameObject>("Prefabs/Arrows/SkillArrows");
        _SkillSelect = Resources.Load<GameObject>("UI/SkillsPanel");
        _SkillSelectButton = Resources.Load<GameObject>("UI/SkillSelectButton");
        _SkillDatabase = SkillDatabase.Instance;

        foreach (Transform trans in transform)
        {
           _Buttons.Add(trans.GetComponent<SkillButton>());
        }
	}

    internal void ChangingSkill(int num, SkillButton button)
    {
        GameObject skillSelect = Instantiate(_SkillSelect, transform.parent.parent, false);
        Transform buttonParent = skillSelect.transform.FindChild("SkillSelectPanel");

        foreach (GameObject skill in _Skills)
        {
            SkillSelectButton newButton = Instantiate(_SkillSelectButton, buttonParent, false).GetComponent<SkillSelectButton>();

            if (skill.GetComponent<Arrow_Skill>())
            {
                newButton.SetData(_SkillDatabase.GetSkillByName(skill.GetComponent<Arrow_Skill>().GetName()));
            }            
        }
    }

    internal void SelectedSkill(Skill skill)
    {

    }
}
