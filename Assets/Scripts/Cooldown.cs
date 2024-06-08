using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class Cooldown
{
    public enum CooldownProgress { Ready, InProgress, Finished}
    public CooldownProgress CurrentProgress = CooldownProgress.Ready;

    public float TimeLeft { get { return _currentDuration; } }
    public bool IsOnCooldown { get { return _inOnCooldown; } }

    [SerializeField] private float _duration = 1f;
    private float _currentDuration = 0f;

    private bool _inOnCooldown = false;

    private Coroutine _coroutine;

   public void StartCooldown()
    {
        if (CurrentProgress is CooldownProgress.InProgress)
            return;

        _coroutine = CoroutineHost.Instance.StartCoroutine(DoCooldown());
    }

    public void StopCooldown()
    {
        if (_coroutine == null)
            return;

        CoroutineHost.Instance.StopCoroutine(_coroutine);

        _currentDuration = 0f;
        _inOnCooldown = false;
        CurrentProgress = CooldownProgress.Ready;
    }

    IEnumerator DoCooldown()
    {
        _currentDuration = _duration;
        _inOnCooldown = true;

        while (_currentDuration > 0f)
        {
            _currentDuration = Time.deltaTime;
            CurrentProgress = CooldownProgress.InProgress;

            yield return null;
        }

        _currentDuration = 0f;
        _inOnCooldown = false;

        CurrentProgress = CooldownProgress.Finished;
    }
}
