using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDatabase : MonoBehaviour
{
    private List<Skill> _ArcherSkills = new List<Skill>();

    private string _SpriteLocation = "UI/Skills/";
    private string _ArrowLocation = "Prefabs/Arrows/SkillArrows/";

    private ActionBar _ActionBar;
    private Player _Player;
    private static SkillDatabase _Skilldatabase;

    public static SkillDatabase Instance
    {
        get
        {
            if(!_Skilldatabase) _Skilldatabase = FindObjectOfType<SkillDatabase>();
            return _Skilldatabase;
        }
    }

    void Awake()
    {
        string skillName = "MultiArrow";
        _ArcherSkills.Add(new Skill(10, skillName, _SpriteLocation + skillName, _ArrowLocation + skillName));
        skillName = "RainOfArrows";
        _ArcherSkills.Add(new Skill(10, skillName,_SpriteLocation + skillName, _ArrowLocation + skillName));
        skillName = "Ricochet";
        _ArcherSkills.Add(new Skill(10, skillName, _SpriteLocation + skillName, _ArrowLocation + skillName));
        skillName = "TeleportArrow";
        _ArcherSkills.Add(new Skill(10, skillName, _SpriteLocation + skillName, _ArrowLocation + skillName));

        _Player = GetComponent<Stats>()._Player;
        _ActionBar = ActionBar.Instance;

        _ActionBar.SkillsChanged(_Player.GetSelectedSkills());
    }

    internal void SetSelectedSkill(int number, Skill skill)
    {
        Skill[] selectedSkills = _Player.GetSelectedSkills();
        for(int i = 0; i < selectedSkills.Length; i++)
        {
            if(selectedSkills[i] == skill)
            {
                selectedSkills[i] = new Skill();
                break;
            }
        }
        selectedSkills[number] = skill;
        _ActionBar.SkillsChanged(selectedSkills);
        _Player.SetSkills(selectedSkills);
    }

    internal Skill[] GetSelectedSkills()
    {
        if(_Player == null) _Player = GetComponent<Stats>()._Player;
        return _Player.GetSelectedSkills();
    }

    internal List<Skill> GetArcherSkills()
    {
        return _ArcherSkills;
    }

    internal Skill GetSkill(int number)
    {
        return _ArcherSkills[number];
    }

    internal Skill GetSkillByName(string name)
    {
        foreach(Skill skill in _ArcherSkills)
        {
            if (skill.SkillName == name) return skill;
        }        
        return _ArcherSkills[1];
    }
}
