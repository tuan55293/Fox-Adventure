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
            Invoke("GameOver", 1);
        }
        if(player)
        {
            if(player.transform.position.y < -6)
            GameOver();
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
