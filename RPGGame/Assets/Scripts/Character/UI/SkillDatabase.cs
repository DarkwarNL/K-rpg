using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDatabase : MonoBehaviour
{
    private List<Skill> _ArcherSkills = new List<Skill>();
    private string _SpriteLocation = "UI/Skills/";
    private string _ArrowLocation = "Prefabs/Arrows/SkillArrows/";

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
    }

    internal List<Skill> ArcherSkills()
    {
        return _ArcherSkills;
    }

    internal Skill GetSkillByName(string name)
    {
        foreach(Skill skill in _ArcherSkills)
        {
            Debug.Log(name);
            if (skill.SkillName == name) return skill;
        }
        
        return _ArcherSkills[1];
    }
}
