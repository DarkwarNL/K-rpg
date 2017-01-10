using UnityEngine;
using UnityEngine.UI;

public class PopupTextCreator : MonoBehaviour {
    private GameObject TextObject;

    private static PopupTextCreator _PopupTextCreator;

    public static PopupTextCreator Instance
    {
        get
        {
            if (!_PopupTextCreator)
                _PopupTextCreator = FindObjectOfType<PopupTextCreator>();
            return _PopupTextCreator;
        }
    }

    void Awake()
    {
        TextObject = Resources.Load<GameObject>("UI/PopupText");
    }

    public void SpawnDamageText(string text)
    {
        Animator anim = CreateObject(text);
        anim.SetTrigger("Damage");
    }

    public void SpawnHealthText(string text)
    {
        Animator anim = CreateObject(text);
        anim.SetTrigger("Heal");
    }

    public void SpawnCriticalText(string text)
    {
        Animator anim = CreateObject(text);
        anim.SetTrigger("Critical");
    }

    Animator CreateObject(string text)
    {
        GameObject textObj = Instantiate(TextObject);
        textObj.transform.SetParent(transform);
        RectTransform rect = textObj.GetComponent<RectTransform>();
        rect.localPosition = new Vector3();
        rect.rotation = new Quaternion();
        rect.localScale = new Vector3(1, 1, 1);
        textObj.GetComponent<Text>().text = text;
        Destroy(textObj, 4);
        return textObj.GetComponent<Animator>();
    }
}
