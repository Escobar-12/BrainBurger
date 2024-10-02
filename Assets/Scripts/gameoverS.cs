using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameoverS : MonoBehaviour
{
    [SerializeField] Button start;
    [SerializeField] Button quit;
    [SerializeField] Button Reward;

    [SerializeField] GameObject menu;
    

    void Awake()
    {
        start.onClick.AddListener(startgame);
        quit.onClick.AddListener(quitgame);
        Reward.onClick.AddListener(reward);
    }
    
    void startgame()
    {
        Invoke("Restart",0.1f);
    }
    void quitgame()
    {
        Invoke("Exit",.1f);
    }
    void reward()
    {
        Invoke("Continue",.1f);
    }
    void Continue()
    {
        menu.SetActive(false);
        Time.timeScale = 1;
        GameManager.Instance.state = GameManager.State.gameplaying;
        GameManager.Instance.addtimereward();
    }
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    void Exit()
    {
        Application.Quit();
    }
}
