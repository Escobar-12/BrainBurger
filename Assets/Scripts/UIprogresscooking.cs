using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIprogresscooking : MonoBehaviour
{
    [SerializeField] stoveCounter stove;
    [SerializeField] Image filling;
    // Start is called before the first frame update
    void Start()
    {
        if (stove != null)
        {
            //stove.ProgressEvent += setProgress;
        }
    }

    void OnDestroy()
    {
        if (stove != null)
        {
            //stove.ProgressEvent -= setProgress; // Unsubscribe from the event to avoid memory leaks
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void fill(float amount){
        Debug.Log("Filling amount: " + amount);
        filling.fillAmount = amount;
    }
    void setProgress(object sender, Ihasprogress.progressONui e){
        filling.fillAmount = e.CurrentProgressNormalized;
    }
    public void show(){
        gameObject.SetActive(true);
    }
    public void hide(){
        gameObject.SetActive(false);
    }
}
