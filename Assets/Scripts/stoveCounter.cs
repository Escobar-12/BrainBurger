using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stoveCounter : MonoBehaviour,Ihasprogress
{
    public event EventHandler<Ihasprogress.progressONui> ProgressHundler;
    [SerializeField] GameObject selectable;
    [SerializeField] GameObject visual;
    [SerializeField] GameObject partical;
    [SerializeField] Transform player,spon_point;
    [SerializeField] uiprogress ui;

    [SerializeField] cookingrecipeSO[] cookingRecipeArray;
    float CurrentCookingProgress = 0f,MaxCookingProgress;
    SOobjects objectONcounter;
    bool ReadyToTake;
    // Start is called before the first frame update
    void Start()
    {
        partical.GetComponent<ParticleSystem>().Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if(hasObject() ){
            CurrentCookingProgress += Time.deltaTime;
            ui.show();
            ProgressHundler?.Invoke(this,new Ihasprogress.progressONui {CurrentProgressNormalized = (float)CurrentCookingProgress / getCookingProgressMax(objectONcounter)});
            if(CurrentCookingProgress >= getCookingProgressMax(objectONcounter)){
                ReadyToTake = true;
                cook();
            }
        }
        // else{
        //         partical.GetComponent<ParticleSystem>().Stop();
        //         visual.SetActive(false);
        //         ui.hide();
        // }
    }
    public void interact(){
        if (!hasObject()  && movment.Instance.PlayerHasSomthing() && HasCookedFood(player.GetChild(0).GetComponent<SOobjects>()))
        {
            // Place on the stove 
            Transform heldObject = player.GetChild(0);
            heldObject.SetParent(spon_point);
            heldObject.localPosition = Vector3.zero;
            heldObject.localRotation = Quaternion.identity;
            movment.Instance.ObjectONplayer = null;
            ui.show();
            SetCounterObject(heldObject.GetComponent<SOobjects>());
            
            objectONcounter.setonstove(this);
            // Particales & visual
            visual.SetActive(true);
            partical.GetComponent<ParticleSystem>().Play();
            // }
            // // Initialize coocking process
            // MaxCookingProgress = getCookingProgressMax(objectONcounter);
            CurrentCookingProgress = 0f;
            ui.reset();
            ReadyToTake = false;
            // // Cook That Shit
            // Invoke("cook",MaxCookingProgress);
        }

        // else if there was nothing on the counter and there is something on the player
        else if(hasObject() && !movment.Instance.PlayerHasSomthing() ){
            // give to the player
            movment.Instance.ObjectONplayer = objectONcounter;
            getobjectonplayer();
            CleartheCounter();
        }
        // else if there was nothing on the counter and there is a plate on the player 
        else if(hasObject() && movment.Instance.GetObjectOnPlayer() is platesSO){
            //give to the plate
            platesSO plateOnplayer = movment.Instance.GetObjectOnPlayer() as platesSO;
                    if(plateOnplayer.AddToPlatList(objectONcounter))
                    // objects will be placed on the plate only if the were of tsg "slicedSO" or "cookeded"
                    {
                        plateOnplayer.spawnatplate(objectONcounter);
                        // reset all effects to default
                        CurrentCookingProgress = 0f;
                        ui.reset();
                        ui.hide();                    
                        visual.SetActive(false);
                        partical.GetComponent<ParticleSystem>().Stop();
                        CleartheCounter();
                        ReadyToTake = false;
                        
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
            visual.SetActive(false);
            partical.GetComponent<ParticleSystem>().Stop();
            movment.Instance.ObjectONplayer = objectONcounter;
            objectONcounter.transform.SetParent(player);
            objectONcounter.transform.localPosition = Vector3.zero;
            CurrentCookingProgress = 0f;
            ui.hide();
            objectONcounter.setonstove(null);
            CleartheCounter();
        }
    }
    public void SetCounterObject(SOobjects counterobject){
        objectONcounter = counterobject;
    }
    public void CleartheCounter(){
        // clears the counter
        objectONcounter = null;
    }
    public void show(){
        selectable.SetActive(true);
    }
    public void hide(){
        selectable.SetActive(false);
    }
    void cook(){
        if(hasObject())
        {
            SOobjects outputSO = GetOutputForInput(objectONcounter);
            
            if (outputSO != null)
            {
                objectONcounter.selfdestroy();
                
                Transform cooked = Instantiate(outputSO.transform);
                if (cooked != null)
                {
                    cooked.SetParent(spon_point);
                    cooked.localPosition = Vector3.zero;
                    //cooked.tag = "cookededSO";
                    SetCounterObject(cooked.GetComponent<SOobjects>());
                    ReadyToTake = true;
                    CurrentCookingProgress = 0f;
                    ui.reset();
                }
                else
                {
                    Debug.Log("Failed to instantiate cooked object.");
                }
            }
            else
            {
                partical.GetComponent<ParticleSystem>().Stop();
                visual.SetActive(false);
                ui.hide();
            }
            
        }
    }
    public bool hasObject(){
        //checks if there is something on the counter
        return objectONcounter != null;
    }
    SOobjects GetOutputForInput(SOobjects inputSO){
        foreach(cookingrecipeSO cuttingRecipe in cookingRecipeArray){
            if(cuttingRecipe.input.tag == inputSO.tag){
                return cuttingRecipe.output;
            }
        }
        return null;
    }
    bool HasCookedFood(SOobjects inputSO){
        foreach(cookingrecipeSO cuttingRecipe in cookingRecipeArray){
            if(cuttingRecipe.input.tag == inputSO.tag){
                return true;
            }
        }
        return false;
    }
    float getCookingProgressMax(SOobjects inputSO){
        foreach(cookingrecipeSO cuttingRecipe in cookingRecipeArray){
            if(cuttingRecipe.input.tag == inputSO.tag){
                return cuttingRecipe.CookingProgressMax;
            }
        }
        return 0;
    }
}
