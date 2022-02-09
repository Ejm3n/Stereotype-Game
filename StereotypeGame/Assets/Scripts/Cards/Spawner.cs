using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class ContentStorage
{
    public CardContent Content;
    public bool IsStereotype;

    public ContentStorage(CardContent content, bool stereotype)
    {
        Content = content;
        IsStereotype = stereotype;
    }
}

public class Spawner : MonoBehaviour
{
    [SerializeField] private int cardCount;
    [SerializeField] private bool autoExpand = true;
    [SerializeField] private CardController cardPrefab;
    [SerializeField] private CardSO easySentences;
    [SerializeField] private CardSO hardSentences;
    [SerializeField] private float timeToCreateCard;
    [SerializeField] private float timeToCreateCardFaster;
    [SerializeField] private Vector2[] positions;
    private ObjectPool<CardController> pool;
    private List<ContentStorage> easyCardContent ;
    private List<ContentStorage> hardCardContent ;

    private int currentCardNum;
    private int currentSpawnPoint;
    private void Start()
    {
        pool = new ObjectPool<CardController>(cardPrefab, cardCount, transform);
        pool.AutoExpand = autoExpand;
        easyCardContent = RandomizeCardContent(easyCardContent, easySentences);
        hardCardContent = RandomizeCardContent(hardCardContent, hardSentences);
        
        StartCoroutine(TimerToCreation());
    }
    private List<ContentStorage> RandomizeCardContent(List<ContentStorage> list, CardSO contentIn)
    {
       
        while(true)
        {
            list = new List<ContentStorage>();
            for (int i = 0; i < contentIn.Content.Length; i++)//заполняем
            {
                list.Add(new ContentStorage(contentIn.Content[i], Random.Range(0, 2) == 0));
            }
            int isa = 0;
            int isnt = 0;
            foreach (ContentStorage cs in list)
            {
                if (cs.IsStereotype)
                    isa++;
                else
                    isnt++;
            }
            if (Mathf.Abs(isa-isnt)<=1)
            {
                break;
            }           
        }
       
        for (int i = 0; i < list.Count; i++)//шафлим
        {
            ContentStorage temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
        return list;
    }

    private IEnumerator TimerToCreation()
    {
        ChangeSpawnPoint();
        if (currentCardNum >= easyCardContent.Count)
            currentCardNum = 0;
        if (GameManager.Instance.EasyMode)
            CreateCard(easyCardContent[currentCardNum].IsStereotype, true);
        else
            CreateCard(hardCardContent[currentCardNum].IsStereotype, false);
        while ( true)
        {
            
            yield return new WaitForSeconds(timeToCreateCard);
            ChangeSpawnPoint();
            if (currentCardNum >= easyCardContent.Count)
                currentCardNum = 0;
            if (GameManager.Instance.EasyMode)
                CreateCard(easyCardContent[currentCardNum].IsStereotype, true);
            else
                CreateCard(hardCardContent[currentCardNum].IsStereotype, false);
        }
    }
    private void ChangeSpawnPoint()
    {
        if (currentSpawnPoint >= positions.Length-1)
        {
            currentSpawnPoint = 0;
        }
        else
        {
            currentSpawnPoint++;
        }
    }
    private void CreateCard(bool isStereotype, bool isEasy)
    {
        CardController card = pool.GetFreeElement();
        card.transform.position = positions[currentSpawnPoint];
        if(isEasy)
            card.CardContent = easyCardContent[currentCardNum].Content;
        else
            card.CardContent = hardCardContent[currentCardNum].Content;
        card.IsStereotype = isStereotype;
        currentCardNum++;
    }
    private void SpeedUp()
    {

    }
}
