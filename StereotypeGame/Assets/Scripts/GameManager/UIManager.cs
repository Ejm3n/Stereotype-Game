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
    [SerializeField] private GameObject[] lives;
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI endScore;
    [SerializeField] private TextMeshProUGUI time;
    private float timeRemain;
    private int localLives;
    private TimeSpan timeSpanLeft;
    private void Start()
    {
        startCanvas.SetActive(true);
        gameCanvas.SetActive(false);
        questionCanvas.SetActive(false);
        Time.timeScale = 0;
        timeRemain = GameManager.Instance.Timer;
        localLives = GameManager.Instance.Lives;
        GameManager.Instance.onTakeDamage += CheckLives;
        GameManager.Instance.onScoreAdding += ChangeScore;
        GameManager.Instance.onGameEnd += EndGame;
    }
    private void Update()
    {
        timeRemain -= Time.deltaTime;
        timeSpanLeft = TimeSpan.FromSeconds(timeRemain);
        time.text = timeSpanLeft.ToString(@"mm\:ss");
    }
    public void StartGame()
    {
        startCanvas.SetActive(false);
        gameCanvas.SetActive(true);
        questionCanvas.SetActive(false);
        Time.timeScale = 1;
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene(0);
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
    
    private void EndGame()
    {
        endScore.text = score.text;
        gameCanvas.SetActive(false);
        questionCanvas.SetActive(true);
        Time.timeScale = 0;
    }

}
