using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectUI : DialogUI
{
    public MainMenuUI mainMenuUI;
    public void ShowMainMenu()
    {
        if (mainMenuUI)
        {
            mainMenuUI.Show(true);
            base.Show(false);
        }
    }
}
