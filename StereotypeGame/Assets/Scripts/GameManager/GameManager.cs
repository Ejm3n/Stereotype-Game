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
    public float CardSpeed { get => cardSpeed;}


    private void Awake()
    {
        instance = this;
    }
    public void LoseLife()
    {
        lives--;
        if (lives <= 0)
            EndGame();
    }

    private void EndGame()
    {
        Debug.Log("EndGame");
    }
}
