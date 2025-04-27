using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CardHolderManager : MonoBehaviour
{
    [Header("Card Holder")]
    [SerializeField] private Transform _cardHolderPosiyion;
    [SerializeField] private GameObject _card;
    [SerializeField] private Card[] _cardSO;
    private int _cardsAmmount;

    [Header("Cards Parametrs ")]
    [SerializeField] private GameObject[] _plantedCards;
    private int _cost;
    private Sprite _icon;

    [SerializeField] private ResourceCounter _resourceCounter;

    void Start()
    {
        _cardsAmmount = _cardSO.Length;
        _plantedCards = new GameObject[_cardsAmmount];

        for (int i = 0; i < _cardSO.Length; i++)
            CreateCard(i);

        GameEvents.Instance.onResourcesCountChange += OnResourcesCountChanged;

        OnResourcesCountChanged();
    }

    private void CreateCard (int i)
    {
        var card = Instantiate(_card, _cardHolderPosiyion);
        CardManager cardManager = card.GetComponent<CardManager>(); 

        cardManager.CardSO = _cardSO[i];

        _plantedCards[i] = card;

        _icon = _cardSO[i].icone;
        _cost = _cardSO[i].cost;

        card.GetComponentInChildren<SpriteRenderer>().sprite = _icon;
        card.GetComponentInChildren<TMP_Text>().text = _cost.ToString();
    }

    private void OnResourcesCountChanged()
    {
        for (int i = 0; i < _cardsAmmount; i++)
        {
            if (_cardSO[i].cost > _resourceCounter.Resources)
            {
                _plantedCards[i].transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(132, 132, 132, 255);
                _plantedCards[i].GetComponent<Image>().color = Color.gray;
                _plantedCards[i].GetComponent<CardManager>().IsAbleToPlant = false;
            }
            else
            {
                _plantedCards[i].transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
                _plantedCards[i].GetComponent<Image>().color = Color.white;
                _plantedCards[i].GetComponent<CardManager>().IsAbleToPlant = true;
            }
        }
    }
}
