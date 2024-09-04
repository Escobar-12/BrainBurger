using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trash : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] GameObject selectable;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void interact()
    {
        movment.Instance.ObjectONplayer?.selfdestroy();
        movment.Instance.ObjectONplayer = null;
    }
    // public void DIS(Transform player)
    // // for removing all the old children (old food)
    // {
    //     foreach (Transform child in player)
    //     {
    //         Destroy(child.gameObject);
    //     }
    // }
    public void show(){
        selectable.SetActive(true);
    }
    public void hide(){
        selectable.SetActive(false);
    }
}
