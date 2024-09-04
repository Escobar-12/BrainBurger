using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class cuttingcounter : MonoBehaviour,Ihasprogress
{

    public event EventHandler<Ihasprogress.progressONui> ProgressHundler;
    
    [SerializeField] GameObject selectable;
    [SerializeField] GameObject visual;
    [SerializeField] uiprogress ui;
    [SerializeField] Transform spon_point,player;
    [SerializeField] SOobjects objectONCC;
    [SerializeField] cuttingRECIPE[] CuttingRecipeArray;
    int CuttingProgress;
    int MaxCuttingProgress;
    bool ReadyToTake = false;
    
    // Update is called once per frame
    void Update()
    {
        
    }
    public void interact()
    {
        // when interacting with a counter, place the object on the table if it's empty or place it on the player if he has no objects in hand , give it to the player
        if (!hasObject()  && movment.Instance.PlayerHasSomthing() && HasRecipe(player.GetChild(0).GetComponent<SOobjects>()) )
        {
            // place on the cutting counter
            Transform heldObject = player.GetChild(0);
            heldObject.SetParent(spon_point);
            heldObject.localPosition = Vector3.zero;
            movment.Instance.ObjectONplayer = null;
            SetCuttingObject(heldObject.GetComponent<SOobjects>());
            objectONCC.setonCuttingcounter(this);

            // Initialize cutting process
            CuttingProgress = 0;
            MaxCuttingProgress = getCuttingProgressMax(objectONCC);
            ui.show();
            ReadyToTake = false;
        }
        
        else if (hasObject() && !movment.Instance.PlayerHasSomthing() && CuttingProgress < MaxCuttingProgress){
            // keep clicking for cutting progress
            CuttingProgress ++;

             // play cutting animation 
            ProgressHundler?.Invoke(this,new Ihasprogress.progressONui{CurrentProgressNormalized = (float)CuttingProgress/MaxCuttingProgress});
            ui.show();
            visual.GetComponent<Animator>().SetTrigger("Cut");
            // spawn if the cutting is done;
            if (CuttingProgress >= MaxCuttingProgress)
            {
                cut();
                ui.hide();
                ReadyToTake = true;
                return;
            }

        }
        else if(hasObject() && !movment.Instance.PlayerHasSomthing() && ReadyToTake){
            
            // placing the slices on the object
            movment.Instance.ObjectONplayer = objectONCC;
            getobjectonplayer();
            CleartheCC();
        }

        // else if there was nothing on the counter and there is a plate on the player 
        else if(hasObject() && movment.Instance.GetObjectOnPlayer() is platesSO){
            platesSO plateOnplayer = movment.Instance.GetObjectOnPlayer() as platesSO;
                    if(plateOnplayer.AddToPlatList(objectONCC))
                    // objects will be placed on the plate only if the were of tsg "slicedSO" or "cookeded"
                    {
                        plateOnplayer.spawnatplate(objectONCC);
                        CleartheCounter();
                    }
                    
        }
        else{
            Debug.Log("Player already has an object in hand");
        }
    }
    public void getobjectonplayer()
    {
        // giving the object to the player after deleting the old children because the player can only cary one object
        
        if (hasObject())
        {
            
            MaxCuttingProgress = 0;
            CuttingProgress = 0;
            ProgressHundler?.Invoke(this,new Ihasprogress.progressONui{CurrentProgressNormalized = (float)CuttingProgress/MaxCuttingProgress});
            objectONCC.transform.SetParent(player);
            objectONCC.transform.localPosition = Vector3.zero;
            CleartheCC();
        }
    }
    public void CleartheCC(){
        //clearing the counter
        objectONCC = null;
    }
    public bool hasObject(){
        // check if the counter has an object
        return objectONCC != null;
    }
    public void SetCuttingObject(SOobjects counterobject){
        objectONCC = counterobject;
    }
    public void show(){
        selectable.SetActive(true);
    }
    public void hide(){
        selectable.SetActive(false);
    }
    void cut(){
        if(hasObject())
        {
            SOobjects outputSO = GetOutputForInput(objectONCC);
            
            if (outputSO != null)
            {
                objectONCC.selfdestroy();
                
                Transform sliced = Instantiate(outputSO.transform, spon_point.position, spon_point.rotation);
                if (sliced != null)
                {
                    sliced.SetParent(spon_point);
                    sliced.localPosition = Vector3.zero;
                    //sliced.tag = "slicedSO";
                    SetCuttingObject(sliced.GetComponent<SOobjects>());
                }
                else
                {
                    Debug.LogError("Failed to instantiate sliced object.");
                }
            }
            else
            {
                Debug.LogError("Object not cuttable!");
            }
        }
    }
    SOobjects GetOutputForInput(SOobjects inputSO){
        foreach(cuttingRECIPE cuttingRecipe in CuttingRecipeArray){
            if(cuttingRecipe.input.tag == inputSO.tag){
                return cuttingRecipe.output;
            }
        }
        return null;
    }
    int getCuttingProgressMax(SOobjects inputSO){
        foreach(cuttingRECIPE cuttingRecipe in CuttingRecipeArray){
            if(cuttingRecipe.input.tag == inputSO.tag){
                return cuttingRecipe.CuttingProgressMax;
            }
        }
        return 0;
    }
    bool HasRecipe(SOobjects inputSO){
        foreach(cuttingRECIPE cuttingRecipe in CuttingRecipeArray){
            if(cuttingRecipe.input.tag == inputSO.tag){
                return true;
            }
        }
        return false;
    }
    public void CleartheCounter(){
        // clears the counter
        objectONCC = null;
    }
}
