using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameCanvas;
    [SerializeField] private GameObject questionCanvas;
    [SerializeField] private GameObject startCanvas;
    [SerializeField] private GameObject buttonsCanvas;
    [SerializeField] private GameObject endCanvas;
    [SerializeField] private GameObject[] lives;
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI endScore;
    [SerializeField] private TextMeshProUGUI time;
    [SerializeField] private TextMeshProUGUI recordScore;
    private float timeRemain;
    private int localLives;
    private TimeSpan timeSpanLeft;
    private void Start()
    {
        startCanvas.SetActive(true);
        gameCanvas.SetActive(false);    
        questionCanvas.SetActive(false);
        endCanvas.SetActive(false);
        buttonsCanvas.SetActive(false);
        Time.timeScale = 0;
        timeRemain = GameManager.Instance.Timer;
        localLives = GameManager.Instance.Lives;
        GameManager.Instance.onTakeDamage += CheckLives;
        GameManager.Instance.onScoreAdding += ChangeScore;
        GameManager.Instance.onGameEnd += OpenEndCanvas;
    }
    private void Update()
    {
        timeRemain -= Time.deltaTime;
        timeSpanLeft = TimeSpan.FromSeconds(timeRemain);
        time.text = timeSpanLeft.ToString(@"mm\:ss");
    }
    public void StartGame()
    {
        buttonsCanvas.SetActive(true);
        startCanvas.SetActive(false);
        gameCanvas.SetActive(true);
        questionCanvas.SetActive(false);
        
        Time.timeScale = 1;
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene(0);
    }
    public void OpenEndCanvas()
    {
        int sco = GameManager.Instance.CurrentScore;
        if (sco == 1 || sco == 21)
            endScore.text = "Игра окончена. \nВы изменили " + sco + " стереотип";
        else if (sco == 2 || sco == 22 || sco == 3 || sco == 4)
            endScore.text = "Игра окончена. \nВы изменили " + sco + " стереотипа";
        else
            endScore.text = "Игра окончена. \nВы изменили " + sco + " стереотипов";
        if (sco > PlayerPrefs.GetInt("RecordScore"))
            PlayerPrefs.SetInt("RecordScore", sco);
        recordScore.text = PlayerPrefs.GetInt("RecordScore").ToString();
        startCanvas.SetActive(false);
        gameCanvas.SetActive(false);
        questionCanvas.SetActive(false);
        endCanvas.SetActive(true);

    }
    public void OpenQuestionCanvas()
    {
        
        gameCanvas.SetActive(false);
        questionCanvas.SetActive(true);
        Time.timeScale = 0;
    }

    private void CheckLives()
    {
        for(int i = localLives;i>GameManager.Instance.Lives;i--)
        {
            if(i>0)
                lives[i-1].SetActive(false);
        }
    }
    private void ChangeScore()
    {
        score.text = GameManager.Instance.CurrentScore.ToString() ;
    }
    
    
}
