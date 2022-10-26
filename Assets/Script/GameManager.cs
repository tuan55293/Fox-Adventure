using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Ins;
    public GameObject player;
    public bool gameOver;
    public bool gameOverHandle;

    public void Awake()
    {
        Time.timeScale = 1f;
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
        Application.targetFrameRate = 60;
        Physics2D.IgnoreLayerCollision(8, 8, true);
        Physics2D.IgnoreLayerCollision(9, 0, true);
        Physics2D.IgnoreLayerCollision(9, 8, true);
        Physics2D.IgnoreLayerCollision(9, 9, true);

        PlayerPrefs.SetInt(LevelConst.LEVEl_PASSED + 1, 1);
        PlayerPrefs.SetInt(LevelConst.LEVEL_UNLOCKED + 1, 1);

    }
    void Update()
    {
        if (gameOver && gameOverHandle == false)
        {
            gameOverHandle = true;
            AudioController.Ins.PlaySound(AudioController.Ins.die);
            Invoke("GameOver", 0.5f);
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void UnLockAllLevel()
    {
        for(int i = 1; i < 5; i++)
        {
            PlayerPrefs.SetInt(LevelConst.LEVEL_UNLOCKED + i,1);
        }
        GUIManager.Ins.ShowUnlockedLevelDialog();
    }
}
