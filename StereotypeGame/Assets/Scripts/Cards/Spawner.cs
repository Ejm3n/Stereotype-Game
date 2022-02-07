using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private int cardCount;
    [SerializeField] private bool autoExpand = true;
    [SerializeField] private CardController cardPrefab;
    [SerializeField] private CardSO easySentences;
    [SerializeField] private Vector2[] positions;
    private ObjectPool<CardController> pool;
    private void Awake()
    {
        pool = new ObjectPool<CardController>(cardPrefab, cardCount, transform);
        pool.AutoExpand = autoExpand;
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Q))
        {
            CreateCard(true);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            CreateCard(false);
        }
    }

    private void CreateCard(bool isStereotype)
    {
        CardController card = pool.GetFreeElement();     
        card.transform.position = transform.position;
        card.IsStereotype = isStereotype;
        card.CardContent = easySentences.Content[0];
    }
}
