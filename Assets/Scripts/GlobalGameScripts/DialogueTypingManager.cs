using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[Serializable]
public class DialogueTypingManager
{
    [SerializeField] private float _typingSpeed = 0.04f;
    [SerializeField] private float _timeBetweenDialogue = 0.5f;
    [SerializeField] private bool _toNextDialogue;

    public bool ToNextDialogue
    {
        get { return _toNextDialogue; }
    }

    [SerializeField] private TMP_Text _dialogueText;

    private Coroutine _coroutine;
    
    public void StartDialogue(string dialogue, TMP_Text dialogueText)
    {
        if (_coroutine != null)
        {
            CoroutineHost.Instance.StopCoroutine(_coroutine);
        }

        _toNextDialogue = false;
        _coroutine = CoroutineHost.Instance.StartCoroutine(TypingDialogue(dialogue, dialogueText));
    }

    private IEnumerator TypingDialogue(string dialogue, TMP_Text dialogueText)
    {
        _dialogueText = dialogueText;
        _dialogueText.text = "";
        Debug.Log("typing");
        foreach (char letter in dialogue.ToCharArray())
        {
            _dialogueText.text += letter;
            yield return new WaitForSeconds(_typingSpeed);
        }

        yield return new WaitForSeconds(_timeBetweenDialogue);
        _toNextDialogue = true;
        _coroutine = null;
    }
}
