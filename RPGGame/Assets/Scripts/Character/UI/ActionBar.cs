using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionBar : MonoBehaviour {
    internal List<Skill> Skills = new List<Skill>();

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
        foreach (Transform trans in transform)
        {
           // Skills.Add(trans.GetComponent<Skill>());
        }
    }
}