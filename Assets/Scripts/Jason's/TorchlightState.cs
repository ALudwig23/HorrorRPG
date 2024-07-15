using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchlightState : MonoBehaviour
{
    private bool isTorchlightOn = false; // Initial state of torchlight

    public static TorchlightState instance; // Singleton instance

    void Awake()
    {
        instance = this; // Singleton pattern
    }

    public void SetTorchlightState(bool state)
    {
        isTorchlightOn = state;
    }

    public bool GetTorchlightState()
    {
        return isTorchlightOn;
    }
}
