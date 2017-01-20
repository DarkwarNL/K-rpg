using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionBar : MonoBehaviour {
    private List<ActionBarObject> _ActionBarObjects = new List<ActionBarObject>();
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
        foreach (Transform obj in transform)
        {
            ActionBarObject actionObj = obj.GetComponent<ActionBarObject>();
            if (actionObj) _ActionBarObjects.Add(actionObj);
        }
    }

    public void SkillsChanged(Skill[] skills)
    {
        if (_ActionBarObjects.Count <= 0) GetActionBarObjects();
        for (int i = 0; i < transform.childCount; i++)
        {            
            skills[i].SetCooldownImage(_ActionBarObjects[i].SetData(skills[i].Sprite));
        }
    }
}