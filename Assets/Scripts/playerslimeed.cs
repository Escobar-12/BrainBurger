using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerslimeed : MonoBehaviour
{
    [SerializeField] GameObject mainbody;
    [SerializeField] GameObject slimedbody;
    [SerializeField] GameObject stonebody;
    // Start is called before the first frame update
    void Start()
    {
        // mainbody.SetActive(true);
        // slimedbody.SetActive(false);
    }

    public void slime(){
        slimedbody.SetActive(true);
        mainbody.SetActive(false);
        reset(3f);
    }
    public void stone(){
        stonebody.SetActive(true);
        mainbody.SetActive(false);
        reset(1.5f);
    }
    public void reset(float time){
        Invoke("resetback",time);
    }
    void resetback(){
        mainbody.SetActive(true);
        slimedbody.SetActive(false);
        stonebody.SetActive(false);
        
    }

}
