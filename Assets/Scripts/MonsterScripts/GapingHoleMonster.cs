using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GapingHoleMonster : MonoBehaviour
{
    private float _maxHealth;
    private float _currentHealth;
    private float _damage = 10;
    private float _sanityDamage = 10;
    private int _movesRandomizer;
    private string _text;
    private bool _finishedDialogue = false;

    [SerializeField] private GameObject _leftLimbSelection;
    [SerializeField] private GameObject _rightLimbSelection;
    [SerializeField] private GameObject _headSelection;
    [SerializeField] private GameObject _dialogueBox;
    [SerializeField] private TMP_Text _dialogueText;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Coroutine _dialogueCoroutine;
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private DialogueTypingManager _dialogueTypingManager;
    public float MaxHealth
    {
        get { return _maxHealth; }
    }
    public float CurrentHealth
    {
        get { return _currentHealth; }
        set { _currentHealth = value; }
    }
    public float Damage
    {
        get { return _damage; }
    }
    public float SanityDamage
    {
        get { return _sanityDamage; }
    }
    public bool FinishedDialogue
    {
        get { return _finishedDialogue; }
        set { _finishedDialogue = value; }
    }

    void Start()
    {
        _playerStats = Resources.Load<PlayerStats>("PlayerStatsData");
        _dialogueBox = GameObject.Find("DialogueBox");
        _dialogueText = _dialogueBox.GetComponentInChildren<TMP_Text>();
        _canvas = FindObjectOfType<Canvas>();
    }

    private void OnDeath()
    {
        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator MovesetHandler()
    {
        _movesRandomizer = Random.Range (0,2);

        switch (_movesRandomizer)
        {
            //Screech attack
            case 0:
                Debug.Log("Screech");
                _playerStats.CurrentSanity -= _sanityDamage;
                Debug.Log("Damaged");

                _text = $"The creature makes a disturbing noise";
                _dialogueTypingManager.StartDialogue(_text, _dialogueText);
                yield return new WaitUntil(() => _dialogueTypingManager.ToNextDialogue == true);
                
                _text = $"Your sanity decreased...";
                _dialogueTypingManager.StartDialogue(_text, _dialogueText);
                //yield return new WaitUntil(() => _dialogueTypingManager.ToNextDialogue == true);

                _finishedDialogue = true;
                break;

            case 1:

                Debug.Log("Lunge");
                _playerStats.CurrentSanity -= _sanityDamage;
                Debug.Log("Damaged");

                _text = $"The creature lunges towards you";
                _dialogueTypingManager.StartDialogue(_text, _dialogueText);
                yield return new WaitUntil(() => _dialogueTypingManager.ToNextDialogue == true);
                
                _text = $"You took minor damage...";
                _dialogueTypingManager.StartDialogue(_text, _dialogueText);
                //yield return new WaitUntil(() => _dialogueTypingManager.ToNextDialogue == true);

                _finishedDialogue = true;
                _playerStats.CurrentHealth -= _damage;
                break;
        }
    }

    //Create UI for limb selection for attacking
    private void CreateLimbTarget()
    {
        _leftLimbSelection = new GameObject("LeftLeg");
        _leftLimbSelection.transform.SetParent(_canvas.transform, false);

        _leftLimbSelection.AddComponent<Button>();
        _leftLimbSelection.AddComponent<RectTransform>();
        _leftLimbSelection.AddComponent<Image>();

        GameObject leftLegText = new GameObject("LeftLegText");
        leftLegText.transform.SetParent(_leftLimbSelection.transform);

        leftLegText.AddComponent<TMP_Text>();

    }
}
