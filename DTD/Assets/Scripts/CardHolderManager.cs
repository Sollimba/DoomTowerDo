using UnityEditor.AssetImporters;
using UnityEngine;
using TMPro;

public class CardHolderManager : MonoBehaviour
{
    [Header("Card Holder")]
    [SerializeField] private Transform _cardHolderPosiyion;
    [SerializeField] private GameObject _card;
    [SerializeField] private Card[] _cardSO;

    [Header("Cards Parametrs ")]
    [SerializeField] private GameObject[] _plantedCards;
    private int _cost;
    private Sprite _icon;

    void Start()
    {
        _plantedCards = new GameObject[_cardSO.Length];

        for (int i = 0; i < _cardSO.Length; i++)
            CreateCard(i);
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
}
