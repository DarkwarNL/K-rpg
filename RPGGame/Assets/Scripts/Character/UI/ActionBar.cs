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
        if (_ActionBarObjects == null) GetActionBarObjects();
       
        for (int i = 0; i < _ActionBarObjects.Length; i++)
        {
            if (skills[i] == null) continue;
            Debug.Log(skills.Length + "--" + _ActionBarObjects.Length);
            skills[i].SetCooldownImage(_ActionBarObjects[i].SetData(skills[i].GetSprite()));
        }
    }
}