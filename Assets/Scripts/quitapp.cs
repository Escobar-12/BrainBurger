using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class quitapp : MonoBehaviour
{
    [SerializeField] Button start;
    [SerializeField] Button quit;
    

    void Awake()
    {
        start.onClick.AddListener(startgame);
        quit.onClick.AddListener(quitgame);
    }
    
    
    void startgame(){
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void quitgame(){
        Application.Quit();
    }
}
