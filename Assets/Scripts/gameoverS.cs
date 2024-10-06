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
    float time = 0;

    void Awake()
    {
        start.onClick.AddListener(Restart);
        quit.onClick.AddListener(Exit);
        Reward.onClick.AddListener(Continue);
    }
    void Continue()
    {
        Time.timeScale = 1;
        menu.SetActive(false);
        GameManager.Instance.addtimereward();
        GameManager.Instance.state = GameManager.State.gameplaying;
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
