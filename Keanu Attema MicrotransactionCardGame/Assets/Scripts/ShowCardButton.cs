using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShowCardButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    internal Card CurrentCard;
    private Button _Button;

    void Awake()
    {
        _Button = GetComponent<Button>();
        _Button.onClick.AddListener(() => Clicked());
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _Button.targetGraphic.GetComponent<RectTransform>().localScale = new Vector3(1.2f , 1.2f ,1.2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _Button.targetGraphic.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
    }
    
    void Clicked()
    {
        Menu.Instance.ShowCard(CurrentCard);
    }
}
