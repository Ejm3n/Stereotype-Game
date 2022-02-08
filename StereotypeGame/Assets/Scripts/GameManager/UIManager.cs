using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameCanvas;
    [SerializeField] private GameObject pauseCanvas;
    [SerializeField] private Image[] lives;
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI time;
    private float timeRemain;
    private float minuts;
    private float second;
    private int localLives;
    private void Start()
    {
        timeRemain = GameManager.Instance.Timer;
        minuts = timeRemain/60;
        second = timeRemain%60;
        time.text = $"{minuts}:00";
        localLives = GameManager.Instance.Lives;
    }
    private void Update()
    {
        ChangeTimer();
    }
    private void ChangeTimer()
    {
        if (second <= 0)
        {
            minuts -= 1;
            second = 59;
        }
        else
            second = Mathf.Clamp(second - Time.deltaTime, 0, 60);

        if (second >= 10)
            time.text = $"{minuts}:{Mathf.CeilToInt(second)}";
        else
            time.text = $"{minuts}:0{Mathf.CeilToInt(second)}";
    }
    private void CheckLives()
    {
        for(int i = localLives;i>GameManager.Instance.Lives;i--)
        {

        }
    }
}
