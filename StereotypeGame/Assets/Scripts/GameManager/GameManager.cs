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

    [SerializeField] private float timeToChangeMode;
    private bool easyMode= true;
    private bool gameplay = true;
    public float CardSpeed { get => cardSpeed;}
    public bool EasyMode { get => easyMode; }
    public bool Gameplay { get => gameplay; set => gameplay = value; }

    private void Awake()
    {
        instance = this;
        StartCoroutine(ModeChanger());
    }
    public void LoseLife()
    {
        lives--;
        if (lives <= 0)
            EndGame();
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
        Gameplay = false;
        Debug.Log("EndGame");
    }
}
