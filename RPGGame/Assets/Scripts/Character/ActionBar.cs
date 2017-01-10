using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionBar : MonoBehaviour {
    private List<Image> _CooldownImages = new List<Image>();

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
            _CooldownImages.Add(trans.GetChild(1).GetComponent<Image>());
        }
    }

    public IEnumerator SetCooldown(int skillNumber ,float num)
    {
        float endTime = Time.time + num;
        while(Time.time < endTime)
        {
            _CooldownImages[skillNumber].fillAmount = (endTime - Time.time) / num;

            yield return null;
        }
    }
}
