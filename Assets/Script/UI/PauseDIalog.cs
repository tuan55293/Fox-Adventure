using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseDIalog : DialogUI
{

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
        Time.timeScale = 1 ;
        GameManager.Ins.helpAndpause.gameObject.SetActive(true);
        base.Show(false);

    }

    public void ClosePauseDialog()
    {
        GameManager.Ins.helpAndpause.gameObject.SetActive(true);
        base.Show(false);
        Time.timeScale = 1 ;
    }
}
