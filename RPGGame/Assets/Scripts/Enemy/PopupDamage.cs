using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PopupDamage : MonoBehaviour {

    private GameObject TextObject;

    private AnimationClip[] Damage;
    private AnimationClip[] Heal;
    private AnimationClip[] Critical;

    void Awake()
    {
        TextObject = Resources.Load<GameObject>("Prefabs/PopupText");
        Damage = Resources.LoadAll<AnimationClip>("UI/Animations/Damage");
        Heal = Resources.LoadAll<AnimationClip>("UI/Animations/Heal");
        Critical = Resources.LoadAll<AnimationClip>("UI/Animations/Critical");
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
        textObj.transform.SetParent(transform);
        RectTransform rect = textObj.GetComponent<RectTransform>();
        rect.localPosition = new Vector3();
        rect.rotation = new Quaternion();
        rect.localScale = new Vector3(1,1,1);
        textObj.GetComponent<Text>().text = text;
        Destroy(textObj, 4);
        return textObj.GetComponent<Animation>();
    }
}
