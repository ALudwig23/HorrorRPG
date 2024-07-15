using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HandleInGameMenu
{
    public void OpenOrCloseInGameMenu(GameObject inGameMenu)
    {
        if (inGameMenu.activeSelf == false)
        {
            inGameMenu.SetActive(true);
            NavigateGameMenu();
        }
        else if (inGameMenu.activeSelf == true)
        {
            inGameMenu.SetActive(false);
        }
    }

    public void NavigateGameMenu()
    {

    }

}
