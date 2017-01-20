using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDatabase : MonoBehaviour
{
    private Skill[] _SelectedSkills = new Skill[4];
    private List<Skill> _ArcherSkills = new List<Skill>();

    private string _SpriteLocation = "UI/Skills/";
    private string _ArrowLocation = "Prefabs/Arrows/SkillArrows/";

    private ActionBar _ActionBar;
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
        _ArcherSkills.Add(new Skill(10, skillName, Resources.Load<Sprite>(_SpriteLocation + skillName), Resources.Load<Arrow>(_ArrowLocation + skillName)));
        skillName = "RainOfArrows";
        _ArcherSkills.Add(new Skill(10, skillName, Resources.Load<Sprite>(_SpriteLocation + skillName), Resources.Load<Arrow>(_ArrowLocation + skillName)));
        skillName = "Ricochet";
        _ArcherSkills.Add(new Skill(10, skillName, Resources.Load<Sprite>(_SpriteLocation + skillName), Resources.Load<Arrow>(_ArrowLocation + skillName)));
        skillName = "TeleportArrow";
        _ArcherSkills.Add(new Skill(10, skillName, Resources.Load<Sprite>(_SpriteLocation + skillName), Resources.Load<Arrow>(_ArrowLocation + skillName)));

        _ActionBar = ActionBar.Instance;
        _ActionBar.SkillsChanged(_SelectedSkills);
    }

    internal void SetSelectedSkill(int number, Skill skill)
    {
        for(int i = 0; i < _SelectedSkills.Length; i++)
        {
            if(_SelectedSkills[i] == skill)
            {
                _SelectedSkills[i] = new Skill();
                break;
            }
        }
        _SelectedSkills[number] = skill;
        _ActionBar.SkillsChanged(_SelectedSkills);
    }

    internal Skill[] GetSelectedSkills()
    {
        return _SelectedSkills;
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
