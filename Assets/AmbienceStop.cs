using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceStop : MonoBehaviour
{
    void Start()
    {
        SoundManager.Instance.StopMusic();

    }
}
