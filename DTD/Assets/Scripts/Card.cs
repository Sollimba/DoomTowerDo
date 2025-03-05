using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Card/New card", fileName = "New card", order = 51)]
public class Card : ScriptableObject
{
    public Sprite icone;
    public GameObject prefab;
    public int cost;
}
