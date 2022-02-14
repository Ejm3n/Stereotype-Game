using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgSprite : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer;
    [SerializeField] private float timeToDisable = 25f;
    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        StartCoroutine(DisableObjectAfterTime());
    }
    private IEnumerator DisableObjectAfterTime()
    {
        yield return new WaitForSeconds(timeToDisable);
        gameObject.SetActive(false);
    }
    void Update()
    {
        transform.Translate(Vector2.down* GameManager.Instance.CardSpeed* Time.deltaTime);

    }
}
