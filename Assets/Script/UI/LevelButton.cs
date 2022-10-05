using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    public int level;

    public GameObject statusLevel = null;

    private void Start()
    {
        if(PlayerPrefs.GetInt(LevelConst.LEVEL_UNLOCKED + level) == 1)
        {
            if (statusLevel)
            {
                statusLevel.SetActive(false);
            }
        }
        else
        {
            if (statusLevel)
            {
                statusLevel.SetActive(true);
            }
        }
    }
    public void EnterLevel()
    {
        if (level == 1 && PlayerPrefs.GetInt(LevelConst.LEVEl_PASSED + "Tutorial",0) != 1)
        {
            GUIManager.Ins.ShowConditionLevel1();
            return;
        }
        if (PlayerPrefs.GetInt(LevelConst.LEVEL_UNLOCKED + level) == 1)
        {
            if(SceneManager.GetSceneByName(LevelConst.LEVEL_UNLOCKED + level).IsValid())
            {
                SceneManager.LoadScene(LevelConst.LEVEL_UNLOCKED + level);
            }

        }

    }
}
