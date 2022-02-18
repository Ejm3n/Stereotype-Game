using System.Collections;
using TMPro;
using UnityEngine;
public class CardController : MonoBehaviour
{
    public Transform SpriteHolder;
    public string Text;
    [SerializeField] private TextMeshPro textMesh;
    [SerializeField] public bool IsClickable = true;
    [SerializeField] private CardContent cardContent;
    [SerializeField] private bool isStereotype = false;
    [SerializeField] private int timeToDisable;
    [SerializeField] private AudioClip stereotypeClip;
    [SerializeField] private AudioClip factClip;
    [SerializeField] private Color red = Color.red;
    [SerializeField] private Color green = Color.green;
    [SerializeField] private Animator animPanel;
    public CardContent CardContent { set => cardContent = value; }
    public bool IsStereotype
    {
        get => isStereotype; set
        {
            isStereotype = value;
            if (isStereotype)
                animPanel.GetComponent<SpriteRenderer>().color = green;
            else
                animPanel.GetComponent<SpriteRenderer>().color = Color.black;
        }
    }



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
    public void DisableObj()
    {
        animPanel.SetTrigger("Close");
    }
    private void OnEnable()
    {
        textMesh.color = Color.black;
        IsClickable = true;
        StartCoroutine(DisableObject());
    }
    private IEnumerator DisableObject()
    {
        yield return new WaitForSeconds(timeToDisable);
        gameObject.SetActive(false);
    }
    private void OnMouseDown()
    {
        if (IsClickable)
            Pressed();
    }
    private void Pressed()
    {
        if (isStereotype)
        {
            if (SoundManagerAllControll.Instance && stereotypeClip != null)
                SoundManagerAllControll.Instance.ClipPlay(stereotypeClip);
            animPanel.SetTrigger("Start");
            textMesh.color = Color.white;
            GameManager.Instance.AddScore();
            isStereotype = false;
        }
        else
        {
            if (SoundManagerAllControll.Instance && factClip != null)
                SoundManagerAllControll.Instance.ClipPlay(factClip);
            animPanel.SetTrigger("Start");
            textMesh.color = Color.white;           
            isStereotype = true;
            GameManager.Instance.LoseLife();
        }
        IsClickable = false;
    }



}
