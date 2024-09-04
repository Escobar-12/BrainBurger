using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] GameObject selectable;
    [SerializeField] Transform spon_point,player;
    SOobjects objectONcounter;
    

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
        // when interacting with a counter, place the object on the table if it's empty or place it on the player if he has no objects in hand , give it to the player
        if (!hasObject())
        {
            if(movment.Instance.PlayerHasSomthing())
            {
                Transform heldObject = player.GetChild(0);
                heldObject.SetParent(spon_point);
                heldObject.localPosition = Vector3.zero;
                heldObject.localRotation = Quaternion.identity;
                movment.Instance.ObjectONplayer = null;
                SetCounterObject(heldObject.GetComponent<SOobjects>());
                objectONcounter.setoncounter(this);
                abjectonkitchen.Instance.thingsInKitchen.Add(objectONcounter);
            }
            else
            {
                //player not carrying anything
            }
            
        }
        // else if there was something on the counter
        else 
        {
            if(!movment.Instance.PlayerHasSomthing()){
                // if the player has nothing give everything on the counter to it
                abjectonkitchen.Instance.thingsInKitchen.Remove(objectONcounter);
                getobjectonplayer();
                
            }
            
            //if the player has Something
            if(movment.Instance.PlayerHasSomthing())
            {
                //player has a plate
                if(movment.Instance.GetObjectOnPlayer() is platesSO)
                {
                    platesSO plateOnplayer = movment.Instance.GetObjectOnPlayer() as platesSO;
                    if(objectONcounter is platesSO)
                    {
                        //if the table also has a plate
                        platesSO platoncounter = objectONcounter as platesSO;
                        plateOnplayer.merge(platoncounter.FoodOnPlate);
                        //plateOnplayer.AddToPlatList(movment.Instance.GetObjectOnPlayer());
                        //platoncounter.spawnatplate(platoncounter.FoodOnPlate);
                        plateOnplayer.spawnall();
                        plateOnplayer.resetplatetotrue(platoncounter.getobjectlist());
                        
                        platoncounter.cleartheplate();
                    }
                    else if(plateOnplayer.AddToPlatList(objectONcounter))
                    // objects will be placed on the plate only if the were of tag "slicedSO" or "cookeded"
                    {
                        plateOnplayer.spawnatplate(objectONcounter);
                        CleartheCounter();
                    }
                }
                else
                //player has something else
                {
                    if(objectONcounter is platesSO)
                    {
                        //if the table has a plate
                        platesSO platoncounter = objectONcounter as platesSO;
                        if(platoncounter.AddToPlatList(movment.Instance.GetObjectOnPlayer()))
                        {
                            // if the player is holding something that is slised or cooked place it on the counter(plate on counter)
                            platoncounter.spawnatplate(movment.Instance.GetObjectOnPlayer());
                            movment.Instance.Cleartheplayer();
                        }
                        else
                        {
                            // object can't be placed on the plates
                        }
                        
                    }
                }
            }
            
        }
    }
    public void getobjectonplayer()
    {
        // giving the object to the player after deleting the old children because the player can only cary one object
        
        if (hasObject())
        {
            movment.Instance.ObjectONplayer = objectONcounter;
            objectONcounter.transform.SetParent(player);
            objectONcounter.transform.localPosition = Vector3.zero;

            objectONcounter.setoncounter(null);
            CleartheCounter();
        }
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
