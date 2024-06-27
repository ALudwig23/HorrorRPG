using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportTrigger : MonoBehaviour
{
    [SerializeField] private int _loadScene = 3;
    [SerializeField] private bool _isTeleporting = false;
    public int LoadScene
    {
        get { return _loadScene; }
    }
    public bool IsTeleporting
    {
        get { return _isTeleporting; }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _isTeleporting = true;
            SceneManager.LoadScene(_loadScene);
        }
    }
}
