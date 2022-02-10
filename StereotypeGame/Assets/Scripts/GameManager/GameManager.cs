using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;

    [SerializeField]private int lives = 3;

    [SerializeField] private float cardSpeed;
    [SerializeField] private float speedToAdd;
    [SerializeField] private float timeToChangeMode;
    [SerializeField] private float timer;
    [SerializeField] private int scoreToAdd = 1;
    [SerializeField] private int whenToSpeedUp;

    [SerializeField] private bool FINISH;

    private int currentScore;

    private bool easyMode= true;
    private bool gameplay = true;
    public delegate void OnTakeDamage();
    public event OnTakeDamage onTakeDamage;
    public delegate void OnScoreAdding();
    public event OnScoreAdding onScoreAdding;
    public delegate void OnGameEnd();
    public event OnGameEnd onGameEnd;
    public delegate void OnSpeedChange();
    public event OnSpeedChange onSpeedChange;
    public float CardSpeed { get => cardSpeed;}
    public bool EasyMode { get => easyMode; }
    public bool Gameplay { get => gameplay; set => gameplay = value; }
    public int Lives { get => lives; set => lives = value; }
    public float Timer { get => timer; set => timer = value; }
    public int CurrentScore { get => currentScore; }

    private void Awake()
    {
        instance = this;
        StartCoroutine(ModeChanger());
        
    }
    private void Update()
    {
        if (FINISH)
            EndGame();
    }
    public void LoseLife()
    {
        Lives--;
        onTakeDamage?.Invoke();
        if (Lives <= 0)
            EndGame();
    }
    public void AddScore()
    {
        currentScore = currentScore + scoreToAdd;
        CheckSpeedToAdd(); 
        onScoreAdding?.Invoke();
    }
    private void CheckSpeedToAdd()
    {
        if (currentScore % whenToSpeedUp == 0)
        {
            cardSpeed += speedToAdd;
            onSpeedChange?.Invoke();
        }          
    }
    private IEnumerator ModeChanger()
    {
        yield return new WaitForSeconds(timeToChangeMode);
        easyMode = false;
        yield return new WaitForSeconds(timeToChangeMode);
        Gameplay = false;
        EndGame();
    }
    private void EndGame()
    {
        
        onGameEnd?.Invoke();
        Gameplay = false;
        Debug.Log("EndGame");
    }
}
