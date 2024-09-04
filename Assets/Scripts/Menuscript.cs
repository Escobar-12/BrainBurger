using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menuscript : MonoBehaviour
{
    [SerializeField] Button start;
    [SerializeField] Button quit;
    [SerializeField] GameObject loading; 
    

    void Awake()
    {
        start.onClick.AddListener(startgame);
        quit.onClick.AddListener(quitgame);
    }
    void Start()
    {
        loading.SetActive(false);
    }
    
    void startgame(){
        loading.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    void quitgame(){
        Application.Quit();
    }
}
