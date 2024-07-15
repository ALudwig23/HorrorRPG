using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NavigationManager : MonoBehaviour
{
    private float _waitTime = 1f;

    private GameObject _previousSelectedOption;
    
    //Main Battle Options
    [Header("Main Battle Options")]
    [SerializeField] private GameObject FightOption;
    [SerializeField] private GameObject StatusOption;
    [SerializeField] private GameObject ActionOption;
    [SerializeField] private GameObject RunOption;

    //Fight UI
    [Header("Fight UI Display")]
    [SerializeField] private GameObject FightDisplay;

    //Status UI
    [Header("Status UI Display")]
    [SerializeField] private GameObject StatusDisplay;

    //Action UI
    [Header("Action UI Display")]
    [SerializeField] private GameObject ActionDisplay;

    private void Update()
    {
        if (_waitTime >= 0f)
        {
            _waitTime -= Time.deltaTime;
        }

        //Return to previous option
        if (Input.GetKeyDown(KeyCode.Backspace) || _waitTime == 0f)
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
        FightOption.SetActive(false);
        StatusOption.SetActive(false);
        ActionOption.SetActive(false);
        RunOption.SetActive(false);

        FightDisplay.SetActive(true);
        _previousSelectedOption = FightOption;
    }

    public void StatusButtonPressed()
    {
        FightOption.SetActive(false);
        StatusOption.SetActive(false);
        ActionOption.SetActive(false);
        RunOption.SetActive(false);

        StatusDisplay.SetActive(true);
        _previousSelectedOption = StatusOption;
    }

    public void ActionOptionPressed()
    {
        FightOption.SetActive(false);
        StatusOption.SetActive(false);
        ActionOption.SetActive(false);
        RunOption.SetActive(false);

        ActionDisplay.SetActive(true);
        _previousSelectedOption = ActionOption;
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
        if (FightDisplay.activeSelf == true)
        {
            FightOption.SetActive(true);
            StatusOption.SetActive(true);
            ActionOption.SetActive(true);
            RunOption.SetActive(true);

            FightDisplay.SetActive(false);
            _previousSelectedOption = null;
        }
        else if (StatusDisplay.activeSelf == true)
        {
            FightOption.SetActive(true);
            StatusOption.SetActive(true);
            ActionOption.SetActive(true);
            RunOption.SetActive(true);

            StatusDisplay.SetActive(false);
            _previousSelectedOption = null;
        }
    }
}
