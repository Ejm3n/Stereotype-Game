using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardContent
{
    public string Fact;
    public string Stereotype;
}


[CreateAssetMenu(fileName = "New card", menuName = "Card")]
public class CardSO : ScriptableObject
{
    public CardContent[] Content;
}
