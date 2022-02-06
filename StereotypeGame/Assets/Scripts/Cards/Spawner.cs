using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private int cardCount;
    [SerializeField] private bool autoExpand = true;
    [SerializeField] private CardController cardPrefab;
    [SerializeField] private Vector3 whereToSpawn;

    private ObjectPool<CardController> pool;
    private void Awake()
    {
        pool = new ObjectPool<CardController>(cardPrefab, cardCount, transform);
        pool.AutoExpand = autoExpand;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            CreateCard();
        }
    }

    private void CreateCard()
    {
        CardController card = pool.GetFreeElement();
        card.transform.position = whereToSpawn;
    }
}
