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
    [SerializeField] private GameObject _headLimbSelection;
    [SerializeField] private GameObject _dialogueBox;
    [SerializeField] private Sprite _buttonSprite;
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
    public GameObject LeftLimbSelection
    {
        get { return _leftLimbSelection; }
    }
    public GameObject RightLimbSelection
    {
        get { return _rightLimbSelection; }
    }
    public GameObject HeadLimbSelection
    {
        get { return _headLimbSelection; }
    }


    void Start()
    {
        _playerStats = Resources.Load<PlayerStats>("PlayerStatsData");
        //_buttonSprite = Resources.Load<Sprite>("");
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
    public void CreateLimbTarget()
    {
        //Left Leg UI
        _leftLimbSelection = new GameObject("LeftLeg");
        _leftLimbSelection.transform.SetParent(_canvas.transform, false);

        _leftLimbSelection.AddComponent<Button>();
        
        //Create RectTransform for button
        RectTransform leftLegRectTransform =_leftLimbSelection.AddComponent<RectTransform>();
        leftLegRectTransform.anchoredPosition = new Vector2(-74f, -145.5f);
        leftLegRectTransform.sizeDelta = new Vector2(115f, 147f);
        
        //Set image for button
        Image leftLegButtonImage = _leftLimbSelection.AddComponent<Image>();
        //leftLegButtonImage.sprite = _buttonSprite;

        //Create child object
        GameObject leftLegChild = new GameObject("LeftLegChild");
        leftLegChild.transform.SetParent(_leftLimbSelection.transform);

        //Add Text in child object
        TMP_Text leftLegText= leftLegChild.AddComponent<TextMeshProUGUI>();
        leftLegText.text = "Left Leg";
        leftLegText.fontSize = 24f;
        leftLegText.color = Color.black;
        leftLegText.alignment = TextAlignmentOptions.Center;

        //Set text object as the same size and position as parent
        RectTransform leftLegTextRectTransform = leftLegText.GetComponent<RectTransform>();
        leftLegTextRectTransform.position = leftLegRectTransform.position;
        leftLegTextRectTransform.sizeDelta = leftLegRectTransform.sizeDelta;
        leftLegTextRectTransform.localScale = leftLegRectTransform.localScale;

        //================================================================================================

        //Right Leg UI
        _rightLimbSelection = new GameObject("RightLeg");
        _rightLimbSelection.transform.SetParent(_canvas.transform, false);

        _rightLimbSelection.AddComponent<Button>();

        //Create RectTransform for button
        RectTransform rightLegRectTransform = _rightLimbSelection.AddComponent<RectTransform>();
        rightLegRectTransform.anchoredPosition = new Vector2(60f, -145.5f);
        rightLegRectTransform.sizeDelta = new Vector2(115f, 147f);

        //Set image for button
        Image rightLegButtonImage = _rightLimbSelection.AddComponent<Image>();
        //rightLegButtonImage.sprite = _buttonSprite;

        //Create child object
        GameObject rightLegChild = new GameObject("RightLegChild");
        rightLegChild.transform.SetParent(_rightLimbSelection.transform);

        //Add Text in child object
        TMP_Text rightLegText = rightLegChild.AddComponent<TextMeshProUGUI>();
        rightLegText.text = "Right Leg";
        rightLegText.fontSize = 24f;
        rightLegText.color = Color.black;
        rightLegText.alignment = TextAlignmentOptions.Center;

        //Set text object as the same size and position as parent
        RectTransform rightLegTextRectTransform = rightLegText.GetComponent<RectTransform>();
        rightLegTextRectTransform.position = rightLegRectTransform.position;
        rightLegTextRectTransform.sizeDelta = rightLegRectTransform.sizeDelta;
        rightLegTextRectTransform.localScale = rightLegRectTransform.localScale;

        //================================================================================================

        //Right Leg UI
        _headLimbSelection = new GameObject("Head");
        _headLimbSelection.transform.SetParent(_canvas.transform, false);

        _headLimbSelection.AddComponent<Button>();

        //Create RectTransform for button
        RectTransform headRectTransform = _headLimbSelection.AddComponent<RectTransform>();
        headRectTransform.anchoredPosition = new Vector2(194f, -145.5f);
        headRectTransform.sizeDelta = new Vector2(115f, 147f);

        //Set image for button
        Image headButtonImage = _headLimbSelection.AddComponent<Image>();
        //headButtonImage.sprite = _buttonSprite;

        //Create child object
        GameObject headChild = new GameObject("headChild");
        headChild.transform.SetParent(_headLimbSelection.transform);

        //Add Text in child object
        TMP_Text headText = headChild.AddComponent<TextMeshProUGUI>();
        headText.text = "Head";
        headText.fontSize = 24f;
        headText.color = Color.black;
        headText.alignment = TextAlignmentOptions.Center;

        //Set text object as the same size and position as parent
        RectTransform headTextRectTransform = headText.GetComponent<RectTransform>();
        headTextRectTransform.position = headRectTransform.position;
        headTextRectTransform.sizeDelta = headRectTransform.sizeDelta;
        headTextRectTransform.localScale = headRectTransform.localScale;
    }
}
