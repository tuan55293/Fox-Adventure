using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : DialogUI
{
    public LevelSelectUI levelSelectUI;

    private void Start()
    {
        PlayerPrefs.SetInt(LevelConst.LEVEl_PASSED + 1, 1);
        PlayerPrefs.SetInt(LevelConst.LEVEL_UNLOCKED + 1, 1);
    }
    public void LoadSceneTutorial()
    {
        SceneManager.LoadScene("Tutorial");

    }
    public void ShowLevelSelect()
    {
        if (levelSelectUI)
        {
            levelSelectUI.Show(true);
            this.Show(false);;
        }
    }
}
