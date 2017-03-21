using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDatabase : MonoBehaviour
{
    public Skill Empty;
    public Skill_Archer[] _ArcherSkills = new Skill_Archer[4];
    public Skill[] _SelectedSkills { get; private set; }
    private ActionBar _ActionBar;
    private Player _Player;

    public const int SkillAmount = 4;

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
        _ArcherSkills = Resources.LoadAll<Skill_Archer>("Skills/ArcherSkills");
        _Player = GetComponent<Stats>().Player;
        _ActionBar = ActionBar.Instance;

        string[] skillNames = _Player.GetSelectedSkills();


        _SelectedSkills = new Skill[4];
        for (int i = 0; i < SkillAmount; i++)
        {
            foreach(Skill skill in _ArcherSkills)
            {
                if (skill.SkillName == skillNames[i])
                {
                    _SelectedSkills[i] = skill;
                    continue;
                }
            }
            if (_SelectedSkills[i] == null)
                _SelectedSkills[i] = Empty;
        }
      _ActionBar.SkillsChanged(_SelectedSkills);
    }

    internal void SetSelectedSkill(int number, Skill skill)
    {
        for (int i = 0; i < SkillAmount; i++)
        {
            if(_SelectedSkills[i] == skill)
            {
                _SelectedSkills[i] = Empty;
                break;
            }
        }
        _SelectedSkills[number] = skill;
        _ActionBar.SkillsChanged(_SelectedSkills);
        SendPlayerSkills();
    }

    private void SendPlayerSkills()
    {
        string[] selectedSkills = new string[4];
        for(int i = 0; i < SkillAmount; i++)
        {
            selectedSkills[i] = "";

            if (_SelectedSkills[i] != null)
                selectedSkills[i] = _SelectedSkills[i].SkillName;
            
        }
        _Player.SetSkills(selectedSkills);
    }

    internal Skill[] GetSelectedSkills()
    {
        return _SelectedSkills;
    }

    internal Skill[] GetArcherSkills()
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
