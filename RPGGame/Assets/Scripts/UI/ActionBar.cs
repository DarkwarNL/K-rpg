using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionBar : MonoBehaviour {
    private ActionBarObject[] _ActionBarObjects = new ActionBarObject[4];
    private static ActionBar _ActionBar;

    public static ActionBar Instance
    {
        get
        {
            if (!_ActionBar) _ActionBar = FindObjectOfType<ActionBar>();
            return _ActionBar;
        }
    }
    void Awake()
    {
        GetActionBarObjects();
    }

    private void GetActionBarObjects()
    {
        _ActionBarObjects = GetComponentsInChildren<ActionBarObject>();
        for(int i = 0; i < _ActionBarObjects.Length; i++)
        {
            _ActionBarObjects[i].SetSkillNumber(i);
        }
    }

    public Image GetCooldownImage(int num)
    {
        return _ActionBarObjects[num].GetImage();
    }

    public void SkillsChanged(Skill[] skills)
    {
        if (_ActionBarObjects[0] == null) GetActionBarObjects();
       
        for (int i = 0; i < SkillDatabase.SkillAmount; i++)
        {
            if (skills[i] == null) continue;
            skills[i].SetCooldownImage(_ActionBarObjects[i].SetData(skills[i].Sprite));
        }
    }
}