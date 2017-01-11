using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillsSelector : MonoBehaviour {
    List<Skill> _Skills = new List<Skill>();
    List<SkillButton> _Buttons = new List<SkillButton>();

	void Awake ()
    {
		foreach(Transform trans in transform)
        {
           _Buttons.Add(trans.GetComponent<SkillButton>());
        }
	}

    internal void ChangingSkill(int num, SkillButton button)
    {
        //open skillselecter
    }
}
