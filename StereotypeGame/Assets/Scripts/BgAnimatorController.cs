using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgAnimatorController : MonoBehaviour
{
    [SerializeField]Animator anim;
    private void Start()
    {
        GameManager.Instance.onGameEnd += DoAnim;
    }

    public void DoAnim()
    {
        anim.SetTrigger("Ending");
    }
}
