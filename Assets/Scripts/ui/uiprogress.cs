using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiprogress : MonoBehaviour
{
    [SerializeField] GameObject CCcounter;
    [SerializeField] Image FillingImage;
    Ihasprogress haveaprogress;
    // Start is called before the first frame update
    void Start()
    {
        haveaprogress = CCcounter.GetComponent<Ihasprogress>();
        haveaprogress.ProgressHundler += setProgress;
        FillingImage.fillAmount = 0f;

    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(FillingImage.fillAmount);
    }
    void setProgress(object sender, Ihasprogress.progressONui e){
        FillingImage.fillAmount = e.CurrentProgressNormalized;
    }
    public void reset(){
        FillingImage.fillAmount = 0f;
    }
    public void show(){
        gameObject.SetActive(true);
    }
    public void hide(){
        gameObject.SetActive(false);
    }
}
