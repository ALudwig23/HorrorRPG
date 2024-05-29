using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using TMPro;
using UnityEngine.UI;

public class InCombatButtonSelect : MonoBehaviour
{
    [SerializeField] private GameObject FightButtonSelected;
    [SerializeField] private GameObject FightButtonUnselected;
    [SerializeField] private GameObject SpecialActionsButtonSelected;
    [SerializeField] private GameObject SpecialActionsButtonUnselected;
    [SerializeField] private GameObject ItemsButtonSelected;
    [SerializeField] private GameObject ItemsButtonUnselected;
    [SerializeField] private GameObject RunButtonSelected;
    [SerializeField] private GameObject RunButtonUnselected;
    [SerializeField] private GameObject Limb1Button;
    [SerializeField] private GameObject Limb2Button;
    [SerializeField] private GameObject Limb3Button;
    [SerializeField] private TMP_Text Dialogue;

    public enum CombatMenu { MainMenu, FightMenu, ItemsMenu , SpecialActionsMenu}
    public enum CombatButtonSelection { None, Fight, Items, SpecialActions, Run };
    public CombatButtonSelection CurrentSelection;
    public CombatMenu CurrentMenu;
    private int MaxSelections;
    private int MainMenuSelections = 4;
    public int _combatSelection;

    //Enemies
    [SerializeField] private TiedMonster TiedMonster;
    [SerializeField] private ToggleMoves ToggleMoves;

    private void Start()
    {
        _combatSelection = 1;
        CurrentMenu = CombatMenu.MainMenu;
        MaxSelections = MainMenuSelections; 
        Dialogue.text = "An enemy approaches...";
    }

    private void FixedUpdate()
    {
        HoverButton();
        SelectButton();
        SelectMovement();
    }

    private void SelectButton()
    {
        switch (CurrentMenu)
        {

            case CombatMenu.MainMenu:

                MaxSelections = MainMenuSelections; //keeps track of max available selections for current menu.

                if (Input.GetKey(KeyCode.Return))
                {
                    Debug.Log("Selected");

                    FightButtonSelected.SetActive(false);
                    FightButtonUnselected.SetActive(false);

                    ItemsButtonSelected.SetActive(false);
                    ItemsButtonUnselected.SetActive(false);

                    SpecialActionsButtonSelected.SetActive(false);
                    SpecialActionsButtonUnselected.SetActive(false);

                    RunButtonSelected.SetActive(false);
                    RunButtonUnselected.SetActive(false);

                    if (CurrentSelection == CombatButtonSelection.Fight)
                    {
                        CurrentMenu = CombatMenu.FightMenu;
                    }

                    if (CurrentSelection == CombatButtonSelection.Items)
                    {
                        CurrentMenu = CombatMenu.ItemsMenu;
                    }

                    if (CurrentSelection == CombatButtonSelection.SpecialActions)
                    {
                        CurrentMenu = CombatMenu.SpecialActionsMenu;
                    }

                    if (CurrentSelection == CombatButtonSelection.Run)
                    {
                        Dialogue.text = "Escaped Successfully...";
                    }
                }
                break;

            case CombatMenu.FightMenu:

                MaxSelections = TiedMonster.MaxBodyParts;
                Dialogue.text = "Choose a limb to attack...";

                Limb1Button.SetActive(true);
                Limb2Button.SetActive(true);
                Limb3Button.SetActive(true);

                break;
        }
    }

    private void HoverButton()
    {
        if (FightButtonSelected == null || FightButtonUnselected == null)
            return;

        if (RunButtonSelected == null || RunButtonUnselected == null)
            return;

        switch (CurrentMenu)
        {
            case CombatMenu.MainMenu:

                if (_combatSelection == 1)
                {
                    if (CurrentSelection == CombatButtonSelection.Fight)
                        return;

                    //Debug.Log("OnFight");

                    FightButtonSelected.SetActive(true);
                    FightButtonUnselected.SetActive(false);

                    ItemsButtonSelected.SetActive(false);
                    ItemsButtonUnselected.SetActive(true);

                    SpecialActionsButtonSelected.SetActive(false);
                    SpecialActionsButtonUnselected.SetActive(true);

                    RunButtonSelected.SetActive(false);
                    RunButtonUnselected.SetActive(true);

                    CurrentSelection = CombatButtonSelection.Fight;
                }

                if (_combatSelection == 2)
                {
                    if (CurrentSelection == CombatButtonSelection.Items)
                        return;

                    //Debug.Log("OnItems");

                    FightButtonSelected.SetActive(false);
                    FightButtonUnselected.SetActive(true);

                    ItemsButtonSelected.SetActive(true);
                    ItemsButtonUnselected.SetActive(false);

                    SpecialActionsButtonSelected.SetActive(false);
                    SpecialActionsButtonUnselected.SetActive(true);

                    RunButtonSelected.SetActive(false);
                    RunButtonUnselected.SetActive(true);

                    CurrentSelection = CombatButtonSelection.Items;
                }

                if (_combatSelection == 3)
                {
                    if (CurrentSelection == CombatButtonSelection.SpecialActions)
                        return;

                    //Debug.Log("OnSpecialAction")

                    FightButtonSelected.SetActive(false);
                    FightButtonUnselected.SetActive(true);

                    ItemsButtonSelected.SetActive(false);
                    ItemsButtonUnselected.SetActive(true);

                    SpecialActionsButtonSelected.SetActive(true);
                    SpecialActionsButtonUnselected.SetActive(false);

                    RunButtonSelected.SetActive(false);
                    RunButtonUnselected.SetActive(true);
                }

                if (_combatSelection == 4)
                {
                    if (CurrentSelection == CombatButtonSelection.Run)
                        return;

                    //Debug.Log("OnRun");

                    FightButtonSelected.SetActive(false);
                    FightButtonUnselected.SetActive(true);

                    ItemsButtonSelected.SetActive(false);
                    ItemsButtonUnselected.SetActive(true);

                    SpecialActionsButtonSelected.SetActive(false);
                    SpecialActionsButtonUnselected.SetActive(true);

                    RunButtonSelected.SetActive(true);
                    RunButtonUnselected.SetActive(false);

                    CurrentSelection = CombatButtonSelection.Run;
                }
                break;

            case CombatMenu.FightMenu:
                break;

        }
    }

    private void SelectMovement()
    {
        if (_combatSelection > MaxSelections)
        {
            _combatSelection = MaxSelections;
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (_combatSelection == 1 || _combatSelection == 3)
                return;

            //Debug.Log("Left");

            if (_combatSelection == 2 || _combatSelection == 4)
            {
                _combatSelection--;
            }
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (_combatSelection == 2 || _combatSelection == 4)
                return;

            //Debug.Log("Right");

            if (_combatSelection == 1 || _combatSelection == 3)
            {
                _combatSelection++;
            }
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            if (_combatSelection >= 3)
                return;

            //Debug.Log("Down");

            if (_combatSelection <= 2)
            {
                _combatSelection += 2;
            }
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            if (_combatSelection <= 2)
                return;

            //Debug.Log("Up")

            if (_combatSelection >= 3)
            {
                _combatSelection -= 2;
            }
        }
    }
}
