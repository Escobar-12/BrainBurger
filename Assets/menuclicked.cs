
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menuclicked : MonoBehaviour
{
    Button menu;
    [SerializeField] Button menubotton;
    [SerializeField] GameObject menuUI;
    [SerializeField] GameObject gamepad;
    // Start is called before the first frame update
    void Start()
    {
        menuUI.SetActive(false);
        menu = gameObject.GetComponent<Button>();
        menu.onClick.AddListener(showmenu);
        menubotton.onClick.AddListener(hidemenu);
        
    }

    void showmenu(){
        gamepad.SetActive(false);
        menu.gameObject.SetActive(false);
        menuUI.SetActive(true);
        Time.timeScale = 0;
    }
    void hidemenu(){
        gamepad.SetActive(true);
        menu.gameObject.SetActive(true);
        menuUI.SetActive(false);
        Time.timeScale = 1;
    }
}
