using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Ins;

    public DialogUI pauseDialog;
    public GameObject helpAndpause;
    public HelpDialog helpDialog;
    public void Awake()
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
    void Start() 
    { 
        Physics2D.IgnoreLayerCollision(8, 8, true);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseGame()
    {
        if (pauseDialog)
        {
            helpAndpause.SetActive(false);
            pauseDialog.Show(true);
            Time.timeScale = 0;
        }
    }
    public void ShowHelpDialog()
    {
        helpDialog.Show(true);
        Time.timeScale = 0;
        helpAndpause.gameObject.SetActive(false);
    }

}
