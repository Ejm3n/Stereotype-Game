using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgSprite : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer;
    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        transform.Translate(Vector2.down* GameManager.Instance.CardSpeed* Time.deltaTime);

    }
}
