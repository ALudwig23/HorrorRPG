using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ambience : MonoBehaviour
{
    void Start()
    {
        SoundManager.Instance.PlayMusic("Ambience");
 
    }
}
