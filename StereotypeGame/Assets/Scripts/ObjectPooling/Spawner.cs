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
    [Header ("Старые карточные скриптаблы")]
    [SerializeField] private CardSO easySentences;
    [SerializeField] private CardSO hardSentences;
    [Header("Новые правильные карточные скриптаблы")]
    [SerializeField] private CardCorrectSO easyCorrect;
    [SerializeField] private CardCorrectSO hardCorrect;
    [SerializeField] private float timeToCreateCard;
    [SerializeField] private Vector2[] positions;
    [SerializeField] private float minLeftSpawn;
    [SerializeField] private float maxLeftSpawn;
    [SerializeField] private float minRightSpawn;
    [SerializeField] private float maxRightSpawn;
    [SerializeField] private float ySpawn;
    [SerializeField] private float rotationMax;
    private ObjectPool<CardController> pool;
    private List<ContentStorage> easyCardContent ;
    private List<ContentStorage> hardCardContent ;

    private int currentCardNum;
    private int currentSpawnPoint;
    private void Start()
    {
        pool = new ObjectPool<CardController>(cardPrefab, cardCount, transform);
        pool.AutoExpand = autoExpand;
        easyCardContent = RandomizeCardContent(easyCardContent, easySentences,easyCorrect);
        hardCardContent = RandomizeCardContent(hardCardContent, hardSentences,hardCorrect);
        
        StartCoroutine(TimerToCreation());
    }

    private List<ContentStorage> RandomizeCardContent(List<ContentStorage> list, CardSO contentIn,CardCorrectSO correctContent)
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
        List<string> correctAnswers = correctContent.Content.OrderBy(x => UnityEngine.Random.Range(0, correctContent.Content.Length)).ToList();
        int correctCurrentNum = 0;
        for (int i = 0; i < list.Count; i++)
        {
            if(!list[i].IsStereotype)
            {
                list[i].Content.Fact = correctAnswers[correctCurrentNum];
              
                correctCurrentNum++;
            }
                
        }
        return list;
    }

    private IEnumerator TimerToCreation()
    {
        bool nulled = false;
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
            if (currentCardNum >= easyCardContent.Count && !nulled)
            {
                currentCardNum = 0;
                nulled = true;
            }
            if (GameManager.Instance.EasyMode)
                CreateCard(easyCardContent[currentCardNum].IsStereotype, true);
            else if (hardCardContent.Count > currentCardNum)
                CreateCard(hardCardContent[currentCardNum].IsStereotype, false);
            else if (!(hardCardContent.Count > currentCardNum))
                break;
        }
    }
    private void ChangeSpawnPoint()
    {
        if (currentSpawnPoint >= 1)
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
        if(currentSpawnPoint==0)       
            card.transform.position = new Vector2( Random.Range(minLeftSpawn,maxLeftSpawn), ySpawn);       
        else
            card.transform.position = new Vector2( Random.Range(minRightSpawn, maxRightSpawn),ySpawn);

        card.SpriteHolder.rotation = Quaternion.Euler(0, 0, Random.Range(-rotationMax, rotationMax));
        if (isEasy)
            card.CardContent = easyCardContent[currentCardNum].Content;
        else
            card.CardContent = hardCardContent[currentCardNum].Content;
        card.IsStereotype = isStereotype;
        currentCardNum++;
    }

}
