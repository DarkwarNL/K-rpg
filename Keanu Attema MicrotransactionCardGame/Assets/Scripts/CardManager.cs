using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Linq;

public class CardManager : MonoBehaviour {

    [SerializeField]
    private List<Card> _Cards;

    [SerializeField]
    private GameObject _CardPrefab;
    [SerializeField]
    private Transform _NewCardPanel;
    [SerializeField]
    private Transform _CollectedCardPanel;

    private Card[] _AvailibleCardsEpic;
    private Card[] _AvailibleCardsNormal;
    private Card[] _AvailibleCardsRare;

    private static CardManager _CardManager;

    public static CardManager Instance
    {
        get
        {
            if (!_CardManager) _CardManager = FindObjectOfType<CardManager>();
            return _CardManager;
        }
    }

    void Awake ()
    {
        _AvailibleCardsNormal = Resources.LoadAll<Card>("Cards/Normal");
        _AvailibleCardsRare = Resources.LoadAll<Card>("Cards/Rare");
        _AvailibleCardsEpic = Resources.LoadAll<Card>("Cards/Epic");

        LoadCards();
        
        if (_Cards == null)
            _Cards = new List<Card>();
	}

    public void PurchasedPack(CardType type)
    {
        int rollEpic = 0;
        int rollRare = 0;
        int cardAmount = 5;
        List<Card> cardsToSend = new List<Card>();        

        switch (type)
        {
            case CardType.Normal:
                rollEpic = 5;
                rollRare = 15;
                break;

            case CardType.Rare:
                rollEpic = 20;
                rollRare = 30;
                cardAmount--;

                cardsToSend.Add(_AvailibleCardsRare[Random.Range(0, _AvailibleCardsRare.Length)]);
                SoundManager.Instance.PlayGotRare();
                break;

            case CardType.Epic:
                rollRare = 35;
                rollEpic = 20;
                cardAmount--;

                cardsToSend.Add(_AvailibleCardsEpic[Random.Range(0, _AvailibleCardsEpic.Length)]);
                SoundManager.Instance.PlayGotEpic();
                break;
        }     

        for(int i =0; i < cardAmount; i++)
        {
            int roll = Random.Range(0,100);

            if (roll <= rollEpic)
            {
                cardsToSend.Add(_AvailibleCardsEpic[Random.Range(0, _AvailibleCardsEpic.Length)]);
            }
            else if(roll <= rollRare)
            {
                cardsToSend.Add(_AvailibleCardsRare[Random.Range(0, _AvailibleCardsRare.Length)]);
            }
            else
            {
                cardsToSend.Add(_AvailibleCardsNormal[Random.Range(0, _AvailibleCardsNormal.Length)]);
            }
        }
        AddCard(cardsToSend);

        _CollectedCardPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, _Cards.Count / 5 * 505);
    }

    private void LoadCards()
    {
        _Cards = Player.Load();
        if (_Cards == null) return;
        
        foreach(Card card in _Cards)
        {
            GameObject newCard = Instantiate(_CardPrefab, _CollectedCardPanel);
            newCard.GetComponent<ShowCardButton>().CurrentCard = card;
            newCard.transform.GetComponentInChildren<Image>().sprite = card.CardImage;
        }

        _CollectedCardPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, _Cards.Count / 5 * 505);
    }

    private void AddCard(List<Card> cards)
    {
        for(int i=0; i < 5; i++)
        {
            _Cards.Add(cards[i]);       
                        
            _NewCardPanel.GetChild(i).GetChild(0).GetComponent<Image>().sprite = cards[i].CardImage;
            
            GameObject newCard = Instantiate(_CardPrefab, _CollectedCardPanel);
            newCard.GetComponent<ShowCardButton>().CurrentCard = cards[i];
            newCard.transform.GetChild(0).GetComponent<Image>().sprite = cards[i].CardImage;
        }        
        
        Player.Save(_Cards);
        _NewCardPanel.GetComponent<Animator>().SetTrigger("Enter");
    }

    public void SortCards(int i)
    {
        if(i == 0)
        {
            //sort epic first
            _Cards = _Cards.OrderByDescending(go => go.Type).ToList();
        }
        else if (i == 1)
        {
            //sort normal first
            _Cards = _Cards.OrderBy(go => go.Type).ToList();
        }
        else
        {
            _Cards = _Cards.OrderBy(go => go.Name).ToList();
        }
        Player.Save(_Cards);
        foreach(Transform trans in _CollectedCardPanel)
        {
            Destroy(trans.gameObject);
        }
        
        LoadCards();
    }
}
