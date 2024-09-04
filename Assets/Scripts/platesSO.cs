using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.UIElements;
using UnityEngine;

public class platesSO : SOobjects
{
    [SerializeField] iconsUIscript uihandler;
    [SerializeField] Transform spawn;
    [SerializeField] public GameObject FullMeal;
    [SerializeField] List<SOobjects> CanPlaceOnPlate;
    public List<SOobjects> FoodOnPlate;
    [SerializeField] SOobjects[] AllowedTags;
    bool hasFullMealVisual;

    void Awake()
    {
        FoodOnPlate = new List<SOobjects>();
    }
    public bool AddToPlatList(SOobjects Ingredient){
        if(HasObjectAllowed(Ingredient))
        {
            //uihandler.iconspawn(Ingredient);
            FoodOnPlate.Add(Ingredient);
            return true;
        }
        else
        {
            Debug.Log("Can't be placed on the plate");
            return false;
        }
    }
    public void spawnatplate(SOobjects food){
        FullMeal.GetComponent<FullMeal>().ActivateFoodVisual(food);
        food.transform.SetParent(spawn);
        food.transform.localPosition = Vector3.zero;
        food.gameObject.SetActive(false);

        uihandler.iconspawn(food);
    }
    public void merge(List<SOobjects> newlist){
        FoodOnPlate = FoodOnPlate.Union(newlist).ToList();
    }
    public void spawnall(){
        foreach(SOobjects ingredient in FoodOnPlate){
            ingredient.transform.SetParent(spawn);
            ingredient.transform.localPosition = Vector3.zero;
        }
    }
    // public void cleartheplate(){
    //     FoodOnPlate.Clear();
    // }
    public void cleartheplate()
    {
        // Deactivate all visuals
        foreach (SOobjects food in FoodOnPlate)
        {
            FullMeal.GetComponent<FullMeal>().DeactivateFoodVisual(food);
        }
        // Clear the plate
        FoodOnPlate.Clear();
        
        // Optionally, clear the UI icons as well
        uihandler.clearcanvas();
    }
    public void resetplatetotrue(List<SOobjects> newsprites){
        // activate all visuals
        foreach (SOobjects food in FoodOnPlate)
        {
            FullMeal.GetComponent<FullMeal>().ActivateFoodVisual(food);
        }
        uihandler.iconspawnlist(newsprites);
    }

    SOobjects GetObjectOnTag(SOobjects inputSO){
        foreach(SOobjects Allowedfood in AllowedTags){
            if(Allowedfood.tag == inputSO.tag){
                return Allowedfood;
            }
        }
        return null;
    }
    bool HasObjectAllowed(SOobjects inputSO){
        if(inputSO != null)
        {
            foreach(SOobjects Allowedfood in AllowedTags){
            if(Allowedfood.tag == inputSO.tag){
                return true;
            }
        }
        }
        return false;
    }
    public List<SOobjects> getobjectlist(){
        return FoodOnPlate;
    }
}
