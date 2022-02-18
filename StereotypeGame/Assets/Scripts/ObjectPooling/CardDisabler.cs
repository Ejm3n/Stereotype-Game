using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDisabler : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<CardController>())
        {
            CardController obj = collision.GetComponent<CardController>();
            if (obj.IsStereotype && obj.IsClickable)
            {
                GameManager.Instance.LoseLife();
            }
            obj.DisableObj();
            collision.gameObject.SetActive(false);
        }
    }
}
