using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class NavigationManager : MonoBehaviour
{
    private float _waitTime = 1f;

    private GameObject _previousSelectedOption;
    
    //Main Battle Options
    [Header("Main Battle Options")]
    [SerializeField] private GameObject _fightOption;
    [SerializeField] private GameObject _statusOption;
    [SerializeField] private GameObject _actionOption;
    [SerializeField] private GameObject _runOption;

    //Attack Pointer
    [Header("Pointer")]
    [SerializeField] private GameObject _pointer;

    //UI Display
    [Header("UI Display")]
    [SerializeField] private GameObject _uiBackground;
    [SerializeField] private GameObject _playerBodyDisplay;
    [SerializeField] private GameObject _statusEffectDisplay;
    [SerializeField] private GameObject _statusDisplay;
    [SerializeField] private GameObject _actionDisplay;

    private void Update()
    {
        if (_waitTime >= 0f)
        {
            _waitTime -= Time.deltaTime;
        }

        //Return to previous option
        if (Input.GetKeyDown(KeyCode.Backspace) && _waitTime <= 0f)
        {
            if (_previousSelectedOption != null)
            {
                EventSystem.current.SetSelectedGameObject(_previousSelectedOption);
                _waitTime = 1f;
                ReturnToPreviousSelection();
            }
        }
    }

    //Main Battle Options
    public void FightButtonPressed()
    {
        _fightOption.SetActive(false);
        _statusOption.SetActive(false);
        _actionOption.SetActive(false);
        _runOption.SetActive(false);

        _uiBackground.SetActive(false);
        _playerBodyDisplay.SetActive(false);
        _statusEffectDisplay.SetActive(false);

        _pointer.SetActive(true);
        _previousSelectedOption = _fightOption;
    }

    public void StatusButtonPressed()
    {
        _fightOption.SetActive(false);
        _statusOption.SetActive(false);
        _actionOption.SetActive(false);
        _runOption.SetActive(false);

        _statusDisplay.SetActive(true);
        _previousSelectedOption = _statusOption;
    }

    public void ActionOptionPressed()
    {
        _fightOption.SetActive(false);
        _statusOption.SetActive(false);
        _actionOption.SetActive(false);
        _runOption.SetActive(false);

        _actionDisplay.SetActive(true);
        _previousSelectedOption = _actionOption;
    }

    //Actions
    public void BlockOptionPressed()
    {

    }

    public void WhistleOptionPressed()
    {

    }

    public void CloseEyesOptionPressed()
    {

    }

    //Function to return to previous selection
    private void ReturnToPreviousSelection()
    {
        if (_pointer.activeSelf == true)
        {
            _fightOption.SetActive(true);
            _statusOption.SetActive(true);
            _actionOption.SetActive(true);
            _runOption.SetActive(true);

            _uiBackground.SetActive(true);
            _playerBodyDisplay.SetActive(true);
            _statusEffectDisplay.SetActive(true);

            _pointer.SetActive(false);
            _previousSelectedOption = null;
        }
        else if (_statusDisplay.activeSelf == true)
        {
            _fightOption.SetActive(true);
            _statusOption.SetActive(true);
            _actionOption.SetActive(true);
            _runOption.SetActive(true);

            _statusDisplay.SetActive(false);
            _previousSelectedOption = null;
        }
        else if (_actionDisplay.activeSelf == true)
        {
            _fightOption.SetActive(true);
            _statusOption.SetActive(true);
            _actionOption.SetActive(true);
            _runOption.SetActive(true);

            _actionDisplay.SetActive(false);
            _previousSelectedOption = null;
        }
    }
}
