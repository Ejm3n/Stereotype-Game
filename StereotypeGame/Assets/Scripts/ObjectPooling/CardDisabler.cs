using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDisabler : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Card"))
        {
            collision.GetComponent<GameObject>().SetActive(false);
        }
    }
}
