using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PopupDamage : MonoBehaviour {
    private GameObject TextObject;

    void Awake()
    {
        TextObject = Resources.Load<GameObject>("UI/PopupText");
    }

    public void CreateFloatingDamageText(string text, string animText)
    {
        GameObject textObj = Instantiate(TextObject);
        textObj.transform.SetParent(transform, false);

        RectTransform rect = textObj.GetComponent<RectTransform>();

        
        rect.rotation = new Quaternion();
        rect.localScale = new Vector3(1, 1, 1);
        textObj.GetComponent<Text>().text = text;

        Animator anim = textObj.GetComponent<Animator>();
        anim.SetTrigger(animText);
        Destroy(anim.gameObject, 4);
    }
}