using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class quitapp : MonoBehaviour
{
    [SerializeField] Button start;
    [SerializeField] Button quit;
    [SerializeField] Button Reward;
    

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
        GameManager.Instance.addtimereward();
    }
    void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Exit()
    {
        Application.Quit();
    }
}
