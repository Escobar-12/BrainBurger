using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class soundvolume : MonoBehaviour
{
    [SerializeField] AudioSource music;
    Slider volume;
    // Start is called before the first frame update
    void Start()
    {
        volume = gameObject.GetComponent<Slider>();
        volume.value = music.volume;
    }

    // Update is called once per frame
    void Update()
    {
        setvolume();
    }    
    void setvolume(){
        music.volume = volume.value;
    }
}
