using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CardController : MonoBehaviour
{
    public string Text;
    [SerializeField] private TextMeshPro textMesh;
    [SerializeField] private bool IsClickable = true;
    private CardContent cardContent;
    private bool isStereotype;

    public CardContent CardContent {set => cardContent = value; }
    public bool IsStereotype {set => isStereotype = value; }
  

    private void Awake()
    {
        textMesh.text = Text;
    }
    private void Update()
    {
        transform.Translate(Vector2.down * GameManager.Instance.CardSpeed * Time.deltaTime);
    }
    private void OnEnable()
    {
        if(isStereotype)
        {
            textMesh.text = cardContent.Stereotype;
        }
        else
        {
            textMesh.text = cardContent.Fact;
        }
    }

    private void OnMouseDown()
    {
        if(IsClickable)
            Pressed();
    }
    private void Pressed()
    {
        if(isStereotype)
        {
            ChangeToFact();
        }
        else
        {
            GameManager.Instance.LoseLife();
        }
    }
    private void ChangeToFact()
    {
        textMesh.text = cardContent.Fact;
    }
   

}
