using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    public static GUIManager Ins;

    public GameObject mainMenu;
    public GameObject levelSelectUI;
    public GameObject pauseDialog;
    public GameObject settingVolume;
    public GameObject helpAndpausePanel;
    public HelpDialog helpDialog;
    public GameObject dashPanel;
    public GameObject completeLevelDialog;
    public GameObject conditionLevel1;
    public GameObject unLockedDialog;
    public Slider sliderBGVolume;
    public Slider sliderSFXVolume;

    public GameObject triggerMove;
    public GameObject triggerJump;
    public GameObject triggerDoubleJump;
    public GameObject triggerDash;
    public GameObject triggerEnemyhelp;
    public GameObject triggerClimb;

    private void Awake()
    {
        if (Ins)
        {
            Destroy(gameObject);
        }
        else
        {
            Ins = this;
        }
    }
    private void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals("Tutorial"))
        {
            if (dashPanel)
            {
                dashPanel.SetActive(false);
            }
        }
        if (SceneManager.GetActiveScene().name.Equals("Tutorial"))
        {
            if (triggerMove)
            {
                ShowMoveTriggerTutorial();
            }
        }
        LoadVolume();
    }
    void LoadVolume()
    {
        if (sliderBGVolume != null && sliderSFXVolume != null)
        {
            sliderBGVolume.value = PlayerPrefs.GetFloat("BGVolume", 1);
            sliderSFXVolume.value = PlayerPrefs.GetFloat("SFXVolume", 1);
            AudioController.Ins.musicAus.volume = sliderBGVolume.value;
            AudioController.Ins.sfxAus.volume = sliderSFXVolume.value;
        }
    }
    public void ChangeBGVolume()
    {
        if (sliderBGVolume != null)
        {
            AudioController.Ins.musicAus.volume = sliderBGVolume.value;

            PlayerPrefs.SetFloat("BGVolume", sliderBGVolume.value);
        }

    }
    public void ChangeSFXVolume()
    {
        if (sliderSFXVolume != null)
        {
            AudioController.Ins.sfxAus.volume = sliderSFXVolume.value;
            PlayerPrefs.SetFloat("SFXVolume", sliderSFXVolume.value);
        }

    }
    public void ShowSettingVolume()
    {
        if (settingVolume)
        {
            Time.timeScale = 0;
            if (mainMenu)
            {
                mainMenu.SetActive(false);
            }
            settingVolume.SetActive(true);
            if (helpAndpausePanel)
            {
                helpAndpausePanel.SetActive(false);
            }

        }
    }
    public void CloseSettingVolume()
    {
        if (settingVolume)
        {
            Time.timeScale = 1;
            if (mainMenu)
            {
                mainMenu.SetActive(true);
            }
            settingVolume.SetActive(false);
            if (helpAndpausePanel)
            {
                helpAndpausePanel.SetActive(true);
            }

        }
    }
    public void ShowLevelSelect()
    {
        if (levelSelectUI && mainMenu)
        {
            levelSelectUI.SetActive(true);
            mainMenu.SetActive(false);
        }
    }
    public void BackToTitle()
    {
        SceneManager.LoadScene("Title");
        Time.timeScale = 1f;
    }
    public void Replay()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Resume()
    {
        if(helpAndpausePanel && pauseDialog)
        {
            Time.timeScale = 1;
            helpAndpausePanel.SetActive(true);
            pauseDialog.SetActive(false);
        }
    }
    public void PauseGame()
    {
        if (pauseDialog && helpAndpausePanel)
        {
            helpAndpausePanel.SetActive(false);
            pauseDialog.SetActive(true);
            Time.timeScale = 0;
        }
    }
    public void ClosePauseDialog()
    {
        if (pauseDialog && helpAndpausePanel)
        {
            helpAndpausePanel.SetActive(true);
            pauseDialog.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void ShowUnlockedLevelDialog()
    {
        if (unLockedDialog && mainMenu)
        {
            unLockedDialog.SetActive(true);
            mainMenu.SetActive(false);
        }
    }
    public void ShowHelpDialog()
    {
        if (helpDialog && helpAndpausePanel)
        {
            helpDialog.Show(true);
            Time.timeScale = 0;
            helpAndpausePanel.gameObject.SetActive(false);
        }
    }
    public void ShowMoveTriggerTutorial()
    {
        if (triggerMove && helpAndpausePanel)
        {
            triggerMove.SetActive(true);
            helpAndpausePanel.gameObject.SetActive(false);
            Time.timeScale = 0;
        }
    }
    public void CloseMoveTriggerTutorial()
    {
        if (triggerMove && helpAndpausePanel)
        {
            triggerMove.SetActive(false);
            helpAndpausePanel.gameObject.SetActive(true);
            Time.timeScale = 1;
        }
    }
    public void ShowHelpDialogTriggerJumpTutorial()
    {
        if (triggerJump && helpAndpausePanel)
        {
            triggerJump.SetActive(true);
            helpAndpausePanel.gameObject.SetActive(false);
            Time.timeScale = 0;
        }
    }
    public void CloseHelpDialogTriggerJumpTutorial()
    {
        if (triggerJump && helpAndpausePanel)
        {
            triggerJump.SetActive(false);
            helpAndpausePanel.gameObject.SetActive(true);
            Time.timeScale = 1;
        }
    }
    public void ShowHelpDialogTriggerDoubleJumpTutorial()
    {
        if (triggerDoubleJump && helpAndpausePanel)
        {
            triggerDoubleJump.SetActive(true);
            helpAndpausePanel.gameObject.SetActive(false);
            Time.timeScale = 0;
        }
    }
    public void CloseHelpDialogTriggerDoubleJumpTutorial()
    {
        if (triggerDoubleJump && helpAndpausePanel)
        {
            triggerDoubleJump.SetActive(false);
            helpAndpausePanel.gameObject.SetActive(true);
            Time.timeScale = 1;
        }
    }
    public void ShowHelpDialogTriggerDashTutorial()
    {
        if (triggerDash && helpAndpausePanel)
        {
            triggerDash.SetActive(true);
            helpAndpausePanel.gameObject.SetActive(false);
            Time.timeScale = 0;
        }
    }
    public void CloseHelpDialogTriggerDashTutorial()
    {
        if (triggerDash && helpAndpausePanel)
        {
            triggerDash.SetActive(false);
            helpAndpausePanel.gameObject.SetActive(true);
            Time.timeScale = 1;
        }
    }

    public void ShowHelpDialogTriggerEnemyHelpTutorial()
    {
        if (triggerEnemyhelp && helpAndpausePanel)
        {
            triggerEnemyhelp.SetActive(true);
            helpAndpausePanel.gameObject.SetActive(false);
            Time.timeScale = 0;
        }
    }
    public void CloseHelpDialogTriggerEnemyHelpTutorial()
    {
        if (triggerEnemyhelp && helpAndpausePanel)
        {
            triggerEnemyhelp.SetActive(false);
            helpAndpausePanel.gameObject.SetActive(true);
            Time.timeScale = 1;
        }
    }

    public void ShowHelpDialogTriggerClimbTutorial()
    {
        if (triggerClimb && helpAndpausePanel)
        {
            triggerClimb.SetActive(true);
            helpAndpausePanel.gameObject.SetActive(false);
            Time.timeScale = 0;
        }
    }
    public void CloseHelpDialogTriggerClimbTutorial()
    {
        if (triggerClimb && helpAndpausePanel)
        {
            triggerClimb.SetActive(false);
            helpAndpausePanel.gameObject.SetActive(true);
            Time.timeScale = 1;
        }
    }

    public void ShowCompleteDialog()
    {
        if (completeLevelDialog && helpAndpausePanel)
        {
            completeLevelDialog.SetActive(true);
            helpAndpausePanel.gameObject.SetActive(false);
            Time.timeScale = 0;
        }
    }
    public void EnterLevel1AfterTutorial() 
    { 
        if(PlayerPrefs.GetInt(LevelConst.LEVEL_UNLOCKED + "1") == 1)
        {
                SceneManager.LoadScene("Level1");
                Time.timeScale = 1;
        }
    }
    public void ShowConditionLevel1()
    {
        if (conditionLevel1 && levelSelectUI)
        {
            conditionLevel1.SetActive(true);
            levelSelectUI.SetActive(false);
        }
    }
    public void LoadSceneTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
