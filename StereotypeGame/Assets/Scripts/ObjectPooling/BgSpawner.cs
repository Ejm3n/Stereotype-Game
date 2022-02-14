
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgSpawner : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private BgSprite bgItemPrefab;
    [Header ("картинок в пуле")]
    [SerializeField] private int imagesCount;
    [SerializeField] private float timeToCreateImage;
    [SerializeField] private float minXPoint;
    [SerializeField] private float maxXPoint;
    [SerializeField] private float YPoint;
    private int currentSprite = 0;
    private ObjectPool<BgSprite> spritesPool;
    private void Awake()
    {
        spritesPool = new ObjectPool<BgSprite>(bgItemPrefab, imagesCount, transform);
        spritesPool.AutoExpand = true;
        StartCoroutine(TimerToCreation());
    }

    private IEnumerator TimerToCreation()
    {
        CreateItem();
        while (true)
        {
            yield return new WaitForSeconds(timeToCreateImage);
            CreateItem();
        }
        
    }
    private void CreateItem()
    {
        BgSprite item = spritesPool.GetFreeElement();
        item.transform.position = GenerateCreationPoint();
        item.SpriteRenderer.sprite = sprites[ currentSprite];
        NextSprite();
       // item.gameObject.SetActive(false);
    }
    private void NextSprite()
    {
        if (currentSprite < sprites.Length - 1)
            currentSprite++;
        else
            currentSprite = 0;
    }
    private Vector2 GenerateCreationPoint()
    {
        float spawnXPoint = Random.Range(minXPoint, maxXPoint);
        return new Vector2(spawnXPoint, YPoint);
    }
}
