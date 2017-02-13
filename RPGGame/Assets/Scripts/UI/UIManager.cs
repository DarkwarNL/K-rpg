using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
    protected KeyCode[] keys = new KeyCode[] { KeyCode.K};
    [SerializeField]
    private SkillsSelector _SkillSelector;

    void FixedUpdate()
    {
        for (int i = 0; i < keys.Length; i++)
        {
            if (Input.GetKeyDown(keys[i]))
            {
                SkillMenu();
            }
        }
    }

    private void SkillMenu()
    {
        if(_SkillSelector.gameObject.activeSelf)
        {
            _SkillSelector.gameObject.SetActive(false);
            Cursor.visible = false;
        }
        else
        {
            _SkillSelector.gameObject.SetActive(true);
            Cursor.visible = true;
        }       
    }
}
