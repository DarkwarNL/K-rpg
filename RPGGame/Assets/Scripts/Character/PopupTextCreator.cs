using UnityEngine;
using UnityEngine.UI;

public class PopupTextCreator : MonoBehaviour {
    public GameObject TextObject;

    public AnimationClip[] Damage;
    public AnimationClip[] Heal;
    public AnimationClip[] Critical;

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

    public void SpawnDamageText(string text)
    {
        Animation anim = CreateObject(text);
        anim.clip = Damage[Random.Range(0, Damage.Length)];
        anim.Play();
    }

    public void SpawnHealthText(string text)
    {
        Animation anim = CreateObject(text);
        anim.clip = Heal[Random.Range(0, Heal.Length)];
        anim.Play();
    }

    Animation CreateObject(string text)
    {
        GameObject textObj = Instantiate(TextObject);
        textObj.GetComponent<Text>().text = text;
        Destroy(textObj, 4);
        return textObj.GetComponent<Animation>();
    }
}
