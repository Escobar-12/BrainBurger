using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class contaners : MonoBehaviour
{
    [SerializeField] GameObject selectable;
    [SerializeField] scriptableobjects Sobject;
    [SerializeField] Transform spon_point,player;
    [SerializeField] public GameObject visual_container;
    SOobjects objectONcontainer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void interact(){
        // when interacting with a container, trigger animation (open door) and instantiate a new object,
        // give it to the player if he has nothing
        if(!movment.Instance.PlayerHasSomthing())
        {
            visual_container.GetComponent<Animator>().SetTrigger("trigger");
            if(objectONcontainer == null)
            {
                Transform heldobject = Instantiate(Sobject.prefab.transform,spon_point.position,Quaternion.identity);
                objectONcontainer = heldobject.GetComponent<SOobjects>();
                objectONcontainer.setoncontaner(this);
            }
            getobjectonplayer();
        }
        else{
            Debug.Log("Player already has an object in hand");
        }
    }
    public void getobjectonplayer(){
        // giving the object to the player after deleting the old children because the player can only cary one object
        if(player.childCount > 0)
        {
            objectONcontainer.DIS(player);
        }
        if(objectONcontainer != null){
            objectONcontainer.setoncontaner(null);
            objectONcontainer.transform.SetParent(player);
            objectONcontainer.transform.localPosition = Vector3.zero;
            movment.Instance.ObjectONplayer =  objectONcontainer;
            
            objectONcontainer = null;
        }
    }
    public void show(){
        selectable.SetActive(true);
    }
    public void hide(){
        selectable.SetActive(false);
    }
}
