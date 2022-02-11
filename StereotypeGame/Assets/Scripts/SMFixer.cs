using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMFixer : MonoBehaviour
{
    [SerializeField] private GameObject SM;
    void Start()
    {
        Instantiate<GameObject>(SM);
    }

   
}
