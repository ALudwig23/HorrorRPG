using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineHost : MonoBehaviour
{
    private static CoroutineHost _instance = null;
    public static CoroutineHost Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (CoroutineHost)FindObjectOfType(typeof(CoroutineHost));
                if (_instance == null)
                {
                    GameObject go = new GameObject("CoroutineHost");
                    _instance = go.AddComponent<CoroutineHost>();
                }
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }
}
