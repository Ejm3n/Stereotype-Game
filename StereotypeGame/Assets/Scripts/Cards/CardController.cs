using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CardController : MonoBehaviour
{
    public string Text;
    [SerializeField] private TextMeshPro textMesh;
    [SerializeField] private bool IsClickable = true;
   [SerializeField] private CardContent cardContent;
   [SerializeField] private bool isStereotype = false;
    [SerializeField] private int timeToDisable;
    public CardContent CardContent {set => cardContent = value; }
    public bool IsStereotype {set => isStereotype = value; }
  

    private void Awake()
    {

        textMesh.text = Text;
    }
    private void Update()
    {
        transform.Translate(Vector2.down * GameManager.Instance.CardSpeed * Time.deltaTime);
        if (isStereotype)
        {
            textMesh.text = cardContent.Stereotype;
        }
        else
        {
            textMesh.text = cardContent.Fact;
        }
    }
    private void OnEnable()
    {
        StartCoroutine(DisableObject());
    }
    private IEnumerator DisableObject()
    {
        yield return new WaitForSeconds(timeToDisable);
        gameObject.SetActive(false);
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
            isStereotype = false;
        }
        else
        {
            GameManager.Instance.LoseLife();
        }
    }

   

}
