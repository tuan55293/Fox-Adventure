using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Ins;
    public GameObject player;
    public bool gameOver;

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
        Physics2D.IgnoreLayerCollision(8, 8, true);
        PlayerPrefs.SetInt(LevelConst.LEVEl_PASSED + 1, 1);
        PlayerPrefs.SetInt(LevelConst.LEVEL_UNLOCKED + 1, 1);
    }
    void Update()
    {
        if (gameOver)
        {
            Invoke("GameOver", 0.5f);
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
