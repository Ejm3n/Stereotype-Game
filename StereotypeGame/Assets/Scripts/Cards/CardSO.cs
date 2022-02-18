using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardContent
{
    public CardContent(CardContent cardContent)
    {
        Fact = cardContent.Fact;
        Stereotype = cardContent.Stereotype;
    }
    public string Fact;
    public string Stereotype;
}


[CreateAssetMenu(fileName = "New card", menuName = "Card")]
public class CardSO : ScriptableObject
{
    public CardContent[] Content;
}
