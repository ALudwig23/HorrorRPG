using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ambience : MonoBehaviour
{
    void Start()
    {
        Debug.Log("calling ambiance " + gameObject.name);
        SoundManager.Instance.PlayMusic("Ambience");
 
    }
}
