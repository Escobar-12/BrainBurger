using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platecounter : MonoBehaviour
{
    public event Action<int> Onplatespawned;
    [SerializeField] GameObject selectable;
    [SerializeField] Transform spon_point,player;
    [SerializeField] scriptableobjects Sobject;
    SOobjects objectONcounter;
    
    float spawnTimer;
    float spawnTimermax = 2f;
    int platesAmount;
    int platesAmountMax = 4;
    bool canpickup = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if(spawnTimer > spawnTimermax){
            spawnTimer = 0f;

            if(platesAmount < platesAmountMax){
                platesAmount ++;
                canpickup = true;

                Onplatespawned?.Invoke(platesAmount);
            }
        }
    }

    public void interact(){
        if(!movment.Instance.PlayerHasSomthing() && canpickup){
            getobjectonplayer();
            removeone();
        }
    }
    void removeone(){
        Destroy(spon_point.GetChild(platesAmount-1).gameObject);
        platesAmount--;
    }
    public void getobjectonplayer(){
        // giving the object to the player after deleting the old children because the player can only cary one object
        
        Transform heldobject = Instantiate(Sobject.prefab.transform,player.transform);
        objectONcounter = heldobject.GetComponent<SOobjects>();
        objectONcounter.transform.SetParent(player);
        objectONcounter.transform.localPosition = Vector3.zero;

        movment.Instance.ObjectONplayer =  objectONcounter;
        objectONcounter = null;
    }
    public void show(){
        selectable.SetActive(true);
    }
    public void hide(){
        selectable.SetActive(false);
    }
    public void SetCounterObject(SOobjects counterobject){
        objectONcounter = counterobject;
    }
    public SOobjects GetCounterObject(){
        // returns the object on the counter
        if(hasObject())
        {
            return objectONcounter;
        }
        else{
            return null;
        }
    }
    public void CleartheCounter(){
        // clears the counter
        objectONcounter = null;
    }
    public bool hasObject(){
        //checks if there is something on the counter
        return objectONcounter != null;
    }

}
