using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ambience : MonoBehaviour
{
    void Start()
    {
        SoundManager.Instance.PlayMusic("Ambience");
        SoundManager.Instance.PlaySFX("Slice");
        SoundManager.Instance.PlaySFX("Hurt");
        SoundManager.Instance.PlaySFX("MonsterHurt");
    }
}
